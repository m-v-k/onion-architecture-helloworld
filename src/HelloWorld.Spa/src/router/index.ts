import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'greetings',
      component: () => import('@/features/greetings/ui/GreetingsPage.vue'),
    },
  ],
})

export default router
