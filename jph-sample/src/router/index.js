import Vue from "vue";
import Router from "vue-router";
import Home from "@/components/Home";
import Albums from "@/components/AlbumList";
import Users from "@/components/UserList";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "Home",
      component: Home
    },
    {
      path: "/users",
      name: "Users",
      component: Users
    },
    {
      path: "/albums",
      name: "Albums",
      component: Albums
    }
  ]
});
