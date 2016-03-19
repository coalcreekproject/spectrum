(function() {
    "use strict";

    angular
        .module('app')
        .controller('OrganizationController', organizationController)
        .config(config);

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

    organizationController.$inject = ["$scope", "$http", "$uibModal", "organizationFactory"];

    function organizationController($scope, $http, $uibModal, $state, organizationFactory) {

        $uibModal.scope = $scope;

        $scope.data = organizationFactory;
        $scope.organizationTypes = organizationFactory.getOrganizationTypes;

        organizationFactory.getOrganizations()
            .then(function(organizations) {
                    // success
                    //$scope.data = organization;
                },
                function() {
                    // error
                    alert("Sorry! There was a problem loading organizations.  Please try again later.");
                });

        $scope.add = function() {
            var modalInstance = $uibModal.open({
                templateUrl: "/Templates/Organization/addOrganizationModal",
                controller: AddOrganizationModalController,
                resolve: {
                    organizationTypes: function() {
                        return angular.copy($scope.organizationTypes);
                    }
                }
            });
        };

        $scope.edit = function(organization) {
            var modalInstance = $uibModal.open({
                templateUrl: "/Templates/Organization/editOrganizationModal",
                controller: EditOrganizationModalController,
                resolve: {
                    organization: function() {
                        return angular.copy(organization);
                    },
                    organizationTypes: function() {
                        return angular.copy($scope.organizationTypes);
                    }
                }
            });
        };

        $scope.delete = function(organization) {
            var modalInstance = $uibModal.open({
                templateUrl: "/Templates/Organization/deleteOrganizationModal",
                controller: DeleteOrganizationModalController,
                resolve: {
                    organization: function() {
                        return angular.copy(organization);
                    }
                }
            });
        };

        $scope.profiles = function(organization) {
            $state.go('profiles', { 'organizationId': organization.id });
        };

        $scope.roles = function(organization) {
            $state.go('roles', { 'organizationId': organization.id });
        };

        $scope.positions = function(organization) {
            $state.go('positions', { 'organizationId': organization.id });
        };
    };

    AddOrganizationModalController.$inject = ["$scope", "$http", "$uibModal", "organizationFactory", "organizationTypes"];

    function AddOrganizationModalController($scope, $uibModalInstance, organizationFactory, organizationTypes) {

        $scope.organizationTypes = organizationTypes;
        $scope.ok = function(organization) {

            organizationFactory.addOrganizations(organization)
                .then(function() {
                        // success
                        $uibModalInstance.close();

                    },
                    function() {
                        // error
                        alert("Could not save organization");
                    });

            $uibModalInstance.close();
        };

        $scope.cancel = function() {
            $uibModalInstance.dismiss('cancel');

        };
    };

    EditOrganizationModalController.$inject = ["$scope", "$http", "$uibModal", "organizationFactory", "organization", "organizationTypes"];

    function EditOrganizationModalController($scope, $uibModalInstance, organizationFactory, organization, organizationTypes) {

        $scope.organization = organization;
        $scope.organizationTypes = organizationTypes;
        //$scope.organizationTypeId = organization.organizationTypeId;

        $scope.ok = function() {

            organizationFactory.editOrganizations(organization)
                .then(function() {
                        // success
                    },
                    function() {
                        // error
                        alert("Could not edit or update organization");
                    });

            $uibModalInstance.close();

        };

        $scope.cancel = function() {
            $uibModalInstance.dismiss('cancel');
        };
    };

    DeleteOrganizationModalController.$inject = ["$scope", "$http", "$uibModal", "organizationFactory", "organization", "organizationTypes"];

    function DeleteOrganizationModalController($scope, $uibModalInstance, organizationFactory, organization) {

        $scope.organization = organization;

        $scope.ok = function() {

            organizationFactory.deleteOrganizations(organization)
                .then(function() {
                        // success

                    },
                    function() {
                        // error
                        alert("Could not delete organization");
                    });

            $uibModalInstance.close();

        };

        $scope.cancel = function() {
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

    var _organizations = [];
    var _organizationTypes = [];

    var _getOrganizationTypes = function() {

        var deferred = $q.defer();

        $http.get('/api/OrganizationTypes')
            .then(function(result) {
                    // Successful
                    angular.copy(result.data, _organizationTypes);
                    deferred.resolve(_organizationTypes);
                },
                function() {
                    // Error
                    deferred.reject();
                });

        return deferred.promise;
    };

    var _getOrganizations = function() {

        var deferred = $q.defer();

        $http.get('/api/Organizations')
            .then(function(result) {
                    // Successful
                    angular.copy(result.data, _organizations);
                    deferred.resolve(_organizations);
                },
                function() {
                    // Error
                    deferred.reject();
                });

        return deferred.promise;
    };

    var _addOrganization = function(newOrganization) {

        var deferred = $q.defer();

        $http.post('/api/Organizations', newOrganization)
            .then(function(result) {
                    // success
                    var newlyCreatedOrganization = result.data;
                    _organizations.splice(0, 0, newlyCreatedOrganization);
                    deferred.resolve(newlyCreatedOrganization);
                },
                function() {
                    // error
                    deferred.reject();
                });

        return deferred.promise;
    };

    var _editOrganization = function(organization) {

        var deferred = $q.defer();

        $http.put('/api/Organizations/' + organization.id, organization)
            .then(function(result) {
                    // success
                    var editedOrganization = result.data;

                    for (var i = 0; i < _organizations.length; i++) {
                        if (_organizations[i].id === editedOrganization.id) {
                            _organizations[i].name = editedOrganization.name;
                            _organizations[i].organizationTypeId = editedOrganization.organizationTypeId;
                            _organizations[i].organizationTypeName = editedOrganization.organizationType.name;
                            _organizations[i].organizationType = editedOrganization.organizationType;
                            break;
                        }
                    }

                    deferred.resolve(editedOrganization);
                },
                function() {
                    // error
                    deferred.reject();
                });

        return deferred.promise;
    };

    var _deleteOrganization = function(organization) {

        var deferred = $q.defer();

        $http.delete('/api/Organizations/' + organization.Id, organization)
            .then(function(result) {

                    var deletedOrganization = result.data;

                    for (var i = 0; i < _organizations.length; i++) {
                        if (_organizations[i].id === deletedOrganization.id) {
                            _organizations.splice(i, 1);
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
        getOrganizationTypes: _getOrganizationTypes,
        organizations: _organizations,
        getOrganizations: _getOrganizations,
        addOrganizations: _addOrganization,
        editOrganizations: _editOrganization,
        deleteOrganizations: _deleteOrganization
    };
};
})();