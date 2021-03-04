<template>
  <div class="container-fluid">
    <div class="row mt-4">
      <div class="col-11 mx-auto justify-content-center">
        <div class="card-columns justify-content-center">
          <keep-component v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
import { AccountService } from '../services/AccountService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  account() {
    AccountService.getAccount()
  },
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      try {
        await keepsService.getAll()
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

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}

</style>
