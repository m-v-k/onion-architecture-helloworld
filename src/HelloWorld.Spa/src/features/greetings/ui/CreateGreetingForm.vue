<template>
  <form class="flex flex-col gap-4" @submit.prevent="onSubmit">
    <div class="flex flex-col gap-1">
      <label for="target" class="text-sm font-medium">Target</label>
      <BaseInput
        id="target"
        v-model="form.target"
        placeholder="e.g. World"
      />
    </div>

    <div class="flex flex-col gap-1">
      <label for="message" class="text-sm font-medium">Message</label>
      <BaseInput
        id="message"
        v-model="form.message"
        placeholder="e.g. Hello"
      />
    </div>

    <div class="flex flex-col gap-1">
      <label for="language" class="text-sm font-medium">Language</label>
      <BaseSelect
        id="language"
        v-model="form.language"
        :options="SUPPORTED_LANGUAGES"
        option-label="name"
        option-value="code"
        placeholder="Select a language"
      />
    </div>

    <div class="flex items-center gap-2">
      <Checkbox v-model="form.isFormal" input-id="isFormal" :binary="true" />
      <label for="isFormal" class="text-sm">Formal</label>
    </div>

    <div v-if="errors.length" class="text-red-500 text-sm">
      <p v-for="error in errors" :key="error">{{ error }}</p>
    </div>

    <BaseButton type="submit" label="Create Greeting" :loading="isPending" />
  </form>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import Checkbox from 'primevue/checkbox'
import { BaseButton, BaseInput, BaseSelect } from '@/shared/ui'
import { SUPPORTED_LANGUAGES } from '@/shared/utils/languages'
import type { CreateGreetingRequest } from '../model/types'
import { validateCreateGreeting } from '../domain/validation'

const emit = defineEmits<{
  submit: [request: CreateGreetingRequest]
}>()

defineProps<{
  isPending: boolean
}>()

const form = reactive<CreateGreetingRequest>({
  target: '',
  message: '',
  language: 'en',
  isFormal: false,
})

const errors = ref<string[]>([])

function onSubmit() {
  errors.value = validateCreateGreeting(form)
  if (errors.value.length === 0) {
    emit('submit', { ...form })
  }
}
</script>
