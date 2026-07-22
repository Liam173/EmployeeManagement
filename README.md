# Employee Management API

> A production-style ASP.NET Core Web API demonstrating enterprise backend development practices using C#, .NET, and SQL Server.

---

## 📖 Overview

The Employee Management API is a backend application built to simulate how a real-world enterprise API is designed and maintained.

Rather than focusing solely on CRUD operations, this project emphasizes clean architecture, maintainability, scalability, resilience, and software engineering best practices.

The project was built as part of an intensive backend development roadmap covering topics commonly encountered in professional .NET development.

---

# 🎯 Goals

This project aims to demonstrate:

- Clean and maintainable code
- SOLID Principles
- Layered Architecture
- Dependency Injection
- RESTful API Design
- Enterprise application structure
- Authentication & Authorization
- Testing strategies
- Resilience
- Event-driven architecture
- Performance optimisation

---

# 🏗 Architecture

The application follows a traditional layered architecture with clear separation of responsibilities.

```
                Client
                   │
                   ▼
             API Controllers
                   │
                   ▼
             Service Layer
                   │
                   ▼
          Repository Layer
                   │
                   ▼
         Entity Framework Core
                   │
                   ▼
              SQL Server
```

Cross-cutting concerns such as logging, authentication, validation, exception handling, caching, and messaging are implemented independently to keep business logic clean and maintainable.

---

# 📂 Project Structure

```
EmployeeManagementAPI

├── Controllers
├── DTOs
│   └── Employee
├── Entities
├── Interfaces
├── Services
├── Repositories
├── Validators
├── Middleware
├── Mapping
├── Events
├── Configuration
├── Logging
├── Authentication
├── Caching
├── Messaging
└── Tests
```

---

# 🚀 Features

## API

- RESTful API Design
- CRUD Operations
- Swagger Documentation
- Versioned API Structure

## Authentication

- JWT Authentication
- Role-based Authorization
- Protected Endpoints

## Validation

- FluentValidation
- Custom Validation Rules

## Mapping

- AutoMapper

## Database

- SQL Server
- Entity Framework Core
- Repository Pattern

## Architecture

- SOLID Principles
- Dependency Injection
- Service Layer
- Clean Separation of Concerns

## Error Handling

- Global Exception Middleware
- Custom Exception Types
- Structured Logging

## Performance

- In-Memory Caching
- Cache Invalidation

## Messaging

- RabbitMQ
- Event Publishing
- Event Consumers

## Reliability

- Retry Policies
- Timeout Policies
- Resilience Pipelines

## Testing

- Unit Tests
- Integration Tests
- Moq
- xUnit

---

# 💻 Technology Stack

### Backend

- C#
- .NET
- ASP.NET Core Web API

### Database

- SQL Server
- Entity Framework Core

### Authentication

- JWT

### Validation

- FluentValidation

### Mapping

- AutoMapper

### Messaging

- RabbitMQ

### Testing

- xUnit
- Moq
- Integration Testing

### Documentation

- Swagger / OpenAPI

### Tools

- Visual Studio
- Git
- GitHub
- Postman

---

# 🔐 Authentication

The API uses JWT Bearer Authentication.

Authenticate using the login endpoint to receive an access token.

Include the token in every protected request.

```
Authorization: Bearer {your_token}
```

---

# 📡 API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/employees` | Retrieve all employees |
| GET | `/api/employees/{id}` | Retrieve an employee |
| POST | `/api/employees` | Create a new employee |
| PUT | `/api/employees/{id}` | Update an employee |
| DELETE | `/api/employees/{id}` | Delete an employee |

---

# 🧪 Testing

The solution includes both Unit Tests and Integration Tests.

Testing focuses on verifying business logic, API behaviour, and application reliability.

Technologies used:

- xUnit
- Moq
- ASP.NET Core Test Host

---

# 📈 Engineering Decisions

This project intentionally follows several software engineering principles.

## Why Repository Pattern?

To separate data access from business logic and improve maintainability and testability.

---

## Why Dependency Injection?

To reduce coupling between components and simplify testing.

---

## Why AutoMapper?

To centralise object mapping and reduce repetitive code.

---

## Why FluentValidation?

To keep validation logic separate from business logic.

---

## Why JWT?

To provide stateless authentication suitable for REST APIs.

---

## Why RabbitMQ?

To decouple long-running processes and external integrations while improving system scalability.

---

## Why Caching?

To reduce unnecessary database calls and improve application performance for frequently requested data.

---

# 📚 Lessons Learned

Building this project strengthened my understanding of:

- Clean Architecture
- SOLID Principles
- Dependency Injection
- Entity Framework Core
- AutoMapper
- FluentValidation
- JWT Authentication
- Integration Testing
- RabbitMQ
- Event-Driven Architecture
- Caching Strategies
- Retry & Resilience Patterns

---

# 🚧 Future Improvements

Planned enhancements include:

- Redis Caching
- Azure Deployment
- Docker Support
- CI/CD Pipeline
- Health Checks
- API Versioning
- Distributed Tracing
- Kubernetes
- Background Services
- Role-based Permissions
- Refresh Tokens

---

# ⚙️ Getting Started

## Clone the repository

```bash
git clone https://github.com/<your-github-username>/EmployeeManagementAPI.git
```

## Navigate to the project

```bash
cd EmployeeManagementAPI
```

## Configure SQL Server

Update the connection string inside:

```
appsettings.json
```

## Apply migrations

```bash
dotnet ef database update
```

## Run the application

```bash
dotnet run
```

Swagger will be available at:

```
https://localhost:5001/swagger
```
---

# 📄 License

This project is intended for educational and portfolio purposes.

---

# 👨‍💻 Author

**Liam du Preez**

Backend Developer

LinkedIn:
https://www.linkedin.com/in/liam-du-preez-1102a51b9/

GitHub:
https://github.com/Liam173
