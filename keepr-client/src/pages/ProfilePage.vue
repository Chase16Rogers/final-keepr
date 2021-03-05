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
      <div class="col-12 d-flex ml-5">
        <p class="mb-0 mr-2 bigP-text">
          Vaults
        </p>
        <p class="mb-0 bigP-text pointer text-info" data-toggle="modal" data-target="#createVault" v-if="state.account.id === state.profile.id">
          +
        </p>
      </div>
    </div>
    <div class="row">
      <div class="col-11 mx-auto justify-content-center">
        <div class="card-columns justify-content-center">
          <vault-component v-for="vault in state.vaults" :key="vault.id" :vault-prop="vault" />
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-12 d-flex ml-5">
        <p class="mb-0 mr-2 bigP-text">
          Keeps
        </p>
        <p class="mb-0 bigP-text pointer text-info" data-toggle="modal" data-target="#createKeep" v-if="state.account.id === state.profile.id">
          +
        </p>
      </div>
    </div>
    <div class="row">
      <div class="col-11 mx-auto justify-content-center">
        <div class="card-columns justify-content-center">
          <keep-component v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" :page-prop="{page: 'profile'}" />
        </div>
      </div>
    </div>
    <create-vault />
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
import { logger } from '../utils/Logger'
import $ from 'jquery'
export default {
  name: 'ProfilePage',
  beforeUpdate() {
    $('*').modal('hide')
  },
  updated() {
    $('*').modal('hide')
  },
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account)
    })
    onMounted(async() => {
      $('*').modal('hide')
      setTimeout(() => $('*').modal('hide'), 2000)
      try {
        await profilesService.getProfileById(route.params.id)
        await vaultsService.getVaultsByProfile(route.params.id)
        await keepsService.getKeepsByProfile(route.params.id)
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
