# FinFlow - Mini Loan Management System

## Overview
A simple Loan Management System built with Clean Architecture principles.

### Features
- Create Customers
- Create Loan Applications
- Approve / Reject Loans
- Auto-generate Installments on Approval
- Pay Installments (with PaidAt date)
- Validation using FluentValidation

### Tech Stack
- ASP.NET Core Web API
- Entity Framework Core + SQLite
- FluentValidation
- Clean Architecture (Domain, Application, Infrastructure, API)

### Current Architecture
Controller → Service → Repository → DbContext
