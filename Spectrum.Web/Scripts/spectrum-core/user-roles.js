angular
    .module('app')
    .factory('userRoleFactory', userRoleFactory);


function userRoleFactory($http, $q) {

    var _availableUserRoles = [];
    var _userRoles = [];

    var _getAvailableUserRoles = function (id) {

        var deferred = $q.defer();

        $http.get('/api/Roles/' + id)
            .then(function (result) {
                // Successful
                angular.copy(result.data, _availableUserRoles);
                deferred.resolve(_availableUserRoles);
            },
                function () {
                    // Error
                    deferred.reject();
                });

        return deferred.promise;
    };

    var _getUserRoles = function(id) {

        var deferred = $q.defer();

        $http.get('/api/UserRoles/' + id)
            .then(function(result) {
                    // Successful
                    angular.copy(result.data, _userRoles);
                    deferred.resolve(_userRoles);
                },
                function() {
                    // Error
                    deferred.reject();
                });

        return deferred.promise;
    };


    var _addUserRoles = function (newlyAddedRole) {

        var deferred = $q.defer();

        $http.post('/api/UserRoles/', newlyAddedRole)
            .then(function(result) {
                    // success
                    var newlyCreatedUser = result.data;
                    _userRoles.splice(0, 0, newlyAddedRole);
                    deferred.resolve(newlyCreatedUser);
                },
                function() {
                    // error
                    deferred.reject();
                });

        return deferred.promise;
    };

    //var _editUser = function(user) {

    //    var deferred = $q.defer();

    //    $http.put('/api/Users/' + user.Id, user)
    //     .then(function (result) {
    //         // success
    //         var editedUser = result.data;

    //         for (var i = 0; i < _users.length; i++) {
    //             if (_users[i].Id === editedUser.Id) {
    //                 _users[i].UserName = editedUser.UserName;
    //                 _users[i].Email = editedUser.Email;
    //                 break;
    //             }
    //         }

    //         deferred.resolve(editedUser);
    //     },
    //     function () {
    //         // error
    //         deferred.reject();
    //     });

    //    return deferred.promise;
    //};

    var _deleteUserRoles = function(user) {

        var deferred = $q.defer();

        $http.delete('/api/UserRoles/' + user.Id, role)
            .then(function(result) {

                    var deletedUserRoles = result.data;

                    for (var i = 0; i < _userRoles.length; i++) {
                        if (_userRoles[i].Id === deletedUserRoles.Id) {
                            _userRoles.splice(i, 1);
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
        getAvailableUserRoles: _getAvailableUserRoles,
        userRoles: _userRoles,
        getUserRoles: _getUserRoles,
        addUserRoles: _addUserRoles,
        //editUser: _editUser,
        deleteUserRoles: _deleteUserRoles
    };
}