<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-3 d-flex justify-content-center mt-5">
        <img :src="state.profile.picture" class="img-fluid bigger-user">
      </div>
      <div class="col-9 mt-4">
        <p class="mb-0 bigP-text">
          {{ state.profile.name }}
        </p>
        <p class="big-text mb-0">
          Vaults: {{ state.vaults.length }}
        </p>
        <p class="big-text">
          Keeps: {{ state.keeps.length }}
        </p>
      </div>
    </div>
    <div class="row mt-2">
      <div class="col-12 d-flex">
        <p class="mb-0 mr-2 bigP-text">
          Vaults
        </p>
        <p class="mb-0 bigP-text">
          +
        </p>
      </div>
    </div>
    <div class="row">
      <div class="col-12 d-flex">
        <p class="mb-0 mr-2 bigP-text">
          Keeps
        </p>
        <p class="mb-0 bigP-text pointer" data-toggle="modal" data-target="#createKeep">
          +
        </p>
      </div>
    </div>
    <create-keep />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import $ from 'jquery'
export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      try {
        $('*').modal('hide')
        await profilesService.getProfileById(route.params.id)
        await vaultsService.getVaultsByProfile(route.params.id)
        await keepsService.getKeepsByProfile(route.params.id)
      } catch (error) {

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
