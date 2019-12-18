<template>
  <div class="home" :title="message" @click="myalert">
    <div>{{ text }}</div>
    <div>
      <icon name="check-circle" scale="2" />
    </div>
  </div>
</template>

<script>
// to use vue-awesome should run npm install vue-awesome command.
import "vue-awesome/icons";
import Icon from "vue-awesome/components/Icon";

export default {
  name: "Home",
  props: ["message"],
  components: {
    icon: Icon
  },
  data() {
    return {
      text: "Home component"
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
.home {
  font-weight: bold;
  color: #41b883;
}
</style>