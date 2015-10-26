angular
    .module('app')
    .controller('OrganizationProfileController', organizationProfileController);

function organizationProfileController($scope, $http, $modal, organizationProfileFactory) {

    $modal.scope = $scope;

    $scope.data = organizationProfileFactory;

    organizationProfileFactory.getOrganizationProfiles()
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
            templateUrl: '/Templates/OrganizationProfile/addOrganizationProfileModal.html',
            controller: AddOrganizationProfileModalController
        });
    };

    $scope.edit = function (organization) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/OrganizationProfile/editOrganizationProfileModal.html',
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
            templateUrl: '/Templates/OrganizationProfile/deleteOrganizationProfileModal.html',
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

function AddOrganizationProfileModalController($scope, $modalInstance, organizationProfileFactory) {

    $scope.ok = function (organizationProfile) {

        organizationProfileFactory.addOrganizationProfiles(organizationProfile)
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

    $scope.organizationProfile = organizationProfile;

    $scope.ok = function () {

        organizationFactory.editOrganizationProfile(organizationProfile)
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

function DeleteOrganizationProfileModalController($scope, $modalInstance, organizationProfileFactory, organizationProfile) {

    $scope.organizationProfile = organizationProfile;

    $scope.ok = function () {

        organizationProfileFactory.deleteOrganizationProfile(organizationProfile)
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
        organizationProfiles: _organizationProfiles,
        getOrganizationProfiles: _getOrganizationProfile,
        addOrganizationProfiles: _addOrganizationProfile,
        editOrganizationProfiles: _editOrganizationProfile,
        deleteOrganizationProfiles: _deleteOrganizationProfile
    };
};