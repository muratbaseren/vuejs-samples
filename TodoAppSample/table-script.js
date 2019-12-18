var app = new Vue({
    el: "#app",
    data: {
        editModel: { fname: "", lname: "", email: "" },
        delModel: { fname: "", lname: "", email: "" },
        addModel: { fname: "", lname: "", email: "" },
        users: []
    },
    created: function () {
        for (let i = 0; i < 20; i++) {
            var firstName = faker.name.firstName();
            var lastName = faker.name.lastName();
            var email = faker.internet.email();

            this.users.push({
                id: i,
                fname: firstName,
                lname: lastName,
                email: email,
                modified: false,
                deleted: false,
                added: false,
                disabled: false,
                hide: false,
                origin: {
                    fname: firstName,
                    lname: lastName,
                    email: email
                }
            });
        }
    },
    methods: {
        addingUser: function () {

        },
        addUser: function () {
            var newid = -1;

            for (let i = 0; i < this.users.length; i++) {
                if (this.users[i].id > newid)
                    newid = this.users[i].id;
            }

            var user = {
                id: newid++,
                fname: this.addModel.fname,
                lname: this.addModel.lname,
                email: this.addModel.email,
                modified: false,
                deleted: false,
                disabled: false,
                added: true,
                hide: false,
                origin: {
                    fname: this.addModel.fname,
                    lname: this.addModel.lname,
                    email: this.addModel.email
                }
            };

            this.users.unshift(user);

            this.addModel = { fname: "", lname: "", email: "" };
        },
        editingUser: function (user) {
            this.editModel = {
                id: user.id,
                index: this.users.indexOf(user),
                fname: user.fname,
                lname: user.lname,
                email: user.email,
            };
        },
        editUser: function () {
            var user = this.users[this.editModel.index];
            user.fname = this.editModel.fname;
            user.lname = this.editModel.lname;
            user.email = this.editModel.email;
            user.modified = true;

            // send modified data to server with save button.
        },
        deletingUser: function (user) {
            this.delModel = user;
        },
        deleteUser: function () {
            var user = this.users[this.users.indexOf(this.delModel)];
            user.deleted = true;

            if (user.added) {
                user.hide = true;
                this.users.splice(this.users.indexOf(user), 1);
            }

            // send deleted data to server with save button.
        },
        revertUser: function (user) {
            user.modified = false;
            user.fname = user.origin.fname;
            user.lname = user.origin.lname;
            user.email = user.origin.email;
        },
        recoveryUser: function (user) {
            user.deleted = false;
        },
        sendData: function () {
            var postData = [];
            for (let i = 0; i < this.users.length; i++) {
                const user = this.users[i];

                if (user.modified || user.deleted || user.added) {
                    postData.push({
                        id: user.id,
                        fname: user.fname,
                        lname: user.lname,
                        email: user.email,
                        modified: user.modified,
                        deleted: user.deleted,
                        added: user.added,
                    });
                }
            }

            // send data to server.
            console.log(postData);
        }
    }
});