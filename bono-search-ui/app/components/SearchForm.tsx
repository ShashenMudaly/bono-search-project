'use client'

import { useState } from 'react'
import { SearchResults, SearchResult } from '@/app/components/SearchResults'

type SearchType = {
  id: string
  name: string
}

const searchTypes: SearchType[] = [
  { id: 'lexical', name: 'Lexical' },
  { id: 'semantic', name: 'Semantic' },
  { id: 'hybrid', name: 'Hybrid' },
]

export function SearchForm() {
  const [query, setQuery] = useState('')
  const [selectedType, setSelectedType] = useState<SearchType>(searchTypes[0])
  const [searchResults, setSearchResults] = useState<SearchResult[]>([])

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSearchResults([]);
    
    try {
      const response = await fetch(
        `${process.env.NEXT_PUBLIC_API_URL}/api/Search/${selectedType.id}?query=${encodeURIComponent(query)}`,
        {
          method: 'GET',
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          }
        }
      );
      
      if (!response.ok) {
        throw new Error(`Search failed: ${response.status}`);
      }
      
      const data: Array<{ name: string; plot: string }> = await response.json();
      setSearchResults(data.map(item => ({
        title: item.name,
        description: item.plot,
        shortDescription: item.plot.length > 1000 
          ? `${item.plot.substring(0, 1000)}...` 
          : item.plot
      })));
    } catch (error) {
      console.error('Search error:', error);
      setSearchResults([]);
    }
  }

  return (
    <>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div className="flex space-x-2">
          <input
            type="text"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            placeholder="Enter your search query"
            className="flex-grow px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          <button
            type="submit"
            className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
          >
            Search
          </button>
        </div>
        <div className="flex justify-center space-x-4">
          {searchTypes.map((type) => (
            <label
              key={type.id}
              className={`relative flex items-center cursor-pointer rounded-lg px-5 py-4 shadow-md focus:outline-none ${
                selectedType.id === type.id
                  ? 'bg-sky-900 bg-opacity-75 text-white'
                  : 'bg-white text-gray-900'
              }`}
            >
              <input
                type="radio"
                value={type.id}
                checked={selectedType.id === type.id}
                onChange={() => setSelectedType(type)}
                className="sr-only"
              />
              <span className="flex items-center">
                <span className="w-5 h-5 mr-2 flex items-center justify-center">
                  {selectedType.id === type.id && (
                    <svg className="h-4 w-4" viewBox="0 0 20 20" fill="currentColor">
                      <path
                        fillRule="evenodd"
                        d="M16.707 5.293a1 1 0 010 1.414l-8 8a1 1 0 01-1.414 0l-4-4a1 1 0 011.414-1.414L8 12.586l7.293-7.293a1 1 0 011.414 0z"
                        clipRule="evenodd"
                      />
                    </svg>
                  )}
                </span>
                <span className="text-sm font-medium">{type.name}</span>
              </span>
            </label>
          ))}
        </div>
      </form>
      <SearchResults results={searchResults} />
    </>
  )
}