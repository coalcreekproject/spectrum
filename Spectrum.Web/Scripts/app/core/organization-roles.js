angular
    .module('app')
    .controller('OrganizationRoleController', organizationRoleController);

function organizationRoleParameters() {
    this.organizationId = null;
};

function organizationRoleController($scope, $http, $window, $uibModal, $stateParams, organizationRoleFactory) {

    organizationRoleParameters.organizationId = $stateParams.organizationId;

    $scope.data = organizationRoleFactory;

    organizationRoleFactory.getOrganizationRoles(organizationRoleParameters.organizationId)
        .then(function() {
                // success
            },
            function() {
                // error
                alert("Sorry! There was a problem loading organization roles.  Please try again later.");
            });

    $scope.add = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Templates/Organization/addOrganizationRoleModal',
            controller: AddOrganizationRoleModalController
        });
    };

    $scope.edit = function (organizationRole) {
        var modalInstance = $uibModal.open({
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
        var modalInstance = $uibModal.open({
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

function AddOrganizationRoleModalController($scope, $uibModalInstance, organizationRoleFactory) {

    $scope.ok = function (organizationRole) {

        organizationRole.organizationId = organizationRoleParameters.organizationId;
        organizationRole.roleId = null;

        organizationRoleFactory.addOrganizationRoles(organizationRole)
            .then(function() {
                    // success
                },
                function() {
                    // error
                    alert("could not save organization role");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};

function EditOrganizationRoleModalController($scope, $uibModalInstance, organizationRoleFactory, organizationRole) {

    organizationRoleFactory.getOrganizationRole(organizationRole)
        .then(function (result) {
            $scope.organizationRole = result;
        },
        function () {
            //Couldn't find it, stick the one passed in out there
            $scope.organizationRole = organizationRole;
        });

    $scope.ok = function () {

        organizationRoleFactory.editOrganizationRoles($scope.organizationRole)
            .then(function () {
                    // success
                },
                function () {
                    // error
                    alert("could not edit or update organization role");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};

function DeleteOrganizationRoleModalController($scope, $uibModalInstance, organizationRoleFactory, organizationRole) {

    organizationRoleFactory.getOrganizationRole(organizationRole)
    .then(function (result) {
        $scope.organizationRole = result;
    },
    function () {
        //Couldn't find it, stick the one passed in out there
        $scope.organizationRole = organizationRole;
    });

    $scope.ok = function() {

        organizationRoleFactory.deleteOrganizationRoles($scope.organizationRole)
            .then(function() {
                    // success
                },
                function() {
                    // error
                    alert("could not delete organization Role");
                });

        $uibModalInstance.close();

    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};

angular
    .module('app')
    .factory('organizationRoleFactory', organizationRoleFactory);

function organizationRoleFactory($http, $q) {

    var _organizationRole = {};
    var _organizationRoles = [];
    
    var _getOrganizationRole = function (organizationRole) {

        var deferred = $q.defer();

        $http.get('/api/Roles/?roleId=' + organizationRole.roleId + '&' + 'organizationId=' + organizationRole.organizationId)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _organizationRole);
              deferred.resolve(_organizationRole);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };


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
             newOrganizationRole = result.data;
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

        $http.put('/api/Roles/', organizationRole)
         .then(function (result) {
             // success
             var editedOrganizationRole = result.data;

             for (var i = 0; i < _organizationRoles.length; i++) {
                 if (_organizationRoles[i].roleId=== editedOrganizationRole.roleId) {
                     _organizationRoles[i].name = editedOrganizationRole.name;
                     _organizationRoles[i].description = editedOrganizationRole.description;

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

        $http.delete('/api/Roles/' + organizationRole.roleId)
         .then(function (result) {

             var deletedOrganizationRole = result.data;

             for (var i = 0; i < _organizationRoles.length; i++) {
                 if (_organizationRoles[i].roleId === deletedOrganizationRole.roleId) {
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
        organizationRole: _organizationRole,         
        getOrganizationRole: _getOrganizationRole,
        organizationRoles: _organizationRoles,
        getOrganizationRoles: _getOrganizationRoles,
        addOrganizationRoles: _addOrganizationRole,
        editOrganizationRoles: _editOrganizationRole,
        deleteOrganizationRoles: _deleteOrganizationRole
    };
};