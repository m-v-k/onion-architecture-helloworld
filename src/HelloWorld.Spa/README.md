# Vue 3 + TypeScript + Vite

This template should help get you started developing with Vue 3 and TypeScript in Vite. The template uses Vue 3 `<script setup>` SFCs, check out the [script setup docs](https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup) to learn more.

Learn more about the recommended Project Setup and IDE Support in the [Vue Docs TypeScript Guide](https://vuejs.org/guide/typescript/overview.html#project-setup).

## Key architecture decisions implemented

- Server state → Vue Query (queries + mutations with automatic cache invalidation)
- Global app state → Pinia (registered, ready for auth/settings stores)
- Local state → component-level ref/reactive
- API layer → isolated in features/greetings/api/, components never call APIs directly — they go through composables in model/
- PrimeVue wrapped in shared/ui/atoms/ following Atomic Design
- Vite proxy configured so /api/* forwards to the backend at http://localhost:5000
- All 6 backend endpoints mapped: list, get by ID, create, formalize, casualize, translate
