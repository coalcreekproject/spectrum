angular
    .module('app')
    .controller('UserRolesController', UserRolesController);


function userRoleParameters() {
    this.userId = null;
    this.organizationId = null;
};


function UserRolesController($scope, $modalInstance, userRoleFactory, userProfileFactory, user) {

    $scope.userId = user.Id;
    userRoleParameters.userId = $scope.userId;
    
    $scope.models = {
        selected: null,
        lists: { "Available": [], "Assigned": [] }
    };

    // Find the user default profile organization id
    for (var i = 0; i < user.UserProfiles.length; ++i) {
        if (user.UserProfiles[i].Default === true) {
            userRoleParameters.organizationId = user.UserProfiles[i].OrganizationId;
        }
    }

    $scope.data = userRoleFactory;
    var availableRoles = [];
    var assignedUserRoles = [];
    
    $scope.data.getAvailableUserRoles(userRoleParameters.organizationId).resolve;
    $scope.data.getUserRoles(user.Id, assignedUserRoles);

    // Generate initial models
    for (var i = 1; i <= availableRoles.length; ++i) {
        $scope.models.lists.Available.push({ label: availableRoles[i].Name });
    }

    for (var i = 1; i <= assignedUserRoles.length; ++i) {
        $scope.models.lists.Assigned.push({ label: assignedUserRoles[i].Name });
    }

    // Model to JSON for demo purpose
    //$scope.$watch('models', function (model) {
    //    $scope.modelAsJson = angular.toJson(model, true);
    //}, true);

    $scope.ok = function (user) {

        //userFactory.addUser(user)
        //    .then(function () {
        //        // success
        //        $modalInstance.close();
        //    },
        //        function () {
        //            // error
        //            alert("could not save roles");
        //        });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};


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