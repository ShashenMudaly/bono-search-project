using Microsoft.EntityFrameworkCore;
using Npgsql;
using BonoSearch.Interfaces;
using BonoSearch.Models;
using BonoSearch.Models.DTOs;
using BonoSearch.Data;


namespace BonoSearch.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _context;
    private const string LANGUAGE = "english";
    private const string EMBEDDING_MODEL = "text-embedding-ada-002";


    public MovieRepository(MovieDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovieDto>> SemanticSearch(string query)
    {
        var parameters = new NpgsqlParameter[]
        {
            new("query", query),
            new("embedding_model", EMBEDDING_MODEL)
        };

        var movies = await _context.Set<Movie>()
            .FromSqlRaw(@"
                SELECT m.id AS Id, 
                       m.name AS Name, 
                       m.plot AS Plot
                FROM movies m
                ORDER BY m.plot_vector <=> azure_openai.create_embeddings(@embedding_model, @query)::vector
                LIMIT 5", parameters)
            .ToListAsync();

        return movies.Select(m => new MovieDto 
        { 
            Id = m.Id, 
            Name = m.Name, 
            Plot = m.Plot 
        });
    }

    public async Task<IEnumerable<MovieDto>> LexicalSearch(string query)
    {
        var parameters = new NpgsqlParameter[]
        {
            new("query", query),
            new("language", LANGUAGE)
        };

        var movies = await _context.Set<Movie>()
            .FromSqlRaw(@"
                SELECT m.id AS Id, 
                       m.name AS Name, 
                       m.plot AS Plot
                FROM movies m
                WHERE to_tsvector('english', m.plot) @@ websearch_to_tsquery('english', @query)
                ORDER BY ts_rank(to_tsvector('english', m.plot), websearch_to_tsquery('english', @query)) DESC
                LIMIT 5", parameters)
            .ToListAsync();

        return movies.Select(m => new MovieDto 
        { 
            Id = m.Id, 
            Name = m.Name, 
            Plot = m.Plot 
        });
    }

    public async Task<IEnumerable<MovieDto>> HybridSearch(string query)
    {
        var parameters = new NpgsqlParameter[]
        {
            new("query", query),
            new("language", LANGUAGE),
            new("embedding_model", EMBEDDING_MODEL)
        };

        var movies =  await _context.Set<Movie>()
            .FromSqlRaw(@"
                WITH semantic_results AS (
                    SELECT m.id AS Id, 
                           m.name AS Name, 
                           m.plot AS Plot,
                           1 - (m.plot_vector <=> azure_openai.create_embeddings(@embedding_model, @query)::vector) as semantic_score
                    FROM movies m
                ),
                fulltext_results AS (
                    SELECT m.id AS Id, 
                           m.name AS Name, 
                           m.plot AS Plot,
                           ts_rank(to_tsvector('english', m.plot), websearch_to_tsquery('english', @query)) as text_score
                    FROM movies m
                    WHERE to_tsvector('english', m.plot) @@ websearch_to_tsquery('english', @query)
                )
                SELECT DISTINCT sr.Id, 
                              sr.Name, 
                              sr.Plot, 
                              (COALESCE(fr.text_score, 0) + sr.semantic_score) as score
                FROM semantic_results sr
                LEFT JOIN fulltext_results fr ON sr.Id = fr.Id
                ORDER BY (COALESCE(fr.text_score, 0) + sr.semantic_score) DESC
                LIMIT 5", parameters)
            .ToListAsync();

        return movies.Select(m => new MovieDto 
        { 
            Id = m.Id, 
            Name = m.Name, 
            Plot = m.Plot 
        });
    }

    public async Task<MovieDto?> GetMovieByName(string movieName)
    {
        var parameters = new NpgsqlParameter[]
        {
            new("movie_name", movieName),
            new("language", LANGUAGE)
        };

        var movie = await _context.Set<Movie>()
            .FromSqlRaw(@"
                SELECT m.id as ""Id"", 
                       m.name as ""Name"", 
                       m.plot as ""Plot""
                FROM movies m
                WHERE to_tsvector('english', m.name) @@ websearch_to_tsquery('english', @movie_name)
                ORDER BY ts_rank(to_tsvector('english', m.name), websearch_to_tsquery('english', @movie_name))  DESC,  m.name ASC
                LIMIT 1", parameters)
            .FirstOrDefaultAsync();

        if (movie == null) return null;

        return new MovieDto 
        { 
            Id = movie.Id, 
            Name = movie.Name, 
            Plot = movie.Plot 
        };
    }
} 