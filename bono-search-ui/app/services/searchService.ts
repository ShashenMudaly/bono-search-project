export type MovieSearchResult = {
  name: string
  plot: string
}

export class SearchService {
  private readonly apiUrl: string;

  constructor() {
    this.apiUrl = process.env.NEXT_PUBLIC_SEARCH_API_URL || '';
  }

  async searchMovies(query: string, searchType: string): Promise<MovieSearchResult[]> {
    try {
      const response = await fetch(
        `${this.apiUrl}/api/Search/${searchType}?query=${encodeURIComponent(query)}`,
        {
          method: 'GET',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          }
        }
      );
      
      if (!response.ok) {
        console.error('Search failed:', {
          status: response.status,
          statusText: response.statusText,
          url: response.url
        });
        throw new Error(`Search failed: ${response.status}`);
      }
      
      return await response.json();
    } catch (error: unknown) {
      const errorMessage = error instanceof Error ? error.message : 'Unknown error occurred';
      console.error('Search error:', {
        message: errorMessage,
        url: this.apiUrl,
      });
      return [];
    }
  }
} 