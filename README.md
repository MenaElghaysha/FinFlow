# FinFlow — Mini Loan Management System

A Loan Management System built with **ASP.NET Core**, following **Clean Architecture** and **CQRS** principles.

This project was created to practice building a maintainable .NET backend by clearly separating Domain, Application, Infrastructure, and API concerns.

---

## Features

- Create Customers
- Create Loan Applications
- Approve / Reject Loans
- Auto-generate Installments on Approval
- Pay Installments
- FluentValidation with MediatR pipeline
- Global Exception Handling with ProblemDetails
- Entity Framework Core + SQLite

---

## Architecture

The solution is divided into four main layers:

```
┌──────────────────────────────┐
│             API              │
│ Controllers / Requests       │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│         Application          │
│ Commands / Queries /         │
│ Validation / Behaviors       │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│            Domain            │
│     Entities + Enums         │
└──────────────────────────────┘
               ▲
               │
┌──────────────┴───────────────┐
│       Infrastructure         │
│ EF Core / SQLite / Data      │
└──────────────────────────────┘
```

### Project Structure

```
FinFlow/
├── FinFlow.API/
│   ├── Controllers/
│   ├── Exceptions/
│   ├── Requests/
│   └── Program.cs
│
├── FinFlow.Application/
│   ├── Behaviors/
│   ├── Common/
│   │   └── Interfaces/
│   ├── Customers/
│   │   └── Commands/
│   └── Loans/
│       ├── Commands/
│       │   ├── CreateLoanApplication/
│       │   ├── ApproveLoan/
│       │   ├── RejectLoan/
│       │   └── PayInstallment/
│       ├── Queries/
│       │   ├── GetAllLoans/
│       │   └── GetLoanById/
│       └── DTOs/
│
├── FinFlow.Domain/
│   └── Entities/
│       ├── Customers/
│       └── Loans/
│
└── FinFlow.Infrastructure/
    └── Persistence/
        └── FinFlowDbContext.cs
```

---

## CQRS + MediatR

All write operations are **Commands** and all read operations are **Queries**.

The API controllers never call the database or business logic directly.  
They only send commands/queries through MediatR:

```csharp
var loanId = await mediator.Send(command);
```

### Commands
- `CreateCustomerCommand`
- `CreateLoanApplicationCommand`
- `ApproveLoanCommand`
- `RejectLoanCommand`
- `PayInstallmentCommand`

### Queries
- `GetAllLoansQuery`
- `GetLoanByIdQuery`

---

## Validation

Validation is handled automatically using **FluentValidation** + a MediatR `ValidationBehavior`.

Validation runs **before** the handler is executed.

---

## Exception Handling

The API uses a global exception handler with `ProblemDetails`:

- `ValidationException` → 400
- `KeyNotFoundException` → 404
- `InvalidOperationException` → 400
- Other exceptions → 500

---

## API Endpoints

### Customers
| Method | Endpoint              | Description        |
|--------|-----------------------|--------------------|
| POST   | /api/customers        | Create a customer  |

### Loans
| Method | Endpoint                          | Description              |
|--------|-----------------------------------|--------------------------|
| POST   | /api/loans                        | Create loan application  |
| GET    | /api/loans                        | Get all loans            |
| GET    | /api/loans/{id}                   | Get loan by ID           |
| PUT    | /api/loans/{id}/approve           | Approve a loan           |
| PUT    | /api/loans/{id}/reject            | Reject a loan            |
| PUT    | /api/loans/installments/{id}/pay  | Pay an installment       |

---

## Technologies

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- MediatR
- FluentValidation
- ProblemDetails
- Clean Architecture + CQRS

---

## Getting Started

### Prerequisites
- .NET 10 SDK

### Run the project

```bash
dotnet restore
dotnet run --project FinFlow.API
```

The API will start and Swagger will be available at:

```
https://localhost:xxxx/swagger
```

---

## Future Improvements

- Authentication & Authorization
- Pagination & Filtering
- Unit & Integration Tests
- Soft Delete
- Logging
- Docker support
- CI/CD pipeline


