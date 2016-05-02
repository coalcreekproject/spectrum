angular
    .module('app')
    .controller('UserRolesModalController', UserRolesModalController);

function userRoleParameters() {
    this.userId = null;
    this.organizationId = null;
};

function UserRolesModalController($scope, $uibModalInstance, userRoleFactory, user) {

    $scope.user = user;
    $scope.userId = user.id;
    userRoleParameters.userId = user.id;
    
    $scope.models = {
        selected: null,
        lists: { "Available": [], "Assigned": [] }
    };

    $scope.data = userRoleFactory;
    
    userRoleFactory.getUserRoles(userRoleParameters.userId)
        .then(function(userRoles) {
                // success
                $scope.userRoles = userRoles;
                $scope.models.lists.Available = userRoles.item1;
                $scope.models.lists.Assigned = userRoles.item2;
            },
            function() {
                // error
                alert("could not get user roles");
            });

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

    var _userRoles = [];


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
        user.UserRoles = roleList;

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
        userRoles: _userRoles,
        getUserRoles: _getUserRoles,
        editUserRoles: _editUserRoles
    };
}