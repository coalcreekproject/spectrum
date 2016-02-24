(function(app) {

    "use strict";

    app.service("UserService", userService);

    userService.$inject = ["$http", "$q"];

    function userService($http, $q) {

        var users = [];

        const getUsers = function() {

            var deferred = $q.defer();

            $http.get("/api/Users")
                .then(function(result) {

                        // Successful
                        angular.copy(result.data, users);
                        deferred.resolve(users);
                    },
                    function() {
                        // Error
                        deferred.reject();
                    });

            return deferred.promise;
        };

        const addUser = function(newUser) {

            var deferred = $q.defer();

            $http.post("/api/Users", newUser)
                .then(function(result) {

                        // success
                        const newlyCreatedUser = result.data;
                        users.splice(0, 0, newlyCreatedUser);
                        deferred.resolve(newlyCreatedUser);
                    },
                    function() {
                        // error
                        deferred.reject();
                    });

            return deferred.promise;
        };

        const editUser = function(user) {
            var deferred = $q.defer();

            $http.put(`/api/Users/${user.Id}`, user)
                .then(function(result) {
                        // success
                        const editedUser = result.data;
                        for (let i = 0; i < users.length; i++) {
                            if (users[i].Id === editedUser.Id) {
                                users[i].UserName = editedUser.UserName;
                                users[i].Email = editedUser.Email;
                                break;
                            }
                        }

                        deferred.resolve(editedUser);
                    },
                    function() {
                        // error
                        deferred.reject();
                    });

            return deferred.promise;
        };

        const deleteUser = function(user) {
            var deferred = $q.defer();

            $http.delete(`/api/Users/${user.Id}`, user)
                .then(function(result) {
                        const deletedUser = result.data;
                        for (let i = 0; i < users.length; i++) {
                            if (users[i].Id === deletedUser.Id) {
                                users.splice(i, 1);
                                break;
                            }
                        }

                        deferred.resolve();
                    },
                    function() {
                        // error
                        deferred.reject();
                    });

            return deferred.promise;
        };
        return {
            users: users,
            getUsers: getUsers,
            addUser: addUser,
            editUser: editUser,
            deleteUser: deleteUser
        };
    }

})(angular.module("app"))