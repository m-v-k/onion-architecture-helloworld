<template>
  <DataTable v-bind="$attrs">
    <template v-for="(_, slot) in $slots" #[slot]="slotProps">
      <slot :name="slot" v-bind="slotProps ?? {}" />
    </template>
  </DataTable>
</template>

<script setup lang="ts">
import DataTable from 'primevue/datatable'
</script>
