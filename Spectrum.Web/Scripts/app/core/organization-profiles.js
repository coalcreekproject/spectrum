angular
    .module('app')
    .controller('OrganizationProfileController', organizationProfileController);

function organizationProfileParameters() {
    this.organizationId = null;
};

function organizationProfileController($scope, $http, $window, $uibModal, $stateParams, lookupDataService, organizationProfileFactory) {

    var languages = [];
    var timeZones = [];
    var usStates = [];
    var countries = [];

    $scope.organizationId = $stateParams.organizationId;
    organizationProfileParameters.organizationId = $scope.organizationId;

    $uibModal.scope = $scope;
    $scope.data = organizationProfileFactory;

    lookupDataService.getLanguages()
        .then(function(result) {
            languages = result;
        }, function () {
            // error
        });

    lookupDataService.getTimeZones()
        .then(function(result) {
            timeZones = result;
        }, function () {
            // error
        });

    lookupDataService.getStates()
        .then(function (result) {
            usStates = result;
        },function() {
            // error
        });

    lookupDataService.getCountries()
        .then(function(result) {
            countries = result;
        }, function() {
            // error
        });

    organizationProfileFactory.getOrganizationProfiles($scope.organizationId)
        .then(function() {
                // success
            },
            function() {
                // error
                alert("Sorry! There was a problem loading organization profiles.  Please try again later.");
            });

    $scope.add = function() {
        var modalInstance = $uibModal.open({
            templateUrl: '/Templates/Organization/addOrganizationProfileModal',
            controller: AddOrganizationProfileModalController,
            resolve: {
                languages: function() {
                    return angular.copy(languages);
                },
                timeZones: function() {
                    return angular.copy(timeZones);
                },
                usStates: function() {
                    return angular.copy(usStates);
                },
                countries: function () {
                    return angular.copy(countries);
                }
            }
        });
    };

    $scope.edit = function (organizationProfile) {
        var modalInstance = $uibModal.open({
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
        var modalInstance = $uibModal.open({
            templateUrl: '/Templates/Organization/deleteOrganizationProfileModal',
            controller: DeleteOrganizationProfileModalController,
            resolve: {
                organizationProfile: function () {
                    return angular.copy(organizationProfile);
                }
            }
        });
    };
};

function AddOrganizationProfileModalController($scope, $uibModalInstance, organizationProfileFactory, languages, timeZones, usStates, countries) {

    $scope.languages = languages;
    $scope.timeZones = timeZones;
    $scope.usStates = usStates;
    $scope.countries = countries;

    $scope.organizationProfile = {};

    $scope.organizationProfile.selectedLang = $scope.languages[0];
    $scope.organizationProfile.selectedTimeZone = $scope.timeZones[0];
    $scope.organizationProfile.selectedState = $scope.usStates[0];
    $scope.organizationProfile.selectedCountry = $scope.countries[0];

    $scope.ok = function (organizationProfile) {

        organizationProfile.organizationId = organizationProfileParameters.organizationId;

        organizationProfileFactory.addOrganizationProfiles(organizationProfile)
            .then(function() {
                // success

                },
                function() {
                    // error
                    alert("could not save organization profile");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};

function EditOrganizationProfileModalController($scope, $uibModalInstance, organizationProfileFactory, organizationProfile) {

    organizationProfileFactory.getOrganizationProfile(organizationProfile)
        .then(function(result) {
                $scope.organizationProfile = result;
            },
            function() {
                //Couldn't find it, stick the one passed in out there
                $scope.organizationProfile = organizationProfile;
            });

    $scope.ok = function() {

        organizationProfileFactory.editOrganizationProfiles($scope.organizationProfile)
            .then(function() {
                    // success
                },
                function() {
                    // error
                    alert("could not edit or update organization profile");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};

function DeleteOrganizationProfileModalController($scope, $uibModalInstance, organizationProfileFactory, organizationProfile) {

    organizationProfileFactory.getOrganizationProfile(organizationProfile)
        .then(function(result) {
                $scope.organizationProfile = result;
            },
            function() {
                //Couldn't find it, stick the one passed in out there
                $scope.organizationProfile = organizationProfile;
            });

    $scope.ok = function() {

        organizationProfileFactory.deleteOrganizationProfiles(organizationProfile)
            .then(function() {
                    // success
                },
                function() {
                    // error
                    alert("could not delete organization profile");
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
    .factory('organizationProfileFactory', organizationProfileFactory);

function organizationProfileFactory($http, $q) {

    var _organizationProfile = {};
    var _organizationProfiles = [];

    var _getOrganizationProfile = function (organizationProfile) {

        var deferred = $q.defer();

        $http.get('/api/OrganizationProfiles/?profileId=' + organizationProfile.id + '&' + 'organizationId=' + organizationProfile.organizationId)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _organizationProfile);
              deferred.resolve(_organizationProfile);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };

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

    var _addOrganizationProfile = function (organizationProfile) {

        organizationProfile.language = organizationProfile.selectedLang.name;
        organizationProfile.country = organizationProfile.selectedCountry.name;
        organizationProfile.timeZone = organizationProfile.selectedTimeZone.name;
        
        var deferred = $q.defer();

        $http.post('/api/OrganizationProfiles', organizationProfile)
         .then(function (result) {
             // success
             var newOrganizationProfile = result.data;
             _organizationProfiles.splice(0, 0, newOrganizationProfile);
             deferred.resolve(newOrganizationProfile);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editOrganizationProfile = function (organizationProfile) {

        var deferred = $q.defer();

        $http.put('/api/OrganizationProfiles/' + organizationProfile.id, organizationProfile)
         .then(function (result) {
             // success
             var editedOrganizationProfile = result.data;

             for (var i = 0; i < _organizationProfiles.length; i++) {
                 if (_organizationProfiles[i].id === editedOrganizationProfile.id) {
                     _organizationProfiles[i] = editedOrganizationProfile;
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

    var _deleteOrganizationProfile = function(organizationProfile) {

        var deferred = $q.defer();

        $http.delete('/api/OrganizationProfiles/' + organizationProfile.id)
            .then(function(result) {

                    var deletedOrganizationProfile = result.data;

                    for (var i = 0; i < _organizationProfiles.length; i++) {
                        if (_organizationProfiles[i].id === deletedOrganizationProfile.id) {
                            _organizationProfiles.splice(i, 1);
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
        organizationProfile: _organizationProfile,
        organizationProfiles: _organizationProfiles,
        getOrganizationProfile: _getOrganizationProfile,
        getOrganizationProfiles: _getOrganizationProfiles,
        addOrganizationProfiles: _addOrganizationProfile,
        editOrganizationProfiles: _editOrganizationProfile,
        deleteOrganizationProfiles: _deleteOrganizationProfile
    };
};