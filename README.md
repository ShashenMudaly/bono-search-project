# BONO (Bayesian Optimized Neural Output) Movie Search

A prototype application demonstrating different search methodologies for movie discovery using BONO - Bayesian Optimized Neural Output. This application showcases the differences between lexical, semantic, and hybrid search approaches.

## Search Methodologies

### 1. Lexical Search
- Traditional keyword-based search enhanced with Bayesian optimization
- Optimized for precise text matching in movie titles and descriptions
- Best for finding movies when you know specific words or titles

### 2. Semantic Search
- Neural network-powered semantic understanding
- Uses Bayesian optimization to improve meaning-based search results
- Capable of understanding thematic elements and contextual relationships
- Useful for finding movies based on themes, concepts, or similar plot elements

### 3. Hybrid Search
- Combines both lexical and semantic approaches
- Uses Bayesian optimization to determine optimal weighting between approaches
- Provides balanced results leveraging both exact matches and semantic understanding
- Attempts to get the best of both worlds

## Technical Implementation

- Built with Next.js/React and TypeScript
- Modern web development practices
- RESTful API integration
- Responsive design using Tailwind CSS
- Client-side state management using React hooks

## Features

- Clean, intuitive search interface
- Real-time search type switching
- Results display including:
  - Movie titles
  - Truncated plot summaries in list view
  - Full plot descriptions in modal view
- Error handling and loading states
- Responsive and interactive UI elements

## Use Cases

This prototype is particularly useful for:
- Demonstrating the strengths and weaknesses of different search approaches
- Educational purposes in information retrieval
- Comparing search result quality across different methodologies
- Understanding how semantic search can enhance traditional keyword-based search

## Bono Search API

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

## Prerequisites

- .NET 8.0
- PostgreSQL 14+ with pgvector extension
- Azure OpenAI API access
