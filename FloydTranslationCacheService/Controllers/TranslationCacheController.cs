using Microsoft.AspNetCore.Mvc;
using FloydTranslationCacheServiceNameSpace;

namespace FloydTranslationCacheServiceNameSpace
{
    /// <summary>
    /// Controller for managing movie plot translations cache
    /// </summary>
    [ApiController]
    [Route("api/TranslationCache")]
    public class TranslationCacheController : ControllerBase
    {
        private readonly IFloydTranslationCacheService _cacheService;

        public TranslationCacheController(IFloydTranslationCacheService cacheService)
        {
            _cacheService = cacheService;
        }

        /// <summary>
        /// Saves a translated movie plot to the cache
        /// </summary>
        /// <param name="request">Translation details including movie name, language code, and translated plot</param>
        [HttpPost("save-movie-plot")]
        public async Task<IActionResult> SaveMoviePlotTranslation([FromBody] TranslationRequest request)
        {
            if (string.IsNullOrEmpty(request.MovieName) || 
                string.IsNullOrEmpty(request.LanguageCode) || 
                string.IsNullOrEmpty(request.TranslatedPlot))
            {
                return BadRequest("All fields are required");
            }

            await _cacheService.SaveTranslation(
                request.MovieName,
                request.LanguageCode,
                request.TranslatedPlot
            );

            return Ok();
        }

        /// <summary>
        /// Retrieves a translated movie plot from the cache
        /// </summary>
        /// <param name="movieName">Name of the movie</param>
        /// <param name="languageCode">Target language code</param>
        [HttpGet("get-movie-plot")]
        public async Task<IActionResult> GetMoviePlotTranslation([FromQuery] string movieName, [FromQuery] string languageCode)
        {
            var translation = await _cacheService.GetTranslation(movieName, languageCode);
            
            if (translation == null)
            {
                return NotFound();
            }

            return Ok(new { translation });
        }
    }
} 