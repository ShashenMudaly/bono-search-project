using Microsoft.AspNetCore.Mvc;
using BonoSearch.Interfaces;
using BonoSearch.Models.DTOs;
using Microsoft.Extensions.Logging;

namespace BonoSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;
    private readonly ILogger<SearchController> _logger;

    public SearchController(ISearchService searchService, ILogger<SearchController> logger)
    {
        _searchService = searchService;
        _logger = logger;
    }

    [HttpGet("semantic")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> SemanticSearch([FromQuery] string query)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty");
            }

            var results = await _searchService.SemanticSearchAsync(query);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing semantic search with query: {Query}", query);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("lexical")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> LexicalSearch([FromQuery] string query)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty");
            }

            var results = await _searchService.LexicalSearchAsync(query);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing lexical search with query: {Query}", query);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("hybrid")]
    public async Task<ActionResult<IEnumerable<MovieDto>>> HybridSearch([FromQuery] string query)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty");
            }

            var results = await _searchService.HybridSearchAsync(query);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing hybrid search with query: {Query}", query);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
} 