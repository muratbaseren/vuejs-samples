<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <a class="navbar-brand" href="#">JPH Sample</a>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item" v-for="(route, ri) in routes" :key="ri">
          <router-link :to="route.path" class="nav-link">{{
            route.name
          }}</router-link>
        </li>

        <!-- <li class="nav-item active">
          <router-link to="/" class="nav-link"
            >Home <span class="sr-only">(current)</span></router-link
          >
        </li>
        <li class="nav-item">
          <router-link to="/users" class="nav-link">Users</router-link>
        </li>
        <li class="nav-item">
          <router-link to="/albums" class="nav-link">Albums</router-link>
        </li> -->
        <!-- <li class="nav-item dropdown">
          <a
            class="nav-link dropdown-toggle"
            href="#"
            id="navbarDropdown"
            role="button"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            Dropdown
          </a>
          <div class="dropdown-menu" aria-labelledby="navbarDropdown">
            <a class="dropdown-item" href="#">Action</a>
            <a class="dropdown-item" href="#">Another action</a>
            <div class="dropdown-divider"></div>
            <a class="dropdown-item" href="#">Something else here</a>
          </div>
        </li>
        <li class="nav-item">
          <a
            class="nav-link disabled"
            href="#"
            tabindex="-1"
            aria-disabled="true"
            >Disabled</a
          >
        </li> -->
      </ul>
      <form class="form-inline my-2 my-lg-0">
        <input
          class="form-control mr-sm-2"
          type="search"
          placeholder="arama"
          aria-label="Search"
          v-model="search"
        />
        <button class="btn btn-outline-success my-2 my-sm-0" type="submit">
          Arama
        </button>
      </form>
    </div>
  </nav>
</template>

<script>
// to use vue-awesome should run npm install vue-awesome command.
// import "vue-awesome/icons";
// import Icon from "vue-awesome/components/Icon";

export default {
  name: "Navbar",
  components: {
    // icon: Icon
  },
  data() {
    return {
      routes: this.$router.options.routes,
      search: ""
    };
  }
};
</script>
