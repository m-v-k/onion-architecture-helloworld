import { httpClient } from '@/core/api'
import type {
  GreetingResponse,
  CreateGreetingRequest,
  TranslateGreetingRequest,
} from '../models'

const BASE = '/greetings'

export async function getAllGreetings(): Promise<GreetingResponse[]> {
  const { data } = await httpClient.get<GreetingResponse[]>(BASE)
  return data
}

export async function getGreetingById(
  id: string,
): Promise<GreetingResponse> {
  const { data } = await httpClient.get<GreetingResponse>(`${BASE}/${id}`)
  return data
}

export async function createGreeting(
  request: CreateGreetingRequest,
): Promise<GreetingResponse> {
  const { data } = await httpClient.post<GreetingResponse>(BASE, request)
  return data
}

export async function formalizeGreeting(
  id: string,
): Promise<GreetingResponse> {
  const { data } = await httpClient.put<GreetingResponse>(
    `${BASE}/${id}/formalize`,
  )
  return data
}

export async function casualizeGreeting(
  id: string,
): Promise<GreetingResponse> {
  const { data } = await httpClient.put<GreetingResponse>(
    `${BASE}/${id}/casualize`,
  )
  return data
}

export async function translateGreeting(
  id: string,
  request: TranslateGreetingRequest,
): Promise<GreetingResponse> {
  const { data } = await httpClient.post<GreetingResponse>(
    `${BASE}/${id}/translate`,
    request,
  )
  return data
}
