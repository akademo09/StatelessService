package main

import (
    "fmt"
    "log"
    "net/http"
    "os"
    "time"

    "github.com/prometheus/client_golang/prometheus"
    "github.com/prometheus/client_golang/prometheus/promhttp"
)

type Labels map[string]string

var (
    httpRequestsCounter = prometheus.NewCounter(prometheus.CounterOpts{
        Name: "http_requests",
        Help: "number of http requests",
    })
)

func init() {
    // Metrics have to be registered to be exposed:
    prometheus.MustRegister(httpRequestsCounter)
}

func main() {
    http.Handle("/metrics", promhttp.Handler())
    http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
        fmt.Fprintf(w, "Hello, you've requested: %s\n", r.URL.Path)
    })
    http.HandleFunc("/end", func(w http.ResponseWriter, r *http.Request) {
        defer fmt.Fprintf(w, "Terminating")
        time.Sleep(3 * time.Second)
        defer httpRequestsCounter.Inc()
        os.Exit(0)
    })
    log.Fatal(http.ListenAndServe(":8080", nil))
}
