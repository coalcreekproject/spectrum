angular
    .module('app')
    .controller('OrganizationProfileController', organizationProfileController);

function organizationProfileParameters() {
    this.organizationId = null;
};

function organizationProfileController($scope, $http, $window, $modal, $stateParams, organizationProfileFactory) {

    $scope.organizationId = $stateParams.organizationId;
    organizationProfileParameters.organizationId = $scope.organizationId;

    $modal.scope = $scope;
    $scope.data = organizationProfileFactory;

    organizationProfileFactory.getOrganizationProfiles($scope.organizationId)
        .then(function (organizationProfiles) {
            // success
            //$scope.data = organization;
        },
            function () {
                // error
                alert("Sorry! There was a problem loading organization profiles.  Please try again later.");
            });

    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/addOrganizationProfileModal',
            controller: AddOrganizationProfileModalController
        });
    };

    $scope.edit = function (organizationProfile) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/EditOrganizationProfileModal',
            controller: EditOrganizationProfileModalController,
            resolve: {
                organizationProfile: function () {
                    return angular.copy(organizationProfile);
                }
            }
        });
    };

    $scope.delete = function (organizationProfile) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/deleteOrganizationProfileModal',
            controller: DeleteOrganizationProfileModalController,
            resolve: {
                organizationProfile: function () {
                    return angular.copy(organizationProfile);
                }
            }
        });
    };
}};

function AddOrganizationProfileModalController($scope, $modalInstance, organizationProfileFactory) {

    $scope.ok = function (organizationProfile) {

        organizationProfile.OrganizationId = organizationProfileParameters.organizationId;

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

function EditOrganizationProfileModalController($scope, $modalInstance, organizationProfileFactory, organizationProfile) {

    $scope.organizationProfile = organizationProfile;

    $scope.ok = function () {

        organizationProfileFactory.editOrganizationProfiles(organizationProfile)
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

        organizationProfileFactory.deleteOrganizationProfiles(organizationProfile)
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

    var _getOrganizationProfiles = function (id) {

        var deferred = $q.defer();

        $http.get('/api/OrganizationProfiles/' + id)
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
             _organizationProfiles.splice(0, 0, newOrganizationProfile);
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
                     _organizationProfiles[i].Default = editedOrganizationProfile.Default;
                     _organizationProfiles[i].Name = editedOrganizationProfile.Name;
                     _organizationProfiles[i].Description = editedOrganizationProfile.Description;
                     _organizationProfiles[i].PrimaryContact = editedOrganizationProfile.PrimaryContact;
                     _organizationProfiles[i].Phone = editedOrganizationProfile.Phone;
                     _organizationProfiles[i].Fax = editedOrganizationProfile.Fax;
                     _organizationProfiles[i].Email = editedOrganizationProfile.Email;
                     _organizationProfiles[i].Country = editedOrganizationProfile.Country;
                     _organizationProfiles[i].County = editedOrganizationProfile.County;
                     _organizationProfiles[i].TimeZone = editedOrganizationProfile.TimeZone;
                     _organizationProfiles[i].Language = editedOrganizationProfile.Language;
                     _organizationProfiles[i].Notes = editedOrganizationProfile.Notes;

                     //_organizationProfiles[i].val = editedOrganizationProfile.val;

                     break;
                 }
             }

             deferred.resolve(editedOrganizationProfile);
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
        getOrganizationProfiles: _getOrganizationProfiles,
        addOrganizationProfiles: _addOrganizationProfile,
        editOrganizationProfiles: _editOrganizationProfile,
        deleteOrganizationProfiles: _deleteOrganizationProfile
    };
};