angular
    .module('app')
    .controller('OrganizationController', organizationController)
    .config(config);

config.$inject = ["$stateProvider", "$urlRouterProvider", "$compileProvider"];

function config($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    // Set default state
    //$urlRouterProvider.otherwise("/dashboard");

    $stateProvider
        .state('index', {
            url: "",
            templateUrl: "/Templates/Organization/OrganizationIndex",
            data: {
                pageTitle: 'index'
            }
        })
        .state('users', {
            url: "/roles/:organizationId",
            templateUrl: "/Templates/Organization/OrganizationUsers",
            data: {
                pageTitle: 'users'
            }
        })
        .state('roles', {
            url: "/roles/:organizationId",
            templateUrl: "/Templates/Organization/OrganizationRoles",
            data: {
                pageTitle: 'roles'
            }
        })
        .state('profiles', {
            url: "/profiles/:organizationId",
            templateUrl: "/Templates/Organization/OrganizationProfiles",
            params: { organizationId: null },
            data: {
                pageTitle: 'profiles'
            }
        })
        .state('positions', {
            url: "/positions/:organizationId",
            templateUrl: "/Templates/Organization/OrganizationPositions",
            params: { organizationId: null },
            data: {
                pageTitle: 'positions'
            }
        });
}


function organizationController($scope, $http, $uibModal, $state, organizationFactory) {

    $scope.data = organizationFactory;
    var organizationTypes;

    organizationFactory.getOrganizationTypes()
        .then(function(result) {
            //succcess
                organizationTypes = result;
            },
            function() {
                //error
            });

    organizationFactory.getOrganizations()
        .then(function () {
            // success
        },
            function () {
                // error
                alert("Sorry! There was a problem loading organizations.  Please try again later.");
            });

    $scope.add = function () {
        var modalInstance = $uibModal.open({
            templateUrl: "/Templates/Organization/addOrganizationModal",
            controller: AddOrganizationModalController
        });
    };

    $scope.edit = function (organization) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Templates/Organization/editOrganizationModal",
            controller: EditOrganizationModalController,
            resolve: {
                organization: function () {
                    return angular.copy(organization);
                }
            }
        });
        modalInstance.result
                .then(function ()
                {
                    // Rebind the scope, which is funked up
                }).catch(function ()
                {
                    // Do nothing
                });
    };

    $scope.delete = function (organization) {
        var modalInstance = $uibModal.open({
            templateUrl: "/Templates/Organization/deleteOrganizationModal",
            controller: DeleteOrganizationModalController,
            resolve: {
                organization: function () {
                    return angular.copy(organization);
                }
            }
        });
    };

    $scope.users = function (organization) {
        $state.go('users', { 'organizationId': organization.id });
    };

    $scope.profiles = function (organization) {
        $state.go('profiles', { 'organizationId': organization.id });
    };

    $scope.roles = function (organization) {
        $state.go('roles', { 'organizationId': organization.id });
    };

    $scope.positions = function (organization) {
        $state.go('positions', { 'organizationId': organization.id });
    };
};

