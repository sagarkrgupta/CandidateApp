# CandidateApp
This web application is designed to help businesses store and manage information about job candidates. The application will provide an API (Application Programming Interface), which allows authorized users (like HR managers) to add, update, and retrieve details about candidates through a web request.


# CandidateApp API

This project is the web API for **CandidateApp**, built using **ASP.NET Core** targeting .NET 8.0. It includes integration with **Entity Framework Core** for database access and **Swagger** for API documentation.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Running the API](#running-the-api)
- [Database Setup](#database-setup)
- [Swagger Documentation](#swagger-documentation)
- [Dependencies](#dependencies)
- [License](#license)

## Prerequisites

Before you can run this project, make sure you have the following installed on your machine:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A suitable IDE such as [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- A database provider (e.g., SQL Server) for **Entity Framework Core** to interact with

## Getting Started

1. **Clone the repository:**

   ```bash
   git clone https://github.com/sagarkrgupta/CandidateApp.git
   cd CandidateApp.API
   ```

2. **Restore the NuGet packages:**

   ```bash   
   dotnet restore
   ```


3. **Build the project:**

   ```bash   
   dotnet build
   ```

   
3. **Running the API:**

   ```bash   
   dotnet run
   ```

# Database Setup

This API uses Entity Framework Core for data access. If you're using migrations to create and update the database schema, follow these steps:

1. **Add a migration:**

   ```bash
   dotnet ef migrations add InitialCreate --project /path/to/your/dataaccess/library --startup-project /path/to/your/api/project   
   ```

2. **Apply the migration to update the database:**

   ```bash   
   dotnet ef database update --project /path/to/your/dataaccess/library --startup-project /path/to/your/api/project
   ```
**Note: Run API project and then it ensures database is created and migrated automatically**
   
# Swagger Documentation

The API includes Swagger to provide interactive API documentation. Once the application is running, you can access the Swagger UI at the following URL:
`https://localhost:5001/swagger for HTTPS`



   
# Dependencies

This project relies on the following NuGet packages:

* Microsoft.EntityFrameworkCore.Design (Version 8.0.10): EF Core tools for migrations and design-time support.
* Swashbuckle.AspNetCore (Version 7.0.0): Provides Swagger support for documenting the API.

Additionally, the API references the CandidateApp.Business project to access business logic for handling candidates.



### Key Sections in the `README.md`:
1. **Prerequisites**: Lists the tools and software you need installed to run the project (e.g., .NET SDK, IDE).
2. **Getting Started**: A step-by-step guide to clone the repo, restore packages, and build the project.
3. **Running the API**: Instructions for running the API either through the command line or Visual Studio.
4. **Database Setup**: Provides guidance for setting up the database using Entity Framework Core migrations / running by application.
5. **Swagger Documentation**: How to access the interactive API documentation provided by Swagger.
6. **Dependencies**: Lists the key NuGet packages and references in the project.
7. **License**: Specifies the licensing information for the project.

This should give anyone reviewing or contributing to your project a good overview of the steps to get started, run the API, and interact with the database.
