public interface IFloydTranslationCacheService
{
    Task SaveTranslation(string movieName, string languageCode, string translatedPlot);
    Task<string?> GetTranslation(string movieName, string languageCode);
} 