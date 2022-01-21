<template>
  <div class="home row">
    <div class="col-md-6">
      <user-list :datasource="users" :selected="selectedUser" />
    </div>
    <div class="col-md-6">
      <album-list :datasource="albums" :user="selectedUser" />
    </div>
  </div>
</template>

<script>
import UserList from "./UserList";
import AlbumList from "./AlbumList";
import axios from "axios";

export default {
  name: "Home",
  components: {
    "user-list": UserList,
    "album-list": AlbumList
  },
  data() {
    return {
      selectedUser: -1,
      users: [],
      albums: []
    };
  },
  mounted() {
    axios
      .get("https://jsonplaceholder.typicode.com/users")
      .then(res => (this.users = res.data));
  }
};
</script>

<style scoped></style>
