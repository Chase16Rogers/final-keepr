<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">
        <p class="huge-text">
          {{ state.vault.name }}
        </p>
      </div>
      <div class="col-12">
        <p>Keeps: {{ state.keeps.length }}</p>
      </div>
    </div>
    <div class="row">
      <div class="col-11 mx-auto justify-content-center">
        <div class="card-columns justify-content-center">
          <keep-component v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, reactive, computed } from 'vue'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      vault: computed(() => AppState.activeVault)
    })
    onMounted(async() => {
      try {
        vaultsService.getVaultById(route.params.id)
        keepsService.getKeepsByVault(route.params.id)
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      state
    }
  }
}
</script>

<style>

</style>
