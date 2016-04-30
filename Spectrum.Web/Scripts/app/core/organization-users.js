angular
    .module('app')
    .controller('OrganizationUserController', organizationUserController);

function organizationUserParameters() {
    this.organizationId = null;
};

function organizationUserController($scope, $http, $window, $stateParams, organizationUserfactory) {

    organizationUserParameters.organizationId = $stateParams.organizationId;
    $scope.organizationId = organizationUserParameters.organizationId;

    $scope.models = {
        selected: null,
        lists: { "Available": [], "Assigned": [] }
    };

    $scope.data = organizationUserfactory;

    organizationUserfactory.getOrganizationUsers(organizationUserParameters.organizationId)
        .then(function(organizationUsers) {
                // success
                $scope.organizationUsers = organizationUsers;
                $scope.models.lists.Available = organizationUsers.item1;
                $scope.models.lists.Assigned = organizationUsers.item2;
            },
            function() {
                // error
                alert("could not get users");
            });


    $scope.add = function() {

        organizationUserfactory.editOrganizationUsers($scope.organizationId, $scope.models.lists)
            .then(function() {
                    // success
                },
                function() {
                    // error
                    alert("could not save user assignmnets");
                });
    };
};


angular
    .module('app')
    .factory('organizationUserfactory', organizationUserfactory);

function organizationUserfactory($http, $q) {

    var _organizationUsers = [];


    var _getOrganizationUsers = function(organizationId) {

        var deferred = $q.defer();

        $http.get('/api/OrganizationUsers/' + organizationId)
            .then(function(result) {
                    // Successful
                    angular.copy(result.data, _organizationUsers);
                    deferred.resolve(_organizationUsers);
                },
                function() {
                    // Error
                    deferred.reject();
                });

        return deferred.promise;
    };

    var _editOrganizationUsers = function(id, lists) {

        var deferred = $q.defer();
        $http.put('/api/OrganizationUsers/' + id, lists)
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
        organizationUsers: _organizationUsers,
        getOrganizationUsers: _getOrganizationUsers,
        editOrganizationUsers: _editOrganizationUsers
    };
}