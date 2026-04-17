import axios from 'axios'
import { setupInterceptors } from './interceptors'

export const httpClient = axios.create({
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json',
  },
})

setupInterceptors(httpClient)
