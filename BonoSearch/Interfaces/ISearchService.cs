using System.Collections.Generic;
using System.Threading.Tasks;
using BonoSearch.Models.DTOs;

namespace BonoSearch.Interfaces;

public interface ISearchService
{
    Task<IEnumerable<MovieDto>> SemanticSearchAsync(string query);
    Task<IEnumerable<MovieDto>> LexicalSearchAsync(string query);
    Task<IEnumerable<MovieDto>> HybridSearchAsync(string query);
    Task<MovieDto?> GetMovieByNameAsync(string movieName);
} 