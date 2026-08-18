# Banking System API 

A secure Online Banking System REST API built using ASP.NET Core Web API.  
Supports role-based access (Admin, Employee, Customer) with both Identity and JWT Authentication.
This System supports the each registered customer to Create/Edit/Delete multiple Accounts (Some Operations Requires Admin/Employee Approval), Each Account will have a Transactions/Loans Logs, Transactions have a Type(Deposit, Withdraw, Transfer, Loan), Transactions also shows a Status if its Commited or Aborted or still Pending,
Each Loan Payment from/to the account will be logged as a transaction (The type may differ with each payment).

---

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Identity
- JWT Authentication
- Serilog Logging
- Swagger
- AWS Deployment

---

## Features

- Secure Authentication with both JWT and Identity : Each User have a profile and for Customers, each profile will have multiple Accounts that he can Manage.
- Role-Based Authorization : Each Role can do a specific jobs that the system allows to do.
- Accounts Management: Create, update, delete, and view accounts with unique account numbers and balances.
- Transactions: Support for deposits, withdrawals, transfers, and Loans(Monthly Reduction, and the Loan Deposit) with concurrency control(using RowVersion).
- Loan System: Apply, approve, and track personal or business loans with automatic interest calculation.
- Logging: Logs every operation that is done on the Database in a logging file based on the Day.
- Search & Filtering: Search accounts and transactions with filters like date ranges, transaction type, and amount.
- Pagination
- Repository Pattern Implementation
- Deployed on AWS EC2 machine using Nginx server and applied SSL certificate to it to make it Secure (HTTPS). 

---

## Test the System

Here is a Live Demo to test the System Features and see how the System acts with different inputs and with corner cases:

https://virtualbankproject12.duckdns.org/swagger/index.html

## Setup Instructions

1. Clone the repository
2. Update `appsettings.json`:
   - Set your SQL Server connection string
   - Change JWT SigningKey
3. Apply Migrations:
   dotnet ef database update
4. Run the application:
   dotnet run
5. Open Swagger:
   https://localhost:xxxx/swagger

---

## Roles

- Admin: Full system control
- Employee: Manage Approvements
- Customer: Manage his Accounts, Transaction history, and Loans

## Next Steps

- Implementing Unit Tests for Some Critical Functions
- Adding Background Services for updating the Loan Remaining Amount
- Implementing Integration Test for the Project