
# JWT Authentication Sample API (.NET 8)

A simple ASP.NET Core Web API project demonstrating JWT (JSON Web Token) authentication and authorization using role-based access control.

## Features

* User Registration
* User Login (JWT Authentication)
* JWT Token Generation
* Role-based Authorization
* Secure Password Storage(Password Hashing)
* Entity Framework Core with SQL Server
* Swagger Integration with JWT Authentication
* Dependency Injection
* Configuration-based Security Setting

## Technologies

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Bearer Authentication
* Swagger / OpenAPI
* Dependency Injection

## Authentication Flow

1. Register a new user.
2. Login using username and password.
3. Receive a JWT access token.
4. Add the token to the Authorization header:

```
Authorization: Bearer <your_token>
```

5. Access protected endpoints.

## JWT Configuration

JWT settings are configured through `appsettings.json`.

Configuration includes:

* Secret Key
* Issuer
* Audience
* Token Expiration

## Claims Included

The generated JWT contains the following claims:

* NameIdentifier
* Name
* Role
* Custom Claim

## Protected Endpoints

| Endpoint              | Authorization       |
| --------------------- | ------------------- |
| GET /Home/GetCategory | Public              |
| POST /Home/Register   | Public              |
| POST /Home/Login      | Public              |
| GET /Home/UserList    | Admin Role Required |

## Password Security

Passwords are hashed before being stored in the database.

> Note: This project uses SHA-256 hashing for demonstration purposes. 
## Project Purpose

This project was created as a demo project to represent implementing JWT authentication and authorization in ASP.NET Core 8 Web API.
