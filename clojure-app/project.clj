(defproject clojure-app "0.1.0-SNAPSHOT"
  :description "BF streaming"
  :url "http://example.com/FIXME"
  :license {:name "EPL-2.0 OR GPL-2.0-or-later WITH Classpath-exception-2.0"
            :url  "https://www.eclipse.org/legal/epl-2.0/"}
  :dependencies [[org.clojure/clojure "1.10.1"]
                 [org.clojure/data.json "0.2.6"]
                 [org.clojure/core.async "0.4.500"]
                 [org.clojure/tools.logging "0.5.0"]]
  :main ^:skip-aot clojure-app.core
  :target-path "target/%s"
  :profiles {:uberjar {:aot :all}})
