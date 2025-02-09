using BonoSearch.Models.DTOs;

namespace BonoSearch.Interfaces;

public interface IMovieRepository
{
    Task<IEnumerable<MovieDto>> SemanticSearch(string query);
    Task<IEnumerable<MovieDto>> LexicalSearch(string query);
    Task<IEnumerable<MovieDto>> HybridSearch(string query);
} 