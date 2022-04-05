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
- [ ] Verification
    - [ ] Verify phone number
    - [ ] Verify email

## :memo: To Do

- Audit
- Unit Tests
- Welcome email on create account
- Phone verification
- Email Verification
- Kibana integration
- Event store
- Store all app settings keys in cloud with [Azure App configuration]('https://azure.microsoft.com/en-us/services/app-configuration/')

## ⚡️ Quickstart

Inside the api folder, exec docker compose

```bash
docker compose-up
```

## :computer: Tests

> Work in progress

## :bar_chart: Project Decisions

- Currently, there is no officially implemented Microsoft Identity for MongoDB, only a third-party library. As authentication is the core of the project, it was decided not to rely on these libraries.
- Sendgrid was chosen to send emails.
- For sending sms, the Zenvia platform was chosen

## Patterns
- [Repository](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

- [Notification](https://martinfowler.com/eaaDev/Notification.html)

- [Common web application architectures](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)

- [Design a DDD-oriented microservice](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)

> The project architecture is based on some concepts of DDD and onion architecture, many of the concepts applied here you can find references in microsoft's own documentation.

## 👍 Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
