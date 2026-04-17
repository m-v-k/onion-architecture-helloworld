import type { AxiosInstance, AxiosError } from 'axios'

export function setupInterceptors(client: AxiosInstance): void {
  client.interceptors.response.use(
    (response) => response,
    (error: AxiosError) => {
      console.error('[API Error]', error.response?.status, error.response?.data)
      return Promise.reject(error)
    },
  )
}
