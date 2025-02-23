# Floyd Translation Cache Service

A Redis-backed caching service for movie plot translations. This service provides a simple API to store and retrieve translated movie plots with support for multiple languages.

## Features

- Cache movie plot translations using Redis
- Support for multiple languages
- RESTful API endpoints
- Performance logging with operation timing
- Swagger documentation (Development environment only)

## Prerequisites

- .NET 7.0 or later
- Redis server
- Visual Studio 2022 or VS Code

## Configuration

Add Redis connection string to `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Redis": "your-redis-connection-string"
  }
}
```

## API Endpoints

### Save Translation
```http
POST /api/TranslationCache/save-movie-plot
Content-Type: application/json

{
  "movieName": "string",
  "languageCode": "string",
  "translatedPlot": "string"
}
```

### Get Translation
```http
GET /api/TranslationCache/get-movie-plot?movieName={movieName}&languageCode={languageCode}
```

## Development

1. Ensure Redis is running
2. Configure Redis connection string in appsettings.json
3. Run the project:
```bash
dotnet run
```
4. Access Swagger UI (development only): `http://localhost:5121/swagger`

## Logging

The service includes detailed performance logging:
- Operation start/end times
- Duration measurements in milliseconds
- Movie name and language code tracking
- Redis operation status

## Error Handling

- 404 Not Found: Translation doesn't exist in cache
- 400 Bad Request: Invalid input parameters
- Detailed logging for troubleshooting

## Dependencies

- StackExchange.Redis: Redis client library
- ASP.NET Core 7.0
- Microsoft.Extensions.Logging 