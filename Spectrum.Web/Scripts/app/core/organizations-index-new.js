(function() {

    angular
        .module('app')
        .controller('OrganizationController', organizationController)
        .config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider", "$compileProvider$stateProvider"];

    function config($stateProvider, $urlRouterProvider, $compileProvider) {
        
        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);
        
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

    organizationController.$inject = ["$scope", "$http", "$uibModal", "$state", "organizationFactory"];

    function organizationController($scope, $http, $uibModal, $state, organizationFactory) {

        var organizationTypes;

        organizationFactory.getOrganizationTypes()
            .then(function (result) {
                //succcess
                organizationTypes = result;
            },
                function () {
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
                controller: addOrganizationModalController
            });
        };

        $scope.edit = function (organization) {
            var modalInstance = $uibModal.open({
                templateUrl: "/Templates/Organization/editOrganizationModal",
                controller: editOrganizationModalController,
                resolve: {
                    organization: function () {
                        return angular.copy(organization);
                    }
                }
            });
        };

        $scope.delete = function (organization) {
            var modalInstance = $uibModal.open({
                templateUrl: "/Templates/Organization/deleteOrganizationModal",
                controller: deleteOrganizationModalController,
                resolve: {
                    organization: function () {
                        return angular.copy(organization);
                    }
                }
            });
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

    //addOrganizationModalController.$inject["$scope", "$uibModalInstance", "organizationFactory"];

    function addOrganizationModalController($scope, $uibModalInstance, organizationFactory) {

        $scope.organizationTypes = organizationFactory.organizationTypes;

        $scope.ok = function (organization) {

            organizationFactory.addOrganizations(organization)
                .then(function () {
                    // success
                    $uibModalInstance.close();

                },
                    function () {
                        // error
                        alert("could not save organization");
                    });

            $uibModalInstance.close();
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };

    //editOrganizationModalController.$inject["$scope", "$uibModalInstance", "organizationFactory", "organization"];

    function editOrganizationModalController($scope, $uibModalInstance, organizationFactory, organization) {

        $scope.organization = organization;

        $scope.ok = function () {

            organizationFactory.editOrganizations(organization)
                .then(function () {
                    // success
                    var local = organization;
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
    };

    //deleteOrganizationModalController.$inject["$scope", "$uibModalInstance", "organizationFactory", "organization"];

    function deleteOrganizationModalController($scope, $uibModalInstance, organizationFactory, organization) {

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
})();