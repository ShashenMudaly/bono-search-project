export type LanguageDetection = {
  language: string
  score: number
}

export type TranslationResult = {
  translatedText: string
  sourceLanguage: string
  targetLanguage: string
}

export class LanguageService {
  private readonly apiUrl: string;

  constructor() {
    this.apiUrl = process.env.NEXT_PUBLIC_TRANSLATOR_API_URL || '';
  }

  async detectLanguage(text: string): Promise<LanguageDetection | null> {
    const url = `${this.apiUrl}/api/Translator/detect`;
    console.log('Detecting language URL:', url);
    try {
      const response = await fetch(
        url,
        {
          method: 'POST',
          headers: {
            'Accept': 'text/plain',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(text)
        }
      );

      if (!response.ok) {
        console.error('Language detection failed:', {
          status: response.status,
          statusText: response.statusText,
          url: response.url
        });
        throw new Error(`Language detection failed: ${response.status} ${response.statusText}`);
      }

      return await response.json();
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'Unknown error occurred';
      console.error('Language detection error:', {
        message: errorMessage,
        url,
      });
      return null;
    }
  }

  async translateText(text: string, sourceLanguage: string, targetLanguage: string): Promise<TranslationResult | null> {
    const url = `${this.apiUrl}/api/Translator/translate?targetLanguage=${targetLanguage}&sourceLanguage=${sourceLanguage}`;
    try {
      const response = await fetch(
        url,
        {
          method: 'POST',
          headers: {
            'Accept': 'text/plain',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(text)
        }
      );

      if (!response.ok) {
        console.error('Translation failed:', {
          status: response.status,
          statusText: response.statusText,
          url: response.url
        });
        throw new Error(`Translation failed: ${response.status} ${response.statusText}`);
      }

      return await response.json();
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'Unknown error occurred';
      console.error('Translation error:', {
        message: errorMessage,
        url,
      });
      return null;
    }
  }
} 