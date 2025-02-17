/** @type {import('next').NextConfig} */
const nextConfig = {
  env: {
    NEXT_PUBLIC_SEARCH_API_URL: process.env.NEXT_PUBLIC_SEARCH_API_URL || 'http://localhost:5261',
    NEXT_PUBLIC_TRANSLATOR_API_URL: process.env.NEXT_PUBLIC_TRANSLATOR_API_URL || 'http://localhost:5131'
  }
}

module.exports = nextConfig 