import type { LanguageCode } from '@/shared/utils/languages'


export interface CreateGreetingRequest {
  target: string
  message: string
  language: LanguageCode
  isFormal: boolean
}
