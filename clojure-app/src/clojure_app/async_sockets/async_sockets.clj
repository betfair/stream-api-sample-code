(ns clojure-app.async-sockets.async-sockets
  (:require [clojure.java.io :as io]
            [clojure.core.async :as async]
            [clojure.tools.logging :as log])
  (:import  [java.net Socket ServerSocket SocketException InetAddress InetSocketAddress]
            [javax.net.ssl SSLSocketFactory SSLSocket]
            [java.io BufferedReader BufferedWriter]))


(def system-newline ;; This is in clojure.core but marked private.
  (System/getProperty "line.separator"))

(defn- socket-open? [^Socket socket]
  (not (or (.isClosed socket) (.isInputShutdown socket) (.isOutputShutdown socket))))

(defn- socket-read-line-or-nil [^Socket socket ^BufferedReader in]
  (when (socket-open? socket)
    (try (.readLine in)
         (catch SocketException e
           (log/error e)))))

(defn- socket-write-line [^Socket socket ^BufferedWriter out line]
  (if (socket-open? socket)
    (try
      (.write out (str line system-newline))
      (when *flush-on-newline* (.flush out))
      true
      (catch SocketException e
        (log/error e)
        false))
    false))

(defrecord AsyncSocket
    [^Socket socket ^InetSocketAddress address in-ch out-ch])
(defrecord AsyncSocketServer
    [^Integer port ^Integer backlog ^InetAddress bind-addr ^ServerSocket server connections])

(defn- ^InetAddress localhost []
  (InetAddress/getLocalHost))

(defn- host-name [^InetAddress address]
  (.getHostName address))

(defn- ^InetAddress inet-address [host]
  (if (instance? InetAddress host) host (InetAddress/getByName host)))

(def ^Integer default-server-backlog 50) ;; derived from SocketServer.java

(defn close-socket-client [{:keys [in out socket address] :as this}]
  (log/info "Closing async socket on address" address)
  ;; (when-not (.isInputShutdown socket)  (.shutdownInput socket))
  ;; (when-not (.isOutputShutdown socket) (.shutdownOutput socket))
  (when-not (.isClosed socket)         (.close socket))
  (async/close! in)
  (async/close! out)
  (assoc this :socket nil :in nil :out nil))

(defn- init-async-socket [^Socket socket ^InetSocketAddress address]
  (let [^BufferedReader in  (io/reader socket)
        ^BufferedWriter out (io/writer socket)
        in-ch               (async/chan)
        out-ch              (async/chan)
        public-socket       (map->AsyncSocket {:socket socket :address address :in in-ch :out out-ch})]

    (async/go-loop []
      (let [line (socket-read-line-or-nil socket in)]
        (if-not line
          (close-socket-client public-socket)
          (do
            (async/>! in-ch line)
            (recur)))))

    (async/go-loop []
      (let [line (and (socket-open? socket) (async/<! out-ch))]
        (if-not (socket-write-line socket out line)
          (close-socket-client public-socket)
          (recur))))

    (log/info "New async socket opened on address" address)
    public-socket))


(def factory  (. SSLSocketFactory (getDefault)))
(def socket (. factory (createSocket "stream-api.betfair.com" 443)))
(.startHandshake socket)

(defn socket-client
  "Given a port and an optional address (localhost by default), returns an AsyncSocket which must be explicitly
   started and stopped by the consumer. Observes value of *flush-on-newline* var for purposes of socket flushing."
  ([port]
   (socket-client (int port) (host-name (localhost))))
  ([^Integer port ^String address]
   (let [factory (. SSLSocketFactory (getDefault))
         socket  (. factory (createSocket address port))
         ;; socket  (SSLSocket.)
         address (InetSocketAddress. address port)]
     (.startHandshake socket)
     ;; (.connect socket address)
     (init-async-socket socket address))))

(defn server-running? [{:keys [^ServerSocket server]}]
  (and server (not (.isClosed server))))

(defn stop-socket-server [{:keys [^ServerSocket server connections port] :as this}]
  (when (server-running? this)
    (log/info "Stopping async socket server on port" port)
    (async/close! connections)
    (.close server)
    (assoc this :server nil :connections nil)))

(defn socket-server
  "Given a port and optional backlog (the maximum queue length of incoming connection indications, 50 by default)
   and an optional bind address (localhost by default), starts and returns a socket server and a :connections channel
   that yields a new socket client on each connection. Observes value of *flush-on-newline* var for purposes of
   socket flushing."
  ([port]
   (socket-server port default-server-backlog nil))
  ([port backlog]
   (socket-server port backlog nil))
  ([port backlog bind-addr]
   (let [java-server   (ServerSocket. port backlog bind-addr)
         conns         (async/chan backlog)
         public-server (map->AsyncSocketServer
                         {:port        (int port)
                          :backlog     (int backlog)
                          :bind-addr   (when bind-addr (inet-address bind-addr))
                          :connections conns
                          :server      java-server})]
     (log/info "Starting async socket server on port" port)

     (async/go-loop []
       (if (and (not (.isClosed java-server)) (.isBound java-server))
         (try
           (async/>! conns
                     (init-async-socket (.accept java-server) (.getLocalSocketAddress java-server)))
           (catch SocketException e
             (log/error e)
             (stop-socket-server public-server)))
         (stop-socket-server public-server)))

     public-server)))
