<template>
  <div>
    <div class="card bg-dark shadow text-white pointer mb-3
    "
         :class="state.height"
         @click="toggleModal(keepProp.id)"
    >
      <img class="card-img" :class="state.height" :src="keepProp.img" :alt="keepProp.img">
      <div class="card-img-overlay pb-2 d-flex align-items-end justify-content-between gradient">
        <h5 class="card-title mb-0">
          {{ keepProp.name }}
        </h5>
        <img :src="keepProp.creator.picture" class="userPic" @click="goToProfile()" v-if="pageProp.page !== 'profile' ">
      </div>
    </div>
    <keeper-modal :modal-prop="keepProp" :page-prop="pageProp" />
  </div>
</template>
<script>
import $ from 'jquery'
import { onMounted, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import { useRouter } from 'vue-router'

export default {
  name: 'KeepComponent',
  props: {
    keepProp: {
      type: Object,
      required: true
    },
    pageProp: {
      type: Object,
      required: true
    }
  },
  beforeMount() {
    this.randomHeights()
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      height: ''
    })

    onMounted(() => {
    })
    return {
      state,
      async toggleModal(id) {
        try {
          keepsService.addKeepView(id)
          const open = setTimeout(() => $('#id' + id).modal('show'), 30)
          console.log(open)
        } catch (error) {
          logger.error(error)
        }
      },
      goToProfile() {
        clearTimeout(open)
        router.push({ name: 'ProfilePage', params: { id: props.keepProp.creatorId } })
      },
      randomHeights() {
        const rnd = Math.floor(Math.random() * 10)
        switch (rnd) {
          case 1:
          case 2:
            state.height = 'xsm-keep'
            return 'xsm-keep'
          case 3:
          case 4:
            state.height = 'sm-keep'
            return 'sm-keep'
          case 5:
          case 6:
            state.height = 'md-keep'
            return 'md-keep'
          case 7:
          case 8:
            state.height = 'lg-keep'
            return 'lg-keep'
          case 9:
          case 10:
            state.height = 'xlg-keep'
            return 'xlg-keep'
        }
      }
    }
  }
}
</script>

<style>

</style>
