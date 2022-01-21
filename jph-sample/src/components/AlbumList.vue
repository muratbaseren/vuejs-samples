<template>
  <div class="row">
    <div class="col-md-12">
      <h3>
        Albums of ...
      </h3>
    </div>
    <div class="col-md-12">
      <div class="albumList">
        <ul class="list-group" v-if="isExistsAlbums">
          <li
            class="list-group-item"
            v-for="(album, aindex) in albums"
            :key="aindex"
          >
            <span>{{ album.name }}</span>
            <icon name="list"></icon>
          </li>
        </ul>
        <div v-else>
          Lütfen bir kişi seçiniz..
        </div>
      </div>
    </div>
  </div>
</template>

<script>
// to use vue-awesome should run npm install vue-awesome command.
import "vue-awesome/icons";
import Icon from "vue-awesome/components/Icon";
// import axios from "axios";

export default {
  name: "AlbumList",
  props: ["datasource"],
  components: {
    icon: Icon
  },
  data() {
    return {
      albums: this.datasource
    };
  },
  computed: {
    isExistsAlbums() {
      return this.albums.length > 0;
    }
  }
  // methods: {
  //   load(userid) {
  //     axios
  //       .get("https://jsonplaceholder.typicode.com/albums?userid=" + userid)
  //       .then(res => (this.albums = res.data));
  //   }
  // }
};
</script>

<style scoped></style>
