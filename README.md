# Microsserviço Restaurant Order
Esse microsserviço é responsável por gerenciar os pedidos, incluindo a criação, atualização, envio para pagamento, cancelamento e consulta.

## Infraestrutura
- AspNet core 10.0
- MongoDB
- Docker

## Ambiente

### Compilação
`dotnet build Restaurant.Order.WebApi/Restaurant.Order.WebApi.csproj`

### Testes
`dotnet test Restaurant.Order.Model.Test/Restaurant.Order.Model.Test.csproj`

`dotnet test Restaurant.Order.Application.Test/Restaurant.Order.Application.Test.csproj`

### Execução
`dotnet Restaurant.Order.WebApi.dll`

### Publicação
`dotnet publish Restaurant.Order.WebApi/Restaurant.Order.WebApi.csproj -c Release -o dist`

### Docker
`docker build -t restaurant-order:{{version}} .`

### Kubernetes

#### Redis
`redis-order-service.yaml` ClusterIP para comunicação interna com o App

`redis-order-secrets.yaml` Senha do Redis

`redis-order.yaml` Deployment do Redis

#### MongoDB
`mongo-order-service.yaml` ClusterIP para comunicação interna com o App

`mongo-order-secrets.yaml` Usuário e senha do MongoDB

`mongo-order-configmap.yaml` Inicialização das coleções do MongoDB

`mongo-order.yaml` StatefulSet do MongoDB

#### App
`app-order-service.yaml` ClusterIP para comunicação interna com o App

`app-order-ingress.yaml` Ingress para comunicação externa com o App

`app-order.yaml` Deployment do App

### Arquitetura
```
app-order (AspNet Core)
│
├── mongo-order (MongoDB - Pedidos, produtos, tipos de produto) (get/set)
|
├── redis-order (Redis - seq número do pedido) (get/set)
|
├── app-id (AspNet Core - identificação de clientes) (out)
|
├── app-product (AspNet Core - gerenciamento de produtos) (out)
|
└── app-pay (AspNet Core - Processamento de pagamento) (in/out)
```

## Endpoints

### Main
- `GET /` retorna `204 No Content` para o teste de vida da API.

### Product
- `GET /product/{id}` retorna os detalhes do produto pelo ID.
- `GET /product/list/{type}` retorna a lista de produtos por tipo.
- `POST /product` cria um novo produto.
- `PUT /product` atualiza um produto existente.
- `DELETE /product/{id}` remove um produto pelo ID.

### Product-Type
- `GET /product/type` retorna uma lista com os tipos de produto.

### Order
- `POST /order` cria um novo pedido.
- `POST /order/items` inclui um produto no pedido.
- `POST /order/confirm` conclui o pedido enviando para pagamento.
- `POST /order/cancel` cancela o pedido.
- `POST /order/pay` recebe a confirmação de pagamento, enviando o pedido para preparo.
