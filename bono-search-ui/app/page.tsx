import Image from 'next/image'
import { SearchForm } from '@/app/components/SearchForm'

export default function Home() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-center p-24">
      <div className="w-full max-w-2xl">
        <div className="mb-8 text-center">
          <Image
            src="/BonoSearchLogo.webp?height=80&width=200"
            alt="BONO Logo"
            width={200}
            height={80}
            className="mx-auto"
          />
          <h1 className="mt-4 flex flex-col items-center">
            <div className="text-2xl font-bold">
              <span className="text-blue-700">BONO</span>
            </div>
            <div className="text-sm text-gray-600 mt-1">
              Bayesian Optimised Neural Output
            </div>
          </h1>
        </div>
        <SearchForm />
      </div>
    </main>
  )
}