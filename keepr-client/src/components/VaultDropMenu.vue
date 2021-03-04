<template>
  <button @click="chosenVault()" class="dropdown-item" type="button">
    {{ uservProp.name }}
  </button>
</template>

<script>
import { reactive } from 'vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
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
    const state = reactive({

    })
    return {
      state,
      chosenVault() {
        vaultKeepsService.createVaultKeep(props.uservProp.id, props.addKeep.id)
      }
    }
  }
}
</script>

<style>

</style>
