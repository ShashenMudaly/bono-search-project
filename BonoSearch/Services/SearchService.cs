using BonoSearch.Interfaces;
using BonoSearch.Models.DTOs;   

namespace BonoSearch.Services;

public class SearchService : ISearchService
{
    private readonly IMovieRepository _movieRepository;

    public SearchService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<MovieDto>> SemanticSearchAsync(string query)
    {
        // Add any business logic here if needed
        return await _movieRepository.SemanticSearch(query);
    }

    public async Task<IEnumerable<MovieDto>> LexicalSearchAsync(string query)
    {
        return await _movieRepository.LexicalSearch(query);
    }

    public async Task<IEnumerable<MovieDto>> HybridSearchAsync(string query)
    {
        return await _movieRepository.HybridSearch(query);
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