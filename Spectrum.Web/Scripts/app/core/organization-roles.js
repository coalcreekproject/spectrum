angular
    .module('app')
    .controller('OrganizationRoleController', organizationRoleController);

function organizationRoleParameters() {
    this.organizationId = null;
};

function organizationRoleController($scope, $http, $window, $modal, $stateParams, organizationRoleFactory) {

    $scope.organizationId = $stateParams.organizationId;
    organizationRoleParameters.organizationId = $scope.organizationId;

    $modal.scope = $scope;
    $scope.data = organizationRoleFactory;

    organizationRoleFactory.getOrganizationRoles($scope.organizationId)
        .then(function (organizationRoles) {
            // success
            //$scope.data = organization;
        },
            function () {
                // error
                alert("Sorry! There was a problem loading organization roles.  Please try again later.");
            });

    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/addOrganizationRoleModal',
            controller: AddOrganizationRoleModalController
        });
    };

    $scope.edit = function (organizationRole) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/EditOrganizationRoleModal',
            controller: EditOrganizationRoleModalController,
            resolve: {
                organizationRole: function () {
                    return angular.copy(organizationRole);
                }
            }
        });
    };

    $scope.delete = function (organizationRole) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/deleteOrganizationRoleModal',
            controller: DeleteOrganizationRoleModalController,
            resolve: {
                organizationRole: function () {
                    return angular.copy(organizationRole);
                }
            }
        });
    };
};

function AddOrganizationRoleModalController($scope, $modalInstance, organizationRoleFactory) {

    $scope.ok = function (organizationRole) {

        organizationRole.OrganizationId = organizationRoleParameters.organizationId;

        organizationRoleFactory.addOrganizationRoles(organizationRole)
            .then(function () {
                // success
                $modalInstance.close();
            },
                function () {
                    // error
                    alert("could not save organization role");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

function EditOrganizationRoleModalController($scope, $modalInstance, organizationRoleFactory, organizationRole) {

    $scope.organizationRole = organizationRole;

    $scope.ok = function () {

        organizationRoleFactory.editOrganizationRoles(organizationRole)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not edit or update organization role");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

function DeleteOrganizationRoleModalController($scope, $modalInstance, organizationRoleFactory, organizationRole) {

    $scope.organizationRole = organizationRole;

    $scope.ok = function () {

        organizationRoleFactory.deleteOrganizationRoles(organizationRole)
            .then(function () {
                // success

            },
                function () {
                    // error
                    alert("could not delete organization Role");
                });

        $modalInstance.close();

    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

/**
 * Pass function into module
 */
angular
    .module('app')
    .factory('organizationRoleFactory', organizationRoleFactory);

function organizationRoleFactory($http, $q) {

    var _organizationRoles = [];

    var _getOrganizationRoles = function (id) {

        var deferred = $q.defer();

        $http.get('/api/Roles/' + id)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _organizationRoles);
              deferred.resolve(_organizationRoles);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };


    var _addOrganizationRole = function (newOrganizationRole) {

        var deferred = $q.defer();

        $http.post('/api/Roles', newOrganizationRole)
         .then(function (result) {
             // success
             var newlyCreatedOrganizationRole = result.data;
             _organizationRoles.splice(0, 0, newOrganizationRole);
             deferred.resolve(newlyCreatedOrganizationRole);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editOrganizationRole = function (organizationRole) {

        var deferred = $q.defer();

        $http.put('/api/Roles/' + organizationRole.Id, organizationRole)
         .then(function (result) {
             // success
             var editedOrganizationRole = result.data;

             for (var i = 0; i < _organizationRoles.length; i++) {
                 if (_organizationRoles[i].Id === editedOrganizationRole.Id) {
                     _organizationRoles[i].Name = editedOrganizationRole.Name;
                     _organizationRoles[i].Description = editedOrganizationRole.Description;

                     break;
                 }
             }

             deferred.resolve(editedOrganizationRole);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _deleteOrganizationRole = function (organizationRole) {

        var deferred = $q.defer();

        $http.delete('/api/Roles/' + organizationRole.Id, organizationRole)
         .then(function (result) {

             var deletedOrganizationRole = result.data;

             for (var i = 0; i < _organizationRoles.length; i++) {
                 if (_organizationRoles[i].Id === deletedOrganizationRole.Id) {
                     _organizationRoles.splice(i, 1);
                     break;
                 }
             }

             deferred.resolve();
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    return {
        organizationRoles: _organizationRoles,
        getOrganizationRoles: _getOrganizationRoles,
        addOrganizationRoles: _addOrganizationRole,
        editOrganizationRoles: _editOrganizationRole,
        deleteOrganizationRoles: _deleteOrganizationRole
    };
};