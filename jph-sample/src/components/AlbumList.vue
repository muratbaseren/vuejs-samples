<template>
  <div class="row">
    <div class="col-md-12">
      <div class="albumList" :title="message" @click="myalert">
        <div>{{ text }}</div>
        <div>
          <icon name="check-circle" scale="2" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// to use vue-awesome should run npm install vue-awesome command.
import "vue-awesome/icons";
import Icon from "vue-awesome/components/Icon";

export default {
  name: "AlbumList",
  props: ["message"],
  components: {
    icon: Icon
  },
  data() {
    return {
      text: "AlbumList component"
    };
  },
  methods: {
    myalert() {
      alert(this.message);
    }
  },
  created() {
    this.text += ", click me!";
  }
};
</script>

<style scoped>
.albumList {
  font-weight: bold;
  color: #41b883;
}
</style>
