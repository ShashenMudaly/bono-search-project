# BonoSearch

A .NET Core Web API that provides intelligent movie search capabilities using PostgreSQL with vector embeddings. The API supports semantic search (using Azure OpenAI embeddings), lexical search (using PostgreSQL full-text search), and a hybrid approach combining both methods.

## Features

- **Semantic Search**: Utilizes Azure OpenAI's text-embedding-ada-002 model to find movies based on semantic meaning
- **Lexical Search**: Full-text search using PostgreSQL's tsquery/tsvector capabilities
- **Hybrid Search**: Combines vector similarity and text matching for optimal results

## Project Structure 
BonoSearch/
├── Data/
│ └── MovieDbContext.cs # EF Core DB Context
├── Models/
│ ├── Movie.cs # Movie entity
│ └── DTOs/
│ └── MovieDto.cs # Data Transfer Object
└── Repositories/
└── MovieRepository.cs # Data access layer
└── Services/
└── MovieService.cs # Business logic layer

## Prerequisites

- .NET 8.0
- PostgreSQL 14+ with pgvector extension
- Azure OpenAI API access

## Running the Application

1. Clone the repository
```bash
git clone [repository-url]
```

2. Navigate to the project directory
```bash
cd BonoSearch
```

3. Restore dependencies
```bash
dotnet restore
```

4. Run the application
```bash
dotnet run
```

## Technologies Used

- .NET 8.0
- Entity Framework Core
- PostgreSQL with pgvector
- Azure OpenAI (text-embedding-ada-002 model)

## License

[MIT License](LICENSE)

## API Endpoints

### Search Endpoints

The API provides several search endpoints to find movies:

#### Semantic Search
```http
GET /api/search/semantic?query={search_term}
```
Performs semantic search using embeddings to find movies based on meaning rather than exact matches.

#### Lexical Search
```http
GET /api/search/lexical?query={search_term}
```
Performs traditional text-based search using PostgreSQL full-text search capabilities.

#### Hybrid Search
```http
GET /api/search/hybrid?query={search_term}
```
Combines both semantic and lexical search approaches for more comprehensive results.

#### Movie Lookup
```http
GET /api/search/movie?name={movie_name}
```
Looks up a specific movie by name using PostgreSQL's websearch_to_tsquery for flexible text matching.

### Response Format

All search endpoints return movie data in the following format:
```json
{
    "id": "string",
    "name": "string",
    "plot": "string"
}
```

### Error Responses

- `400 Bad Request`: When the search query or movie name is empty
- `404 Not Found`: When no movie is found (movie lookup endpoint only)
- `500 Internal Server Error`: When an unexpected error occurs