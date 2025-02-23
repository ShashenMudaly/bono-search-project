using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using Microsoft.Extensions.Logging;

namespace FloydTranslationCacheServiceNameSpace
{
    public class FloydTranslationCacheService : IFloydTranslationCacheService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<FloydTranslationCacheService> _logger;
        private const string KeyPrefix = "movie_translation:";

        public FloydTranslationCacheService(IConnectionMultiplexer redis, ILogger<FloydTranslationCacheService> logger)
        {
            _redis = redis;
            _logger = logger;
        }

        public async Task SaveTranslation(string movieName, string languageCode, string translatedPlot)
        {
            _logger.LogInformation("Starting SaveTranslation for movie: {MovieName}, language: {LanguageCode}", 
                movieName, languageCode);
            var startTime = DateTime.UtcNow;

            var db = _redis.GetDatabase();
            string key = GenerateKey(movieName, languageCode);
            
            // This will automatically overwrite if the key exists
            await db.StringSetAsync(key, translatedPlot);

            var duration = DateTime.UtcNow - startTime;
            _logger.LogInformation("Completed SaveTranslation in {Duration}ms", duration.TotalMilliseconds);
        }

        public async Task<string?> GetTranslation(string movieName, string languageCode)
        {
            _logger.LogInformation("Starting GetTranslation for movie: {MovieName}, language: {LanguageCode}", 
                movieName, languageCode);
            var startTime = DateTime.UtcNow;

            var db = _redis.GetDatabase();
            string key = GenerateKey(movieName, languageCode);
            
            var translation = await db.StringGetAsync(key);

            var duration = DateTime.UtcNow - startTime;
            _logger.LogInformation("Completed GetTranslation in {Duration}ms", duration.TotalMilliseconds);
            
            return translation.HasValue ? translation.ToString() : null;
        }

        private static string GenerateKey(string movieName, string languageCode)
        {
            return $"{KeyPrefix}{movieName.ToLower()}:{languageCode.ToLower()}";
        }
    }
} 