<template>
  <button @click="chosenVault()" class="dropdown-item" type="button">
    {{ uservProp.name }}
  </button>
</template>

<script>
import { reactive } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { useRouter } from 'vue-router'
import $ from 'jquery'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'VaultDropMenu',
  props: {
    uservProp: {
      type: Object,
      required: true
    },
    addKeep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({

    })
    return {
      state,
      async chosenVault() {
        await vaultKeepsService.createVaultKeep(props.uservProp.id, props.addKeep.id)
        $('*').modal('hide')
        router.push({ name: 'VaultPage', params: { id: props.uservProp.id } })
        await keepsService.addKeepCount(props.addKeep.id)
      }
    }
  }
}
</script>

<style>

</style>
