import type { LanguageCode } from '@/shared/utils/languages'

export interface GreetingResponse {
  id: string
  target: string
  message: string
  language: string
  isFormal: boolean
  formattedGreeting: string
  createdAt: string
}

export interface CreateGreetingRequest {
  target: string
  message: string
  language: LanguageCode
  isFormal: boolean
}

export interface TranslateGreetingRequest {
  targetLanguage: LanguageCode
}
