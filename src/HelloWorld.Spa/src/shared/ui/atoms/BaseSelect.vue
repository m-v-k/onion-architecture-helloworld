<template>
  <Select v-bind="$attrs">
    <template v-for="(_, slot) in $slots" #[slot]="slotProps">
      <slot :name="slot" v-bind="slotProps ?? {}" />
    </template>
  </Select>
</template>

<script setup lang="ts">
import Select from 'primevue/select'
</script>
