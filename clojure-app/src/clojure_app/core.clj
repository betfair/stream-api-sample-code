(ns clojure-app.core
  (:require [clojure.data.json :as json]
            [clojure.core.async :as async]
            [clojure-app.async-sockets.async-sockets :as async-socket])
  (:gen-class))

(def options {:host "stream-api.betfair.com"
              :port 443})

(def auth {:op      "authentication"
           :appKey  "<you-app-key>"
           :session "<your-session>"})

(def market-req {:op               "marketSubscription"
                 :marketFilter     {:marketIds    []
                                    :bettingTypes ["ODDS"]
                                    :eventTypeIds [1 4]
                                    :eventIds     []
                                    :marketTypes  ["MATCH_ODDS"]}
                 :marketDataFilter {}})

(defn event-loop [socket]
  (loop []
    (when-let [line (async/<!! (:in socket))]
      (let [msg (json/read-str line :key-fn keyword)
            op  (:op msg)]
        (when-let [mc (:mc msg)]
          (prn "Received market change" mc))
        (when (= op "connection")
          (async/>!! (:out socket) (json/write-str auth))
          (prn "Send authentication message"))
        (when (= op "status")
          (async/>!! (:out socket) (json/write-str market-req))
          (prn "Subscribe to order/market stream"))
        (recur)))))

(defn -main [& _]
  (let [socket (async-socket/socket-client (:port options) (:host options))]
    (event-loop socket)))
