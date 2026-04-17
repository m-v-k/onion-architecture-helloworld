<template>
  <Dialog
    v-model:visible="visible"
    modal
    header="Translate Greeting"
    class="w-full max-w-md"
  >
    <div class="flex flex-col gap-4">
      <div class="flex flex-col gap-1">
        <label for="targetLanguage" class="text-sm font-medium">Target Language</label>
        <BaseSelect
          id="targetLanguage"
          v-model="selectedLanguage"
          :options="SUPPORTED_LANGUAGES"
          option-label="name"
          option-value="code"
          placeholder="Select target language"
        />
      </div>

      <BaseButton
        label="Translate"
        :loading="isPending"
        @click="onTranslate"
      />
    </div>
  </Dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import Dialog from 'primevue/dialog'
import { BaseButton, BaseSelect } from '@/shared/ui'
import { SUPPORTED_LANGUAGES, type LanguageCode } from '@/shared/utils/languages'

const visible = defineModel<boolean>('visible', { required: true })

const props = defineProps<{
  greetingId: string | null
  isPending: boolean
}>()

const emit = defineEmits<{
  translate: [payload: { id: string; targetLanguage: LanguageCode }]
}>()

const selectedLanguage = ref<LanguageCode>('nl')

function onTranslate() {
  if (props.greetingId) {
    emit('translate', {
      id: props.greetingId,
      targetLanguage: selectedLanguage.value,
    })
  }
}
</script>
