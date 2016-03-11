angular
    .module('app')
    .controller('UserRolesModalController', UserRolesModalController);

function userRoleParameters() {
    this.userId = null;
    this.organizationId = null;
};

function UserRolesModalController($scope, $uibModalInstance, userRoleFactory, user) {

    $scope.userId = user.id;
    userRoleParameters.userId = $scope.userId;
    $scope.user = user;

    $scope.models = {
        selected: null,
        lists: { "Available": [], "Assigned": [] }
    };

    // Find the user default profile organization id
    for (var i = 0; i < user.userOrganizations.length; ++i) {
        if (user.userOrganizations[i].default === true) {
            userRoleParameters.organizationId = user.userOrganizations[i].organizationId;
        } else {
            if (user.userOrganizations.length > 0) {
                userRoleParameters.organizationId = user.userOrganizations[0].organizationId;
            }
        }
    }

    $scope.data = userRoleFactory;

    userRoleFactory.getAvailableRoles(userRoleParameters.organizationId)
        .then(function(availableRoles) {
                // success
                $scope.availableRoles = availableRoles;
                for (var i = 0; i < $scope.availableRoles.length; ++i) {
                    $scope.models.lists.Available.push({
                        label: $scope.availableRoles[i].name,
                        object: $scope.availableRoles[i]
                    });
                }
            },
            function() {
                // error
                alert("could not get available roles");
            });


    userRoleFactory.getUserRoles(userRoleParameters.userId)
        .then(function(userRoles) {
                // success
                $scope.userRoles = userRoles;
                for (var i = 0; i < $scope.userRoles.length; ++i) {
                    $scope.models.lists.Assigned.push({
                        label: $scope.userRoles[i].name,
                        Default: $scope.userRoles[i].default,
                        object: $scope.userRoles[i]
                    });
                    var j = 0;
                    while (j < $scope.models.lists.Available.length) {
                        if ($scope.userRoles[i].roleId === $scope.models.lists.Available[j].object.roleId) {
                            $scope.models.lists.Available.splice(j, 1);
                        }
                        j++;
                    };
                }
            },
            function() {
                // error
                alert("could not get user roles");
            });

    // Model to JSON for demo purpose
    //$scope.$watch('models', function (model) {
    //    $scope.modelAsJson = angular.toJson(model, true);
    //}, true);

    $scope.ok = function () {

        userRoleFactory.editUserRoles($scope.models.lists.Assigned, $scope.user)
            .then(function () {
                // success
                $uibModalInstance.close();
            },
                function () {
                    // error
                    alert("could not save roles");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};


angular
    .module('app')
    .factory('userRoleFactory', userRoleFactory);

function userRoleFactory($http, $q) {

    var _availableRoles = [];
    var _userRoles = [];
    var _getAvailableRoles = function (id) {

        var deferred = $q.defer();

        $http.get('/api/Roles/' + id)
            .then(function (result) {
                    // Successful
                    angular.copy(result.data, _availableRoles);
                    deferred.resolve(_availableRoles);
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

    var _editUserRoles = function(roleList, user) {

        user.UserRoles = [];

        for (var i = 0; i < roleList.length; i++) {
            var userRole = {
                UserId: user.id,
                RoleId: roleList[i].object.roleId,
                OrganizationId: userRoleParameters.organizationId,
                Default: roleList[i].default
            };
            user.UserRoles.push(userRole);
        }

        var deferred = $q.defer();

        $http.put('/api/UserRoles/', user)
            .then(function(result) {
                    // success
                    deferred.resolve(result);
                },
                function() {
                    // error
                    deferred.reject();
                });

        return deferred.promise;
    };

    return {
        availableRoles: _availableRoles,
        getAvailableRoles: _getAvailableRoles,
        userRoles: _userRoles,
        getUserRoles: _getUserRoles,
        editUserRoles: _editUserRoles
    };
}