# FinFlow — Loan Management System

A full-stack Loan Management System built with **ASP.NET Core** (Clean Architecture + CQRS) and **React**.

---

## Features

### Backend
- Create Customers
- Create Loan Applications
- Approve / Reject Loans
- Auto-generate Installments on Approval
- Pay Installments
- FluentValidation + MediatR Pipeline
- Global Exception Handling (ProblemDetails)
- Entity Framework Core + SQLite

### Frontend (React)
- Dashboard with real-time statistics
- Customers list + Create form + Search
- Loans list with Approve / Reject actions
- Installments view + Pay action
- Customer name displayed in Loans table

---

## Architecture

```
┌────────────────────┐
│   React Client     │
│  (FinFlow.Client)  │
└─────────┬──────────┘
          │ HTTP
          ▼
┌────────────────────┐
│       API          │
│   Controllers      │
└─────────┬──────────┘
          │
          ▼
┌────────────────────┐
│    Application     │
│ Commands / Queries │
│   + Validation     │
└─────────┬──────────┘
          │
          ▼
┌────────────────────┐
│      Domain        │
│     Entities       │
└────────────────────┘
          ▲
          │
┌─────────┴──────────┐
│   Infrastructure   │
│  EF Core + SQLite  │
└────────────────────┘
```

---

## Project Structure

```
FinFlow/
├── FinFlow.API/               → Web API + Controllers
├── FinFlow.Application/       → CQRS (Commands, Queries, Behaviors)
├── FinFlow.Domain/            → Entities
├── FinFlow.Infrastructure/    → EF Core + DbContext
└── FinFlow.Client/            → React Frontend (Vite)
```

---

## Technologies

### Backend
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- MediatR
- FluentValidation
- Clean Architecture + CQRS

### Frontend
- React + TypeScript
- Vite
- React Router
- Fetch API

---

## Getting Started

### 1. Backend

```bash
# Restore & run
dotnet restore
dotnet run --project FinFlow.API
```

API will run on: `http://localhost:5155`  
Swagger: `http://localhost:5155/swagger`

### 2. Frontend

```bash
cd FinFlow.Client
npm install
npm run dev
```

Frontend will run on: `http://localhost:5173`

> Make sure the `API_URL` in `src/api/client.ts` matches your backend port.

---

## API Endpoints

### Customers
| Method | Endpoint            | Description          |
|--------|---------------------|----------------------|
| GET    | /api/customers      | Get all customers    |
| POST   | /api/customers      | Create a customer    |

### Loans
| Method | Endpoint                            | Description              |
|--------|-------------------------------------|--------------------------|
| GET    | /api/loans                          | Get all loans            |
| GET    | /api/loans/{id}                     | Get loan by ID           |
| POST   | /api/loans                          | Create loan application  |
| PUT    | /api/loans/{id}/approve             | Approve a loan           |
| PUT    | /api/loans/{id}/reject              | Reject a loan            |
| PUT    | /api/loans/installments/{id}/pay    | Pay an installment       |

---

## How to Use

1. Create a **Customer**
2. Copy the Customer ID
3. Create a **Loan Application** using that ID
4. **Approve** the loan → Installments are generated automatically
5. Click **Show Installments** → Mark them as paid

---

## Future Improvements

- Authentication & Authorization
- Dropdown to select Customer when creating a Loan
- Pagination & Filtering
- Unit & Integration Tests
- Docker support
- Better UI (Tailwind / component library)

---

## Author

**Mena H. Elghaysha**
