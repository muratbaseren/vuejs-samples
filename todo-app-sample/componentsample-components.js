Vue.component("user-list", {
    data: function () {
        return { users: [] };
    },
    created: function () {
        const getUsers = () => {
            return fetch("https://jsonplaceholder.typicode.com/users")
                .then(res => res.json())
                .then(json => this.users = json);
        }

        getUsers();
    },
    template: "<ul class='list-group'><li class='list-group-item' v-for='user in users'>{{user.name}}</li></ul>"
});