# EF Core Migrations

## Prerequisites
- .NET 7 SDK installed
- Database server running (SQL Server or PostgreSQL)
- EF Core Tools: `dotnet tool install --global dotnet-ef`

## Setup Environment

### PostgreSQL (default)
```powershell
$env:DATABASE_CONNECTION = "Host=localhost;Port=5432;Database=pix_db;User Id=postgres;Password=postgres;"
```

### SQL Server
```powershell
$env:DATABASE_CONNECTION = "Server=localhost;Database=PixDb;Trusted_Connection=True;TrustServerCertificate=True"
$env:USE_SQL_SERVER = "true"
```

## Commands (run from solution root)

### Create Initial Migration
```powershell
dotnet ef migrations add InitialCreate --project "src/building blocks/Pix.Microservices.Infrastructure" --startup-project "src/services/Pix.Users.Api"
```

### Apply Migrations
```powershell
dotnet ef database update --project "src/building blocks/Pix.Microservices.Infrastructure" --startup-project "src/services/Pix.Users.Api"
```

### Remove Last Migration
```powershell
dotnet ef migrations remove --project "src/building blocks/Pix.Microservices.Infrastructure" --startup-project "src/services/Pix.Users.Api"
```

### List Migrations
```powershell
dotnet ef migrations list --project "src/building blocks/Pix.Microservices.Infrastructure" --startup-project "src/services/Pix.Users.Api"
```
