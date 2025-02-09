# BONO (Bayesian Optimized Neural Output) Movie Search

A prototype application demonstrating different search methodologies for movie discovery using BONO - Bayesian Optimized Neural Output. This application showcases the differences between lexical, semantic, and hybrid search approaches.

## Search Methodologies

### 1. Lexical Search
- Traditional keyword-based search enhanced with Bayesian optimization
- Optimized for precise text matching in movie titles and descriptions
- Best for finding movies when you know specific words or titles

### 2. Semantic Search
- Neural network-powered semantic understanding
- Uses Bayesian optimization to improve meaning-based search results
- Capable of understanding thematic elements and contextual relationships
- Useful for finding movies based on themes, concepts, or similar plot elements

### 3. Hybrid Search
- Combines both lexical and semantic approaches
- Uses Bayesian optimization to determine optimal weighting between approaches
- Provides balanced results leveraging both exact matches and semantic understanding
- Attempts to get the best of both worlds

## Technical Implementation

- Built with Next.js/React and TypeScript
- Modern web development practices
- RESTful API integration
- Responsive design using Tailwind CSS
- Client-side state management using React hooks

## Features

- Clean, intuitive search interface
- Real-time search type switching
- Results display including:
  - Movie titles
  - Truncated plot summaries in list view
  - Full plot descriptions in modal view
- Error handling and loading states
- Responsive and interactive UI elements

## Use Cases

This prototype is particularly useful for:
- Demonstrating the strengths and weaknesses of different search approaches
- Educational purposes in information retrieval
- Comparing search result quality across different methodologies
- Understanding how semantic search can enhance traditional keyword-based search

## Getting Started

First, run the development server:

```bash
npm run dev
# or
yarn dev
# or
pnpm dev
# or
bun dev
```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the result.

You can start editing the page by modifying `app/page.tsx`. The page auto-updates as you edit the file.

This project uses [`next/font`](https://nextjs.org/docs/app/building-your-application/optimizing/fonts) to automatically optimize and load [Geist](https://vercel.com/font), a new font family for Vercel.

## Learn More

To learn more about Next.js, take a look at the following resources:

- [Next.js Documentation](https://nextjs.org/docs) - learn about Next.js features and API.
- [Learn Next.js](https://nextjs.org/learn) - an interactive Next.js tutorial.

You can check out [the Next.js GitHub repository](https://github.com/vercel/next.js) - your feedback and contributions are welcome!

## Deploy on Vercel

The easiest way to deploy your Next.js app is to use the [Vercel Platform](https://vercel.com/new?utm_medium=default-template&filter=next.js&utm_source=create-next-app&utm_campaign=create-next-app-readme) from the creators of Next.js.

Check out our [Next.js deployment documentation](https://nextjs.org/docs/app/building-your-application/deploying) for more details.
