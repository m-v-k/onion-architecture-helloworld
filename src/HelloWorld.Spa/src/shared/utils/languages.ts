export const SUPPORTED_LANGUAGES = [
  { code: 'en', name: 'English' },
  { code: 'nl', name: 'Dutch' },
  { code: 'fr', name: 'French' },
  { code: 'es', name: 'Spanish' },
  { code: 'de', name: 'German' },
] as const

export type LanguageCode = (typeof SUPPORTED_LANGUAGES)[number]['code']
