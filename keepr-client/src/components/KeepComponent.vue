<template>
  <div @click="toggleModal(keepProp.id)" class="pointer">
    <div class="card bg-dark text-white" :class="state.height">
      <img class="card-img" :class="state.height" :src="keepProp.img" :alt="keepProp.img">
      <div class="card-img-overlay pb-2 d-flex align-items-end justify-content-between gradient">
        <h5 class="card-title mb-0">
          {{ keepProp.name }} {{ randomHeights() }}
        </h5>
        <router-link :to="{name: 'ProfilePage', params: {id: keepProp.creatorId}}">
          <img :src="keepProp.creator.picture" class="userPic">
        </router-link>
      </div>
    </div>
    <keeper-modal :modal-prop="keepProp" />
  </div>
</template>
<script>
import $ from 'jquery'
import { onMounted, reactive } from 'vue'
export default {
  name: 'KeepComponent',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      height: ''
    })
    onMounted(() => {

    })
    return {
      state,
      toggleModal(id) {
        setTimeout(() => $('#id' + id).modal('show'), 50)
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
.xsm-keep{
  min-height: 20em;
}
.sm-keep{
  min-height: 23em;
}
.md-keep{
  min-height: 26em;
}
.lg-keep{
  min-height: 29em;
}
.xlg-keep{
  min-height: 32em;
}
.full-img{
  min-height: 100%;
  width: 100%;
}

</style>
