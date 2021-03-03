<template>
  <!-- Modal -->
  <div class="modal fade" id="createKeep" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
                  New Keep
                </p>
              </div>
            </div>
            <form class="form-group" @submit.prevent="createKeep()">
              <div class="row">
                <div class="col-12">
                  <p>Title</p>
                  <input class="form-control mb-3" type="text" placeholder="Title..." v-model="state.newKeep.name" required>
                  <p>Image URL</p>
                  <input class="form-control mb-3" type="text" placeholder="URL..." v-model="state.newKeep.img" required>
                  <p>Description</p>
                  <textarea class="form-control taller-text-area hide-scroller" id="validationTextarea" placeholder="Description..." required v-model="state.newKeep.description"></textarea>
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
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
export default {
  name: 'CreateKeep',
  setup() {
    const state = reactive({
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        $('#createKeep').modal('hide')
        await keepsService.createKeep(state.newKeep)
        state.newKeep = {}
      }
    }
  }

}
</script>

<style>
p{
  margin-bottom: 0;
}
.taller-text-area{
  min-height: 6.6em;
}
.hide-scroller::-webkit-scrollbar {
  display: none;
}
</style>
