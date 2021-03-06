# Identity Service

> This repository contains a robust and scalable example of an authentication microservice using JWT in .NET 6

## :hammer_and_wrench: Built With
- [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Mongo DB](https://www.mongodb.com/)
- [Mass Transit](https://masstransit-project.com/)
- [Rabbit MQ](https://www.rabbitmq.com/)
- [Redis](https://redis.io/)
- [Fluent Validation](https://fluentvalidation.net/)

## 🎯 Features

- [x] Identity
    - [x] Authenticate a user
    - [x] Refresh token
- [x] Register
    - [x] Create a new user
- [x] Password recovery
    - [x] Request change passoword verification code
    - [x] Confirm verification code
    - [x] Change password with verification
- [x] User
    - [x] Get authenticated user
    - [x] User update
- [X] Verification
    - [X] Verify phone number
    - [X] Verify email

## ⚡️ Quickstart

Inside the src folder, exec docker compose

```bash
docker compose-up
```

![Screenshot_3](https://user-images.githubusercontent.com/44218496/161750918-29d3c5d0-d017-47f7-8f06-bb1b0fa0be12.png)


## :computer: Tests

1 - Go to the directory where the test project is.
```bash
cd src\Sample.Identity.Tests
```
2 - Execute the following command.
```bash
dotnet test
```

## :bar_chart: Project Decisions

- Currently, there is no officially implemented Microsoft Identity for MongoDB, only a third-party library. As authentication is the core of the project, it was decided not to rely on these libraries.

- Sendgrid was chosen to send emails because the platform offers 100 free emails a day, in addition to being extremely cheap on paid plans and the possibility of using dynamic templates.

- For sending sms, the Zenvia platform was chosen

## :bookmark: Logs

The project has integration with the Kibana analytical tool, you can view the logs through the container that is started when performing the compose-up

![Kibana](https://user-images.githubusercontent.com/44218496/161693359-3b8c14ab-3359-47c9-83c6-93a65480c815.png)

## Patterns
- [Repository](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

- [Notification](https://martinfowler.com/eaaDev/Notification.html)

- [Common web application architectures](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)

- [Design a DDD-oriented microservice](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)

> The project architecture is based on some concepts of DDD and onion architecture, many of the concepts applied here you can find references in microsoft's own documentation.

## 👍 Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
