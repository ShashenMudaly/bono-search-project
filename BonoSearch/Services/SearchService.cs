using BonoSearch.Interfaces;
using BonoSearch.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace BonoSearch.Services;


public class SearchService : ISearchService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<SearchService> _logger;

    public SearchService(IMovieRepository movieRepository, ILogger<SearchService> logger)
    {
        _movieRepository = movieRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<MovieDto>> SemanticSearchAsync(string query)
    {
        try
        {
            _logger.LogInformation("Performing semantic search with query: {Query}", query);
            
            var results = await _movieRepository.SemanticSearch(query);
            
            if (!results.Any())
            {
                _logger.LogInformation("No results found for semantic search query: {Query}", query);
            }
            
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to perform semantic search with query: {Query}", query);
            throw new ApplicationException("Failed to perform semantic search", ex);
        }
    }

    public async Task<IEnumerable<MovieDto>> LexicalSearchAsync(string query)
    {
        try
        {
            _logger.LogInformation("Performing lexical search with query: {Query}", query);
            
            var results = await _movieRepository.LexicalSearch(query);
            
            if (!results.Any())
            {
                _logger.LogInformation("No results found for lexical search query: {Query}", query);
            }
            
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to perform lexical search with query: {Query}", query);
            throw new ApplicationException("Failed to perform lexical search", ex);
        }
    }

    public async Task<IEnumerable<MovieDto>> HybridSearchAsync(string query)
    {
        try
        {
            _logger.LogInformation("Performing hybrid search with query: {Query}", query);
            
            var results = await _movieRepository.HybridSearch(query);
            
            if (!results.Any())
            {
                _logger.LogInformation("No results found for hybrid search query: {Query}", query);
            }
            
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to perform hybrid search with query: {Query}", query);
            throw new ApplicationException("Failed to perform hybrid search", ex);
        }
    }
}

// Add a comparer to handle deduplication
public class MovieDtoComparer : IEqualityComparer<MovieDto>
{
    public bool Equals(MovieDto? x, MovieDto? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null || y is null) return false;
        return x.Name == y.Name && x.Plot == y.Plot;
    }

    public int GetHashCode(MovieDto obj)
    {
        if (obj is null) return 0;
        return HashCode.Combine(obj.Name, obj.Plot);
    }

} 