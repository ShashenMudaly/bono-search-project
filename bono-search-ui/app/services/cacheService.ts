export class CacheService {
  private apiUrl: string;

  constructor() {
    this.apiUrl = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5121';
  }

  async getTranslatedPlot(movieName: string, languageCode: string): Promise<string | null> {
    try {
      console.log(`Checking cache for movie: ${movieName}, language: ${languageCode}`);
      const url = `${this.apiUrl}/api/TranslationCache/get-movie-plot?movieName=${encodeURIComponent(movieName)}&languageCode=${languageCode}`;
      const response = await fetch(url, {
        headers: {
          'accept': '*/*',
          'mode': 'cors',
          'Access-Control-Allow-Origin': '*'
        }
      });

      if (response.status === 204 || !response.ok) {
        console.log(`Cache miss for ${movieName}`);
        return null;
      }
      
      const data = await response.json();
      console.log('Cache hit for', movieName);
      return data.translation;
    } catch (error) {
      console.error('Cache service error:', error);
      return null;
    }
  }

  async saveTranslatedPlot(movieName: string, languageCode: string, translatedPlot: string): Promise<boolean> {
    try {
      console.log(`Saving translation to cache for movie: ${movieName}, language: ${languageCode}`);
      const url = `${this.apiUrl}/api/TranslationCache/save-movie-plot`;
      const response = await fetch(url, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'accept': '*/*',
          'mode': 'cors',
          'Access-Control-Allow-Origin': '*'
        },
        body: JSON.stringify({
          movieName,
          languageCode,
          translatedPlot
        })
      });

      console.log(`Save ${response.ok ? 'successful' : 'failed'} for ${movieName}`);
      return response.ok;
    } catch (error) {
      console.error('Cache service error:', error);
      return false;
    }
  }
} 