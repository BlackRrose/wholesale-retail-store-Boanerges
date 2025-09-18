# Wholesale-Retail-Store-<yourname>

**online store supporting both wholesale and retail customers**

## Summary
This repository contains the source and documentation for an online store that serves both retail and wholesale customers. The backend is intended to be built with C# (.NET) and MS SQL Server. The frontend will be implemented in Angular.

## Goals
- Provide product browsing, cart, and checkout for retail customers.
- Provide bulk-order workflows, tiered pricing, and minimum order quantities for wholesale customers.
- Clean, testable backend code; clear SQL schema; maintainable Angular frontend.
- Demonstrate Git best practices (feature branches, descriptive commits, PR reviews).

- --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

# 🏬 Wholesale & Retail Store Demo

A full-stack demo application that supports:
- Product catalog*
- Customer and order creation
- Checkout flow with validation & stock decrement
- Pricing quotes with applied rules/discounts
- Angular frontend + ASP.NET Core Web API backend

---

## 🚀 Tech Stack
- **Frontend:** Angular 17, ngx-toastr
- **Backend:** ASP.NET Core 8 Web API, EF Core
- **Database:** SQL Server LocalDB / SQLite (configurable)
- **Tooling:** Swagger for API docs


## 📦 Setup Instructions
### Backend (API)
1. Navigate to backend folder:
   ```bash
   cd backend
2.Restore dependencies:
  dotnet restore
  
3. Update database (EF migrations):
  dotnet ef database update

4. Update database (EF migrations):
   dotnet ef database update

   Default URL: https://localhost:5001/api OR https://localhost:44325/api/
   Swagger UI: https://localhost:7190 OR http://localhost:5022


### Frontend (Angular)

1. Navigate to frontend folder:
cd frontend

2. Install dependencies:
npm install

3. Start Angular dev server:
ng serve


Default UI URL: http://localhost:4200/



🔗 API Usage
Swagger

Swagger UI is enabled automatically.
Start the backend and open:
👉 http://localhost:5022

Example Endpoints
GET /api/products/getProducts → list products
POST /api/Customers/addCustomer → creates customer
POST /api/orders → create order (decrements stock)
POST /api/orders/quote → get price quote with applied discounts



🏗️ Architecture Notes

Backend:
ASP.NET Core 8 Web API
Repository pattern for data access
Business logic in Services layer
XML doc comments + inline explanations for maintainability

Frontend:
Angular standalone modules (Products, Checkout)
Service-based state management (CartService, CheckoutService)
ngx-toastr for user notifications (e.g., “Product added to cart”)
Discounts: Applied at checkout (backend), sent in API response, displayed in UI summary
Validation: Backend ensures order stock levels are respected
