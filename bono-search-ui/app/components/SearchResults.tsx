'use client';
import { useState } from 'react';
import { Modal } from '@/app/components/Modal';  

export type SearchResult = {
    title: string
    description: string
    shortDescription: string
  }
  
  type SearchResultsProps = {
    results: SearchResult[]
  }
  
  export function SearchResults({ results }: SearchResultsProps) {
    const [selectedResult, setSelectedResult] = useState<SearchResult | null>(null);
  
    if (results.length === 0) return null
  
    return (
      <>
        <div className="mt-8 space-y-6">
          <h2 className="text-xl font-semibold mb-4">Search Results</h2>
          {results.map((result, index) => (
            <div 
              key={index} 
              className="bg-white shadow-md rounded-lg p-4 cursor-pointer hover:shadow-lg transition-shadow"
              onClick={() => setSelectedResult(result)}
            >
              <h3 className="text-lg font-medium mb-2">{result.title}</h3>
              <p className="text-gray-600">{result.shortDescription}</p>
            </div>
          ))}
        </div>
  
        <Modal 
          isOpen={selectedResult !== null}
          onClose={() => setSelectedResult(null)}
          title={selectedResult?.title}
        >
          {selectedResult && (
            <p className="text-gray-700 whitespace-pre-wrap">{selectedResult.description}</p>
          )}
        </Modal>
      </>
    )
  }