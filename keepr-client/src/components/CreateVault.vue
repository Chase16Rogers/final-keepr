<template>
  <div class="modal fade" id="createVault" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-body pt-0">
          <div class="container-fluid">
            <div class="row">
              <div class="col-12 d-flex justify-content-end">
                <p class="big-text mb-0 pointer move-x" data-dismiss="modal">
                  x
                </p>
              </div>
            </div>
            <div class="row">
              <div class="col-12">
                <p class="bigP-text">
                  New Vault
                </p>
              </div>
            </div>
            <form class="form-group" @submit.prevent="createVault()">
              <div class="row">
                <div class="col-12">
                  <p>Title</p>
                  <input class="form-control mb-3" type="text" placeholder="Title..." v-model="state.newVault.name" required>
                  <p>Image URL</p>
                  <input class="form-control mb-3" type="text" placeholder="URL..." v-model="state.newVault.img" required>
                  <div class="d-flex align-items-center">
                    <input type="checkbox" aria-label="Checkbox" v-model="state.newVault.isPrivate" class="mr-2">
                    <p>Private?</p>
                  </div>
                  <p>Private Vaults can only be seen by you</p>
                  <div class="col-12 d-flex justify-content-end mt-4">
                    <button type="submit" class="btn btn-info">
                      Create
                    </button>
                  </div>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from 'vue'
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'
export default {
  name: 'CreateVault',
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createVault() {
        $('#createVault').modal('hide')
        await vaultsService.createVault(state.newVault)
        state.newVault = {}
      }
    }
  }
}
</script>

<style>

</style>
