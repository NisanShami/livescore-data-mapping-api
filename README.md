# LSports Data Mapping API

This API provides endpoints for managing period mappings between different data providers and LSports internal period definitions.

## Features

- **Period Mappings Management**: Create, read, update, and delete period mappings
- **Mapped/Unmapped Views**: Separate endpoints for mapped and unmapped periods
- **Filtering & Pagination**: Support for filtering by sport, provider, and search text
- **RESTful API**: Standard REST endpoints with proper HTTP status codes

## Project Structure

```
src/
├── LSports.DataMapping.Abstractions/     # Interfaces, Models, DTOs
├── LSports.DataMapping.Services/         # Business Logic, Repository, DbContext
└── LSports.DataMapping.WebApi/           # Controllers, Program.cs, Configuration
```

## Prerequisites

- .NET 9.0 SDK
- MySQL Server
- Visual Studio 2022 or VS Code

## Getting Started

### 1. Database Setup

Create a MySQL database:
```sql
CREATE DATABASE livescore_data_mapping;
```

### 2. Configuration

Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=livescore_data_mapping;Uid=your_user;Pwd=your_password;"
  }
}
```

### 3. Build and Run

```bash
cd src/LSports.DataMapping.WebApi
dotnet restore
dotnet build
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001` (in development)

## API Endpoints

### Period Mappings

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/period-mapping/mapped/get` | Get mapped periods with pagination and filtering |
| POST | `/period-mapping/not-mapped/get` | Get unmapped periods |
| POST | `/period-mapping/mapped` | Create a new period mapping |
| PUT | `/period-mapping/mapped/{id}` | Update an existing period mapping |
| DELETE | `/period-mapping/mapped/{id}` | Delete a period mapping |
| POST | `/period-mapping/filters` | Get available filters (sports, providers) |

### Example Requests

#### Get Mapped Periods
```json
POST /period-mapping/mapped/get
{
  "page": 1,
  "pageSize": 50,
  "sportIds": [1, 2],
  "providerIds": [1],
  "searchText": "half",
  "sortField": "sportName",
  "sortDirection": "asc"
}
```

#### Create Period Mapping
```json
POST /period-mapping/mapped
{
  "providerId": 1,
  "providerName": "Bet365",
  "sportId": 1,
  "sportName": "Football",
  "providerPeriod": "First Half",
  "lsportsPeriodId": 1,
  "lsportsPeriodName": "First Half",
  "updatedBy": "admin"
}
```

## Database Schema

### period_mappings Table

| Column | Type | Description |
|--------|------|-------------|
| id | INT (PK) | Primary key |
| provider_id | INT | Provider identifier |
| provider_name | VARCHAR(255) | Provider name |
| sport_id | INT | Sport identifier |
| sport_name | VARCHAR(255) | Sport name |
| provider_period | VARCHAR(255) | Period name from provider |
| lsports_period_id | INT (NULL) | LSports period ID (NULL for unmapped) |
| lsports_period_name | VARCHAR(255) | LSports period name |
| is_active | BOOLEAN | Soft delete flag |
| created_date | DATETIME | Creation timestamp |
| updated_date | DATETIME | Last update timestamp |
| updated_by | VARCHAR(255) | User who made the last update |

## Development

### Adding New Features

1. Add models to `LSports.DataMapping.Abstractions/Models/`
2. Add interfaces to `LSports.DataMapping.Abstractions/Interfaces/`
3. Implement services in `LSports.DataMapping.Services/`
4. Add controllers in `LSports.DataMapping.WebApi/Controllers/`

### Testing

The API includes Swagger UI for interactive testing in development mode.

## Logging

The application uses Serilog for structured logging:
- Console output for development
- File logging to `logs/` directory
- Configurable log levels via `appsettings.json`

## CORS

CORS is configured to allow all origins in development. Update the CORS policy for production use.
# livescore-data-mapping-api
