<template>
  <!-- Modal -->
  <div class="modal fade" :id="'id' + modalProp.id" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered bigger-modal mt-5">
      <div class="modal-content">
        <div class="modal-body p-2">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-6 col-sm-12 p-0">
                <img :src="modalProp.img" alt="" class="img-fluid">
              </div>
              <div class="col-md-6 col-sm-12">
                <div class="container-fluid d-flex flex-column">
                  <div class="row justify-content-end">
                    <p class="mb-0 pointer move-x" data-dismiss="modal">
                      X
                    </p>
                  </div>
                  <div class="row">
                    <div class="d-flex justify-content-around mx-auto">
                      <div class="d-flex align-items-center justify-content-between">
                        <i class="fa fa-eye mr-2" aria-hidden="true"></i>
                        <p class="mb-0">
                          {{ modalProp.views }}
                        </p>
                      </div>
                      <div class="d-flex align-items-center justify-content-between mx-3">
                        <i class="fa fa-thumb-tack mr-2" aria-hidden="true"></i>
                        <p class="mb-0">
                          {{ modalProp.keeps }}
                        </p>
                      </div>
                      <div class="d-flex align-items-center justify-content-between">
                        <i class="fa fa-share-alt mr-2" aria-hidden="true"></i>
                        <p class="mb-0">
                          {{ modalProp.shares }}
                        </p>
                      </div>
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-12 text-center">
                      <p class="huge-text">
                        {{ modalProp.name }}
                      </p>
                    </div>
                  </div>
                  <div class="row bottom-space">
                    <div class="col-12 px-5">
                      <p>
                        {{ modalProp.description }}
                      </p>
                    </div>
                  </div>
                  <div class="row bottom-out">
                    <div class="col-12 d-flex justify-content-between align-items-center">
                      <!-- drop -->
                      <div class="dropdown">
                        <button class="btn btn-outline-info dropdown-toggle"
                                type="button"
                                id="dropdownMenu2"
                                data-toggle="dropdown"
                                aria-haspopup="true"
                                aria-expanded="false"
                                @click="getVaults()"
                        >
                          Add to Vault
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenu2">
                          <vault-drop-menu v-for="vault in state.userVaults" :key="vault.id" :userv-prop="vault" :add-keep="modalProp" />
                        </div>
                      </div>
                      <!-- end drop -->
                      <i class="fa fa-trash-o fa-2x align-self-center" aria-hidden="true"></i>
                      <div class="d-flex align-items-center" @click="profileRoute(modalProp.creatorId)">
                        <img :src="modalProp.creator.picture" class="modal-user mobile-margin">
                        <p class="mb-0 hide-mobile">
                          {{ modalProp.creator.name }}
                        </p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- ▲ -->
</template>
<script>
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import $ from 'jquery'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'KeeperModal',
  props: {
    modalProp: {
      type: Object,
      required: true
    }
  },
  setup() {
    const router = useRouter()
    const state = reactive({
      userVaults: computed(() => AppState.usersVaults)
    })
    return {
      state,
      profileRoute(id) {
        $('*').modal('hide')
        router.push({ name: 'ProfilePage', params: { id: id } })
      },
      getVaults() {
        if (AppState.usersVaults.length < 1) {
          vaultsService.getUsersVaults()
        }
      }
    }
  }
}
</script>

<style>
p{
  word-wrap: break-word;
}
</style>
