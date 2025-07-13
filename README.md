# Portfolio Performance API

This API allows users to manage investment portfolios, track asset transactions, and retrieve portfolio performance metrics.

## Features

* 📁 Create, update, retrieve, and delete portfolios
* 📈 Add and update assets within a portfolio
* 📉 Record buy/sell transactions for assets
* 📊 Retrieve performance metrics of a portfolio over time

---

## 🔧 Setup Instructions

### Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* [Postman](https://www.postman.com/downloads/) (for API testing)

### Run the Application

```bash
dotnet restore
dotnet build
dotnet run
```

The API will be hosted at: `https://localhost:7069/`.

---

## 📢 API Endpoints

| Resource    | Method | Endpoint                    | Description                   |
| ----------- | ------ | --------------------------- | ----------------------------- |
| Portfolio   | POST   | `/api/portfolio`            | Create a new portfolio        |
| Portfolio   | GET    | `/api/portfolio/{id}`       | Get portfolio by Id           |
| Portfolio   | PATCH  | `/api/portfolio/{id}`       | Update an existing portfolio  |
| Performance | GET    | `/api/portfolio_perfomance` | Get portfolio performance     |
| Asset       | POST   | `/api/asset`                | Create a new asset            |
| Asset       | PATCH  | `/api/asset/{id}`           | Update an asset               |
| Asset       | DELETE | `/api/asset/{id}`           | Delete an asset               |
| Transaction | POST   | `/api/transaction`          | Create a buy/sell transaction |

---

## 📦 Postman Collection

1. Import the file: `Postman Scripts.json` into Postman.
2. Set the environment variable `{{baseUrl}}` to match your running API base URL:

   * Example: `https://localhost:7069/`
3. Ensure all required request parameters and bodies are filled correctly.

---

## 📜 Swagger Documentation

Swagger UI is available for API exploration and testing:

```
https://localhost:7069/swagger
```

This includes:

* Endpoint descriptions
* Parameter details
* Schema definitions
* Try-it-out functionality

---

## 🔬 Running Tests

If unit tests are implemented:

```bash
dotnet test
```

---

## 📂 Folder Structure

* `Features/` - Organized by domain (Portfolio, Asset, Transaction)
* `DTO/` - Data Transfer Objects for request/response
* `Validators/` - FluentValidation rules
* `Infrastructure/` - Contracts, filters, extensions
* `Data/` - Repository interfaces and entity definitions

