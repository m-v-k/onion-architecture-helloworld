import { useQuery, useMutation, useQueryClient } from '@tanstack/vue-query'
import {
  getAllGreetings,
  getGreetingById,
  createGreeting,
  formalizeGreeting,
  casualizeGreeting,
  translateGreeting,
} from '../data-access'
import type { CreateGreetingRequest, TranslateGreetingRequest } from './index'
import type { Ref } from 'vue'

const GREETINGS_KEY = ['greetings'] as const

export function useGreetingsQuery() {
  return useQuery({
    queryKey: GREETINGS_KEY,
    queryFn: getAllGreetings,
  })
}

export function useGreetingByIdQuery(id: Ref<string>) {
  return useQuery({
    queryKey: [...GREETINGS_KEY, id],
    queryFn: () => getGreetingById(id.value),
    enabled: () => !!id.value,
  })
}

export function useCreateGreetingMutation() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: (request: CreateGreetingRequest) => createGreeting(request),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: GREETINGS_KEY }),
  })
}

export function useFormalizeGreetingMutation() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: (id: string) => formalizeGreeting(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: GREETINGS_KEY }),
  })
}

export function useCasualizeGreetingMutation() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: (id: string) => casualizeGreeting(id),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: GREETINGS_KEY }),
  })
}

export function useTranslateGreetingMutation() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: ({ id, request }: { id: string; request: TranslateGreetingRequest }) =>
      translateGreeting(id, request),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: GREETINGS_KEY }),
  })
}
