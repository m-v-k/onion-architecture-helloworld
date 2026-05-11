<template>
  <div class="max-w-5xl mx-auto p-6 flex flex-col gap-8">
    <header>
      <h1 class="text-3xl font-bold">Hello World Greetings</h1>
      <p class="text-surface-500">Manage your greetings via the API</p>
    </header>

    <section>
      <h2 class="text-xl font-semibold mb-4">Create a Greeting</h2>
      <CreateGreetingForm
        :is-pending="createMutation.isPending.value"
        @submit="onCreate"
      />
    </section>

    <section>
      <h2 class="text-xl font-semibold mb-4">All Greetings</h2>
      <GreetingList
        :greetings="greetingsQuery.data.value"
        :is-loading="greetingsQuery.isLoading.value"
        :is-error="greetingsQuery.isError.value"
        @formalize="formalizeMutation.mutate($event)"
        @casualize="casualizeMutation.mutate($event)"
        @translate="openTranslateDialog"
      />
    </section>

    <TranslateDialog
      v-model:visible="translateDialogVisible"
      :greeting-id="translateTargetId"
      :is-pending="translateMutation.isPending.value"
      @translate="onTranslate"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import CreateGreetingForm from './CreateGreetingForm.vue'
import GreetingList from './GreetingList.vue'
import TranslateDialog from './TranslateDialog.vue'
import {
  useGreetingsQuery,
  useCreateGreetingMutation,
  useFormalizeGreetingMutation,
  useCasualizeGreetingMutation,
  useTranslateGreetingMutation,
} from '../model'
import type { CreateGreetingRequest } from '../model/types'
import type { LanguageCode } from '@/shared/utils/languages'

const greetingsQuery = useGreetingsQuery()
const createMutation = useCreateGreetingMutation()
const formalizeMutation = useFormalizeGreetingMutation()
const casualizeMutation = useCasualizeGreetingMutation()
const translateMutation = useTranslateGreetingMutation()

const translateDialogVisible = ref(false)
const translateTargetId = ref<string | null>(null)

function onCreate(request: CreateGreetingRequest) {
  createMutation.mutate(request)
}

function openTranslateDialog(id: string) {
  translateTargetId.value = id
  translateDialogVisible.value = true
}

function onTranslate(payload: { id: string; targetLanguage: LanguageCode }) {
  translateMutation.mutate(
    { id: payload.id, request: { targetLanguage: payload.targetLanguage } },
    {
      onSuccess: () => {
        translateDialogVisible.value = false
      },
    },
  )
}
</script>
