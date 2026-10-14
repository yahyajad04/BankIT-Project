# Banking Management System

A full-stack Banking Management System designed to provide a simple and centralized platform for managing customer accounts, profiles, loans, and financial transactions.

The system is built with **ASP.NET Core** for the backend, **React.js** for the frontend, and **Microsoft SQL Server** for data storage and management.

for more insight on the project watch the Demo:
https://youtu.be/wD__G8JwgBs
## 🏦 Overview

The Banking Management System provides a role-based environment where users can manage their banking information and perform common banking operations through a web-based interface.

The system is organized around four main modules:

- **Accounts**
- **Customer Profiles**
- **Loans**
- **Transactions**

The application follows a client-server architecture, where the React.js frontend communicates with the ASP.NET Core Web API to retrieve and modify data stored in the SQL Server database.

---

## 🛠️ Technologies Used

### Backend

- ASP.NET Core Web API
- C#
- Entity Framework Core
- ASP.NET Core Identity
- JWT Authentication
- Repository Pattern
- RESTful APIs
- Serilog

### Frontend

- React.js
- JavaScript
- React Router
- Bootstrap
- CSS
- Vite

### Database

- Microsoft SQL Server
- SQL Server Management Studio (SSMS)
- Entity Framework Core

### Development Tools

- Visual Studio
- Visual Studio Code
- Git & GitHub
- Swagger / OpenAPI

---

## 📦 Main Modules

### 1. Accounts

The Accounts module allows customers to manage their bank accounts and view important account information.

Main functionality includes:

- Create bank accounts
- View account details
- Check account balances
- Deposit money
- Withdraw money
- Transfer money
- View account-related transactions
- Manage different account types

---

### 2. Customer Profiles

The Customer Profiles module manages information associated with customers.

Main functionality includes:

- View customer profile
- Update personal information
- Manage profile information
- View accounts associated with the customer
- Access customer-related banking information

---

### 3. Loans

The Loans module provides functionality for managing customer loans.

Main functionality includes:

- Apply for loans
- View loan applications
- Track loan status
- View loan details
- View monthly repayment amounts
- Track remaining loan balance
- Manage loan repayments

---

### 4. Transactions

The Transactions module is responsible for recording and displaying financial activity.

Supported operations include:

- Deposits
- Withdrawals
- Account-to-account transfers
- Transaction history
- Transaction details
- Tracking transaction dates and amounts

---

## 🔐 Authentication & Authorization

The system uses **JWT (JSON Web Tokens)** for authentication and role-based authorization.

Users are authenticated before accessing protected resources, and access to specific functionality is determined by the user's role.

The system supports different user roles, including:

- **Customer**
- **Employee**
- **Admin**

Each role has access to different parts of the system according to its responsibilities.
