using Microsoft.AspNetCore.Mvc;
using BonoSearch.Interfaces;
using BonoSearch.Models.DTOs;

namespace BonoSearch.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet("semantic")]
    public async Task<IActionResult> SemanticSearch([FromQuery] string query)
    {
        var results = await _searchService.SemanticSearchAsync(query);
        return Ok(results);
    }

    [HttpGet("lexical")]
    public async Task<IActionResult> LexicalSearch([FromQuery] string query)
    {
        var results = await _searchService.LexicalSearchAsync(query);
        return Ok(results);
    }


    [HttpGet("hybrid")]
    public async Task<IActionResult> HybridSearch([FromQuery] string query)
    {
        var results = await _searchService.HybridSearchAsync(query);
        return Ok(results);
    }
} 