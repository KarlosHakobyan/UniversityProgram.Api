# 🎓 University Program Management System

A .NET-based University Program Management System developed to explore backend development, layered architecture, database management, and different application approaches within the .NET ecosystem.

The repository contains a collection of related projects demonstrating the development of Web APIs, business logic, data access layers, authentication, and user interfaces using C# and .NET.

## 📌 Project Overview

The solution is organized into multiple independent projects, each focusing on a particular application layer or development concept.

The main architecture separates responsibilities into:

- **API Layer** – Handles HTTP requests and exposes application endpoints.
- **Business Logic Layer (BLL)** – Contains application services and business rules.
- **Data Layer** – Handles database operations and data access.
- **Domain Layer** – Contains core application models and domain entities.
- **Presentation Layer** – Provides web interfaces using Blazor and MVC.

Additional projects demonstrate authentication, database-first development, Mediator patterns, testing, and other .NET concepts.

## 🏗️ Solution Architecture

```text
UniversityProgram.Api/
│
├── UniversityProgram.Api/
│   └── Web API
│
├── UniversityProgram.BLL/
│   └── Business Logic Layer
│
├── UniversityProgram.BLL.Tests/
│   └── Business Logic Tests
│
├── UniversityProgram.Data/
│   └── Data Access Layer
│
├── UniversityProgram.Domain/
│   └── Domain Models
│
├── UniversityProgram.LocalData/
│   └── Local Data Management
│
├── UniversityProgram.Blazor/
│   └── Blazor Application
│
├── UniversityProgram.Mvc/
│   └── MVC Application
│
├── UniversityProgram.Console/
│   └── Console Application
│
├── UniversityProgramBlazor.Test/
│   └── Blazor Tests
│
├── AuthApi/
├── AuthTest.Api/
├── NewAuthTest.Api/
├── NewApi/
├── Backend/
├── BankApi/
├── DbFirst.Api/
├── BlazorWASM/
├── StudentApiForMediator/
│
├── UniversityProgram.Api.sln
└── LICENSE.txt
```

## 🧩 Main Components

### 1. UniversityProgram.Api

The main Web API project.

Responsibilities:
- Exposing HTTP endpoints.
- Handling incoming API requests.
- Connecting the presentation layer with business logic.
- Providing an API entry point for client applications.

### 2. UniversityProgram.BLL

Business Logic Layer.

Responsibilities:
- Application business rules.
- Service implementations.
- Processing application operations.
- Separating business logic from data access.

### 3. UniversityProgram.Data

Data access layer responsible for database-related operations.

### 4. UniversityProgram.Domain

Contains the core domain models and application entities.

### 5. UniversityProgram.LocalData

A separate project for working with local data.

### 6. UniversityProgram.Blazor

Blazor-based user interface project.

### 7. UniversityProgram.Mvc

MVC-based web application.

### 8. UniversityProgram.Console

Console application for working with the solution's functionality.

## 🧪 Additional Projects

The repository also includes several projects dedicated to experimenting with different .NET development techniques.

| Project | Purpose |
|---|---|
| `AuthApi` | Authentication API |
| `AuthTest.Api` | Authentication experiments and testing |
| `NewAuthTest.Api` | Additional authentication implementation |
| `NewApi` | Separate API development |
| `Backend` | Backend development experiments |
| `BankApi` | Banking API project |
| `DbFirst.Api` | Database-First approach |
| `BlazorWASM` | Blazor WebAssembly |
| `StudentApiForMediator` | Student API and Mediator pattern |
| `UniversityProgram.BLL.Tests` | Business logic testing |
| `UniversityProgramBlazor.Test` | Blazor application testing |

## 🛠️ Technologies

- **C#** – Primary programming language.
- **.NET** – Application development platform.
- **ASP.NET Core Web API** – REST API development.
- **Blazor** – Interactive web applications.
- **ASP.NET Core MVC** – MVC-based web development.
- **SQL / Database Technologies** – Data persistence.
- **Visual Studio** – Development environment.

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/KarlosHakobyan/UniversityProgram.Api.git
```

Navigate to the repository:

```bash
cd UniversityProgram.Api
```

### 2. Open the Solution

Open the solution file:

```text
UniversityProgram.Api.sln
```

Use Microsoft Visual Studio to load the solution and its projects.

### 3. Restore Dependencies

Using the .NET CLI:

```bash
dotnet restore UniversityProgram.Api.sln
```

### 4. Build the Solution

```bash
dotnet build UniversityProgram.Api.sln
```

### 5. Run the Main API

```bash
dotnet run --project UniversityProgram.Api
```

The actual database connection settings and application configuration should be configured before running projects that require external data sources.

## 🧪 Running Tests

Run the Business Logic tests:

```bash
dotnet test UniversityProgram.BLL.Tests
```

Run all tests in the solution:

```bash
dotnet test UniversityProgram.Api.sln
```

## 🎯 Learning Objectives

This repository serves as a practical environment for developing and improving .NET development skills.

Key learning areas:

- C# and object-oriented programming.
- ASP.NET Core Web API development.
- Layered application architecture.
- Separation of concerns.
- Business logic implementation.
- Data access and database integration.
- Authentication and authorization.
- Blazor and MVC development.
- Unit testing.
- Mediator pattern.
- Database-First development.
- Working with multiple projects in a Visual Studio solution.

## 📄 License

This project is distributed under the MIT License. See [LICENSE.txt](LICENSE.txt) for details.

## 👨‍💻 Author

**Karlos Hakobyan**

GitHub: [@KarlosHakobyan](https://github.com/KarlosHakobyan)

---

⭐ If you find this repository useful, feel free to explore the projects and follow their development.
