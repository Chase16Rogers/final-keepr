<template>
  <div @click="vaultRoute(vaultProp.id)" class="pointer">
    <div class="card bg-dark text-white" :class="state.height">
      <img class="card-img" :class="state.height" :src="vaultProp.img" :alt="vaultProp.img">
      <div class="card-img-overlay pb-2 d-flex align-items-end justify-content-between gradient">
        <h5 class="card-title mb-0">
          {{ vaultProp.name }}
        </h5>
      </div>
    </div>
  </div>
</template>
<script>
import { useRouter } from 'vue-router'
import { onMounted, reactive } from 'vue'
export default {
  name: 'VaultComponent',
  props: {
    vaultProp: {
      type: Object,
      required: true
    }
  },
  setup() {
    const router = useRouter()
    const state = reactive({
      height: ''
    })
    onMounted(() => {
    })
    return {
      state,
      vaultRoute(id) {
        router.push({ name: 'VaultPage', params: { id: id } })
      },
      randomHeights() {
        const rnd = Math.floor(Math.random() * 10)
        switch (rnd) {
          case 1:
          case 2:
            state.height = 'xsm-keep'
            return 'xsm-keep'
          case 3:
          case 4:
            state.height = 'sm-keep'
            return 'sm-keep'
          case 5:
          case 6:
            state.height = 'md-keep'
            return 'md-keep'
          case 7:
          case 8:
            state.height = 'lg-keep'
            return 'lg-keep'
          case 9:
          case 10:
            state.height = 'xlg-keep'
            return 'xlg-keep'
        }
      }
    }
  }
}
</script>

<style>

</style>
