import type { CreateGreetingRequest } from '../types'

const MAX_TARGET_LENGTH = 100
const MAX_MESSAGE_LENGTH = 250

export function validateCreateGreeting(
  request: CreateGreetingRequest,
): string[] {
  const errors: string[] = []

  if (!request.target.trim()) {
    errors.push('Target name is required.')
  } else if (request.target.length > MAX_TARGET_LENGTH) {
    errors.push(`Target name must be at most ${MAX_TARGET_LENGTH} characters.`)
  }

  if (!request.message.trim()) {
    errors.push('Message is required.')
  } else if (request.message.length > MAX_MESSAGE_LENGTH) {
    errors.push(`Message must be at most ${MAX_MESSAGE_LENGTH} characters.`)
  }

  if (!request.language) {
    errors.push('Language is required.')
  }

  return errors
}