function AddOrganizationModalController($scope, $uibModalInstance, organizationFactory) {

    $scope.organizationTypes = organizationFactory.organizationTypes;

    $scope.ok = function(newOrganization) {

        // Set the values to the selected organization type
        newOrganization.organizationTypeId = newOrganization.selectedOrgType.organizationTypeId;
        newOrganization.organizationTypeName = newOrganization.selectedOrgType.name;

        organizationFactory.addOrganizations(newOrganization)
            .then(function (newlyCreatedOrganization) {
                // success
                newlyCreatedOrganization.organizationTypeName =
                    findOrganizationTypeName(organizationFactory.organizationTypes, newlyCreatedOrganization.organizationTypeId);
                },
                function() {
                    // error
                    alert("could not save organization");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    };

    function findOrganizationTypeName(orgTypes, orgIdToFind) {
        for (var i = 0; i < orgTypes.length; i++) {
            if (orgTypes[i].organizationTypeId === orgIdToFind) {
                return orgTypes[i].name;
            }
        }
        return "Not Found"; // Not found
    }
};

function EditOrganizationModalController($scope, $uibModalInstance, organizationFactory, organization) {

    $scope.organizationTypes = organizationFactory.organizationTypes;
    $scope.selectedOrgType = findSelectedOrganization($scope.organizationTypes, organization.organizationTypeId);

    organizationFactory.getOrganization(organization.id)
    .then(function (result) {
        $scope.organization = result;
    });

    $scope.ok = function (editOrganization)
    {
        // Set the values to the selected organization type
        editOrganization.organizationTypeId = editOrganization.selectedOrgType.organizationTypeId;
        editOrganization.organizationTypeName = editOrganization.selectedOrgType.name;
        editOrganization.organizationType = null;

        organizationFactory.editOrganizations(editOrganization)
            .then(function () {
                // success
                },
                function () {
                    // error
                    alert("could not edit or update organization");
                });

        $uibModalInstance.close();

    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    function findSelectedOrganization(orgTypes, orgIdToFind) {
        for (var i = 0; i < orgTypes.length; i++) {
            if (orgTypes[i].organizationTypeId === orgIdToFind) {
                return orgTypes[i];
            }
        }
        return -1; // Not found
    }
};

function DeleteOrganizationModalController($scope, $uibModalInstance, organizationFactory, organization) {

    $scope.organization = organization;

    $scope.ok = function () {

        organizationFactory.deleteOrganizations(organization)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not delete organization");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');

    };
};

/**
 * Pass function into module
 */
angular
    .module('app')
    .factory('organizationFactory', organizationFactory);

function organizationFactory($http, $q) {

    var _organization = {};
    var _organizations = [];
    var _organizationTypes = [];

    var _getOrganizationTypes = function() {
        var deferred = $q.defer();

        $http.get('/api/OrganizationType')
          .then(function (result) {
              // Successful
              angular.copy(result.data, _organizationTypes);
              deferred.resolve(_organizationTypes);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    }

    var _getOrganization = function (id) {

        var deferred = $q.defer();

        $http.get('/api/Organizations/' + id)
         .then(function (result) {
             // success
             angular.copy(result.data, _organization);
             deferred.resolve(_organization);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _getOrganizations = function () {

        var deferred = $q.defer();

        $http.get('/api/Organizations')
          .then(function (result) {
              // Successful
              angular.copy(result.data, _organizations);
              deferred.resolve(_organizations);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };


    var _addOrganization = function (newOrganization) {

        var local = newOrganization;

        var deferred = $q.defer();

        $http.post('/api/Organizations', newOrganization)
         .then(function (result) {
             // success
             var newlyCreatedOrganization = result.data;
             _organizations.splice(0, 0, newlyCreatedOrganization);
             deferred.resolve(newlyCreatedOrganization);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editOrganization = function (organization) {

        var deferred = $q.defer();

        $http.put('/api/Organizations/' + organization.id, organization)
         .then(function (result) {
             // success
             var editedOrganization = result.data;

             for (var i = 0; i < _organizations.length; i++) {
                 if (_organizations[i].id === editedOrganization.id) {
                     _organizations[i].name = editedOrganization.name;
                     _organizations[i].organizationTypeId = editedOrganization.organizationTypeId;
                     _organizations[i].organizationTypeName = editedOrganization.organizationType.name;

                     break;
                 }
             }

             deferred.resolve(editedOrganization);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _deleteOrganization = function (organization) {

        var deferred = $q.defer();

        $http.delete('/api/Organizations/' + organization.id, organization)
         .then(function (result) {

             var deletedOrganization = result.data;

             for (var i = 0; i < _organizations.length; i++) {
                 if (_organizations[i].id === deletedOrganization.id) {
                     _organizations.splice(i, 1);
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
        organizationTypes: _organizationTypes,
        getOrganizationTypes: _getOrganizationTypes,
        organization: _organization,
        organizations: _organizations,
        getOrganization: _getOrganization,
        getOrganizations: _getOrganizations,
        addOrganizations: _addOrganization,
        editOrganizations: _editOrganization,
        deleteOrganizations: _deleteOrganization
    };
};