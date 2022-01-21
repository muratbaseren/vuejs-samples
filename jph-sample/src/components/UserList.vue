<template>
  <div class="row">
    <div class="col-md-12">
      <h3>
        Users
      </h3>
    </div>
    <div class="col-md-12">
      <ul class="list-group">
        <li
          class="list-group-item text-left"
          v-for="(user, uindex) in users"
          :key="uindex"
        >
          <div class="row" v-if="users.length > 0">
            <div class="col-md-8">
              <icon name="user" class="mr-2" />
              <span>{{ user.name }}</span>
            </div>
            <div class="col-md-4 text-right">
              <button class="btn" type="button" @click="userClicked(user)">
                <icon name="chevron-right"></icon>
              </button>
            </div>
          </div>
          <div class="row" v-else>
            <div class="col-md-12">
              Kullanıcı verisi bulunmuyor..
            </div>
          </div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
// to use vue-awesome should run npm install vue-awesome command.
import "vue-awesome/icons";
import Icon from "vue-awesome/components/Icon";
// import axios from "axios";

export default {
  name: "UserList",
  props: ["datasource", "selected"],
  components: {
    icon: Icon
  },
  methods: {
    userClicked(user) {
      this.selected = user.id;
    }
  },
  data() {
    return {
      users: this.datasource
    };
  }
  // methods: {
  //   load() {
  //     axios.get("https://jsonplaceholder.typicode.com/users").then(res => {
  //       this.users = res.data;
  //     });
  //   }
  // },
  // mounted() {
  //    this.load();
  //   // this.selected = -1;
  // }
};
</script>

<style scoped></style>
