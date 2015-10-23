angular
    .module('app')
    .controller('OrganizationProfileController', organizationProfileController);

function organizationController($scope, $http, $modal, organizationFactory) {

    $modal.scope = $scope;

    $scope.data = organizationFactory;

    organizationFactory.getOrganizations()
        .then(function (organizations) {
            // success
            //$scope.data = organization;
        },
            function () {
                // error
                alert("Sorry! There was a problem loading organization profiles.  Please try again later.");
            });

    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/addOrganizationProfileModal.html',
            controller: AddOrganizationProfileModalController
        });
    };

    $scope.edit = function (organization) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/editOrganizationProfileModal.html',
            controller: EditOrganizationProfileModalController,
            resolve: {
                organization: function () {
                    return angular.copy(organization);
                }
            }
        });
    };

    $scope.delete = function (organization) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/deleteOrganizationProfileModal.html',
            controller: DeleteOrganizationProfileModalController,
            resolve: {
                organization: function () {
                    return angular.copy(organization);
                }
            }
        });
    };

    $scope.profiles = function (organization) {
        window.location = "/OrganizationProfiles/Index/" + organization.Id;
    };
};

function AddOrganizationProfileModalController($scope, $modalInstance, organizationFactory) {

    $scope.ok = function (organization) {

        organizationFactory.addOrganizations(organization)
            .then(function () {
                // success
                $modalInstance.close();
            },
                function () {
                    // error
                    alert("could not save organization profile");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

function EditOrganizationProfileProfileModalController($scope, $modalInstance, organizationProfileFactory, organizationProfile) {

    $scope.organization = organization;

    $scope.ok = function () {

        organizationFactory.editOrganization(organization)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not edit or update organization profile");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

function DeleteOrganizationProfileModalController($scope, $modalInstance, organizationFactory, organization) {

    $scope.organization = organization;

    $scope.ok = function () {

        organizationFactory.deleteOrganizations(organization)
            .then(function () {
                // success

            },
                function () {
                    // error
                    alert("could not delete organization profile");
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
    .factory('organizationProfileFactory', organizationProfileFactory);

function organizationProfileFactory($http, $q) {

    var _organizationProfiles = [];

    var _getOrganizationProfile = function () {

        var deferred = $q.defer();

        $http.get('/api/OrganizationProfiles')
          .then(function (result) {
              // Successful
              angular.copy(result.data, _organizationProfiles);
              deferred.resolve(_organizationProfiles);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };


    var _addOrganizationProfile = function (newOrganizationProfile) {

        var deferred = $q.defer();

        $http.post('/api/OrganizationProfiles', newOrganizationProfile)
         .then(function (result) {
             // success
             var newlyCreatedOrganizationProfile = result.data;
             _organizationProfiles.splice(0, 0, newlyCreatedOrganization);
             deferred.resolve(newlyCreatedOrganizationProfile);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editOrganizationProfile = function (organizationProfile) {

        var deferred = $q.defer();

        $http.put('/api/OrganizationProfiles/' + organizationProfile.Id, organizationProfile)
         .then(function (result) {
             // success
             var editedOrganizationProfile = result.data;

             for (var i = 0; i < _organizationProfiles.length; i++) {
                 if (_organizationProfiles[i].Id === editedOrganizationProfile.Id) {
                     _organizationProfiles[i].Name = editedOrganizationProfile.Name;
                     _organizationProfiles[i].TypeId = editedOrganizationProfile.TypeId;
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

    var _deleteOrganizationProfile = function (organizationProfile) {

        var deferred = $q.defer();

        $http.delete('/api/OrganizationProfiles/' + organizationProfile.Id, organizationProfile)
         .then(function (result) {

             var deletedOrganizationProfile = result.data;

             for (var i = 0; i < _organizations.length; i++) {
                 if (_organizationProfiles[i].Id === deletedOrganizationProfile.Id) {
                     _organizationProfiles.splice(i, 1);
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
        organizationProfiles: _organizationProfile,
        getOrganizations: _getOrganizationProfile,
        addOrganizations: _addOrganizationProfile,
        editOrganizations: _editOrganizationProfile,
        deleteOrganizations: _deleteOrganizationProfile
    };
};