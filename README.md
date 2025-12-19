# School Management System

A clean architecture ASP.NET Core Web API built to demonstrate
real-world backend engineering principles.

## Tech Stack
- ASP.NET Core Web API
- Onion Architecture
- CQRS with MediatR
- Entity Framework Core
- ASP.NET Core Identity
- FluentValidation
- xUnit + Moq

## Architecture Overview
- Domain: Core business entities
- Application: Use cases, business rules
- Infrastructure: EF Core, Identity, external services
- API: HTTP, authentication, authorization

## Features
- Student management
- Teacher management
- Class management
- Student enrollment with business rules
- Role-based authorization (Admin, Teacher, Student)
- Global error handling
- Unit testing for business logic

## Why This Architecture?
- Separation of concerns
- Testability
- Maintainability
- Scalable design

## How to Run
1. Clone repository
2. Update connection string
3. Run migrations
4. Start API

## Author
Muhdin Mussema
