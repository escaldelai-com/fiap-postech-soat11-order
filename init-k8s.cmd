@echo off

REM -- Redis
kubectl apply -f redis-order-service.yaml
kubectl apply -f redis-order-secrets.yaml
kubectl apply -f redis-order.yaml

REM -- MongoDB
kubectl apply -f mongo-order-service.yaml
kubectl apply -f mongo-order-secrets.yaml
kubectl apply -f mongo-order-configmap.yaml
kubectl apply -f mongo-order.yaml

REM -- App
kubectl apply -f app-order-service.yaml
kubectl apply -f app-order-ingress.yaml
kubectl apply -f app-order.yaml
