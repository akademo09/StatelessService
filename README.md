# README

## Add git repo to origin

git remote add origin <https://github.com/akademo09/StatelessService.git>  
git branch -M main  
git push -u origin main  
git add keda-prom-dotnet2/*  

## Keda demo

kubectl create ns keda-demo  
kubectl config set-context --current --namespace=keda-demo  
kubectl apply -f prometheus.yaml  
kubectl port-forward svc/prometheus-service 9090  
kubectl apply -f https://github.com/kedacore/keda/releases/download/v2.4.0/keda-2.4.0.yaml  
kubectl get pods -n keda  
kubectl apply -f go-deployment.yaml  
kubectl port-forward svc/go-prom-app-service 8080  
kubectl apply -f scaled-object.yaml  
kubectl get hpa  
kubectl get scaledobject.keda.sh/prometheus-scaledobject  
kubectl port-forward svc/go-prom-app-service 8080  
kubectl get pods -w -n keda-demo  
hey -n 10000 -m GET http://localhost:8080  

## Running Prometheus with service monitors working

See https://se7entyse7en.dev/posts/how-to-set-up-kubernetes-service-discovery-in-prometheus/
git clone https://github.com/se7entyse7en/prometheus-k8s-service-discovery.git
kubectl apply -f kubernetes/prometheus/namespace.yaml 
kubectl apply -f kubernetes/prometheus
kubectl port-forward --namespace monitoring deployment/prometheus 9090:9090
