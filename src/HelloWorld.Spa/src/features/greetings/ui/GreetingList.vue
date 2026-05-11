<template>
  <div v-if="isLoading" class="flex justify-center p-8">
    <ProgressSpinner />
  </div>

  <div v-else-if="isError" class="text-red-500 p-4">
    Failed to load greetings. Please try again.
  </div>

  <div v-else-if="greetings?.length === 0" class="text-center p-8 text-surface-500">
    No greetings yet. Create your first one!
  </div>

  <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
    <GreetingCard
      v-for="greeting in greetings"
      :key="greeting.id"
      :greeting="greeting"
      @formalize="$emit('formalize', $event)"
      @casualize="$emit('casualize', $event)"
      @translate="$emit('translate', $event)"
    />
  </div>
</template>

<script setup lang="ts">
import ProgressSpinner from 'primevue/progressspinner'
import GreetingCard from './GreetingCard.vue'
import type { GreetingResponse } from '../model/types'

defineProps<{
  greetings: GreetingResponse[] | undefined
  isLoading: boolean
  isError: boolean
}>()

defineEmits<{
  formalize: [id: string]
  casualize: [id: string]
  translate: [id: string]
}>()
</script>
