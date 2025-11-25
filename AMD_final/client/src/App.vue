<template>
  <div id="app">
    <AppHeader />

    <main style="min-height: calc(100vh - 140px); padding: 20px;">
      <router-view />
    </main>

    <AppFooter />
  </div>
</template>

<script>
import AppHeader from './components/AppHeader.vue'
import AppFooter from './components/AppFooter.vue'

export default {
  components: {
    AppHeader,
    AppFooter
  }
}
</script>
