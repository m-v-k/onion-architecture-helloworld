<template>
  <Card class="border border-surface-200 dark:border-surface-700">
    <template #title>
      <div class="flex items-center justify-between">
        <span class="text-lg font-semibold">{{ greeting.formattedGreeting }}</span>
        <BaseTag
          :value="greeting.isFormal ? 'Formal' : 'Casual'"
          :severity="greeting.isFormal ? 'info' : 'success'"
        />
      </div>
    </template>
    <template #content>
      <div class="flex flex-col gap-2 text-sm">
        <p><span class="font-medium">Target:</span> {{ greeting.target }}</p>
        <p><span class="font-medium">Message:</span> {{ greeting.message }}</p>
        <p><span class="font-medium">Language:</span> {{ greeting.language }}</p>
        <p><span class="font-medium">Created:</span> {{ formattedDate }}</p>
      </div>
    </template>
    <template #footer>
      <div class="flex gap-2 flex-wrap">
        <BaseButton
          v-if="!greeting.isFormal"
          label="Make Formal"
          severity="info"
          size="small"
          outlined
          @click="$emit('formalize', greeting.id)"
        />
        <BaseButton
          v-if="greeting.isFormal"
          label="Make Casual"
          severity="success"
          size="small"
          outlined
          @click="$emit('casualize', greeting.id)"
        />
        <BaseButton
          label="Translate"
          severity="secondary"
          size="small"
          outlined
          @click="$emit('translate', greeting.id)"
        />
      </div>
    </template>
  </Card>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import Card from 'primevue/card'
import { BaseButton, BaseTag } from '@/shared/ui'
import type { GreetingResponse } from '../model/types'

const props = defineProps<{
  greeting: GreetingResponse
}>()

defineEmits<{
  formalize: [id: string]
  casualize: [id: string]
  translate: [id: string]
}>()

const formattedDate = computed(() =>
  new Date(props.greeting.createdAt).toLocaleString(),
)
</script>
