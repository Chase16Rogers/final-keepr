<template>
  <i class="fa fa-trash-o fa-2x align-self-center pointer trash-style text-secondary" aria-hidden="true" @click="coolAlert()"></i>
</template>

<script>
import { computed, reactive } from 'vue'
import swal from 'sweetalert'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import { vaultsService } from '../services/VaultsService'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  name: 'ConfirmDelete',
  props: {
    deleteProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      account: computed(() => AppState.account)
    })
    return {
      state,
      coolAlert() {
        swal({
          title: 'Are you sure?',
          text: 'Once deleted it cannot be undone!',
          closeOnClickOutside: false,
          icon: 'warning',
          buttons: true,
          dangerMode: true
        })
          .then((willDelete) => {
            if (willDelete) {
              this.deleter()
              swal({
                icon: 'success',
                text: 'Successfully deleted!',
                toast: true,
                position: 'top-end',
                buttons: false,
                timer: 1000
              })
            }
            // else {
            //   swal('Your imaginary file is safe!', {
            //     timer: 1000,
            //     buttons: false
            //   })
            // }
          })
      },
      async deleter() {
        switch (props.deleteProp.dataType) {
          case 'keep':
            $('*').modal('hide')
            await keepsService.deleteKeep(props.deleteProp.id)
            break
          case 'vault':
            router.push({ name: 'ProfilePage', params: { id: state.account.id } })
            await vaultsService.deleteVault(props.deleteProp.id)
            break
        }
      }
    }
  }

}
</script>

<style>

</style>
