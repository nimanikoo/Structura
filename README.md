# Structura

**Structura** is a sample back-end project implemented with **.NET 8**, demonstrating **Clean Architecture** principles along with **CQRS** and **MediatR** for command/query separation. It uses **Entity Framework Core** for data access and supports both **InMemory** and **PostgreSQL** databases. The project is designed to be modular, scalable, and easily reusable for future projects.

---

## Features

* **Clean Architecture**
  Clear separation of concerns across layers:

  * **Domain**: Core entities and business rules
  * **Application**: Commands, Queries, Validators, and Pipeline Behaviors
  * **Infrastructure**: Repositories, DbContext, external services
  * **API**: Web API endpoints

* **CQRS + MediatR**
  Command and Query responsibilities are fully separated. Handlers, Notifications, and Pipeline Behaviors (e.g., Logging, Validation) are implemented using **MediatR**.

* **Validation & Logging Pipeline**
  Requests are automatically validated using **FluentValidation** before reaching handlers. Logging behavior captures request and response flow for easier debugging.

* **Flexible Database Configuration**

  * **InMemory Database** for development and testing
  * **PostgreSQL** for production use  
    Configuration is environment-aware and automatically switches between databases.

* **Unit Testing Ready**
  Comprehensive **unit tests** are included for:

  * Handlers
  * Pipeline Behaviors
  * Repositories using InMemory database

* **Extensible & Reusable**
  Designed to allow easy addition of new features, modules, or external integrations without violating Clean Architecture principles.

---

## Getting Started

### Prerequisites

* .NET 8 SDK
* PostgreSQL (optional for production)

### Running the Project

1. Clone the repository:

```bash
git clone git@github.com:nimanikoo/Structura.git
cd Structura
```

2. Configure the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=structura;Username=postgres;Password=postgres"
  }
}
```

3. Run the application:

```bash
dotnet run --project src/Structura.Api
```

4. The API will be available at `https://localhost:5182` (or `http://localhost:5182` for non-HTTPS).  
   Swagger UI is enabled in Development mode at `/swagger/index.html`.

## 🐳 Docker & Running in Container

You can run **Structura API** inside a Docker container with PostgreSQL or InMemory database.

### 🚀 Build Docker Image

From the root of the project (where `Dockerfile` is located):

```bash
docker build -t structura-api .
````

### 🏃‍♂️ Run the Container

```bash
docker run -d -p 5182:8080 -e ASPNETCORE_ENVIRONMENT=Development --name structura-api structura-api
```

* `-p 5182:8080` maps the container port 8080 to your local port 5182.
* `-e ASPNETCORE_ENVIRONMENT=Development` ensures Swagger is enabled.

### 🌐 Access Swagger UI

After the container starts, open your browser:

```
http://localhost:5182/swagger/index.html
```
   

---

## Project Structure

```
Structura/
│
├─ src/
│  ├─ Structura.Api/              # ASP.NET Core Web API
│  ├─ Structura.Application/      # Application layer: Commands, Queries, Validators, Behaviors
│  ├─ Structura.Domain/           # Domain entities and business rules
│  └─ Structura.Infrastructure/   # Repositories, DbContext, database configuration
│
└─ tests/
   └─ Structura.Tests/            # Unit tests for Handlers, Behaviors, Repositories
```

---

## Key Patterns

* **Pipeline Behaviors**: Middleware-like handlers for cross-cutting concerns (Logging, Validation)
* **CQRS**: Separation of read and write operations
* **Repository Pattern**: Abstraction over EF Core DbContext for cleaner data access
* **Unit Testing**: InMemory EF Core used for isolated, fast, reliable tests

---

## Contributing

This project is designed to be reused and extended. Contributions are welcome via pull requests. Please follow Clean Architecture principles and write tests for new features.

---

## License

This project is licensed under the MIT License.
