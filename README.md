# API Gateway for Service Fabric

The purpose of this sample is to provide a general guide to create a single single API Gateway endpoint when you don't want to expose your individual services.

So, behind this public api gateway endpoint, you will have all your services in place. Then, all requests come in through a single entrypoint that resolves internal stateless/statefull services through the Service Fabric Reverse Proxy and then, it gives out the result back to the client application.
![SF API Gateway](https://github.com/mauricio-msft/APIGatewayApplication/blob/master/images/sf-web-app-stateless-gateway.png)
