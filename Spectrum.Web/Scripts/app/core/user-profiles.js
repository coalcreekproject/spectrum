angular
    .module('app')
    .controller('userProfileController', userProfileController);

function userProfileParameters() {
    this.userId = null;
};

function userProfileController($scope, $http, $window, $uibModal, $stateParams, lookupDataService, userProfileFactory) {

    var languages = [];
    var timeZones = [];
    var usStates = [];
    var countries = [];

    $scope.userId = $stateParams.userId;
    userProfileParameters.userId = $stateParams.userId;
    $scope.data = userProfileFactory;

    lookupDataService.getLanguages()
        .then(function (result) {
            languages = result;
        }, function () {
            // error
        });

    lookupDataService.getTimeZones()
        .then(function (result) {
            timeZones = result;
        }, function () {
            // error
        });

    lookupDataService.getStates()
        .then(function (result) {
            usStates = result;
        }, function () {
            // error
        });

    lookupDataService.getCountries()
        .then(function (result) {
            countries = result;
        }, function () {
            // error
        });

    userProfileFactory.getUserProfiles($scope.userId)
        .then(function(userProfiles) {
                // success
            },
            function() {
                // error
                alert("could not load user profiles");
            });

    $scope.add = function () {
        var modalInstance = $uibModal.open({
            templateUrl: '/Templates/User/AddUserProfileModal',
            controller: AddUserProfileModalController,
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

    $scope.edit = function (userProfile) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Templates/User/EditUserProfileModal',
            controller: EditUserProfileModalController,
            resolve: {
                userProfile: function () {
                    return angular.copy(userProfile);
                }
            }
        });
    };

    $scope.delete = function (userProfile) {
        var modalInstance = $uibModal.open({
            templateUrl: '/Templates/User/DeleteUserProfileModal',
            controller: DeleteUserProfileModalController,
            resolve: {
                userProfile: function () {
                    return angular.copy(userProfile);
                }
            }
        });
    };
};

function AddUserProfileModalController($scope, $uibModalInstance, userProfileFactory, languages, timeZones, usStates, countries) {

    $scope.languages = languages;
    $scope.timeZones = timeZones;
    $scope.usStates = usStates;
    $scope.countries = countries;

    $scope.userProfile = {};

    $scope.userProfile.selectedLang = $scope.languages[0];
    $scope.userProfile.selectedTimeZone = $scope.timeZones[0];
    $scope.userProfile.selectedState = $scope.usStates[0];
    $scope.userProfile.selectedCountry = $scope.countries[0];

    $scope.ok = function(userProfile) {

        userProfile.userId = userProfileParameters.userId;

        userProfileFactory.addUserProfile(userProfile)
            .then(function() {
                    // success
                },
                function() {
                    // error
                    alert("could not save the profile.");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    };
};

function EditUserProfileModalController($scope, $uibModalInstance, userProfileFactory, userProfile) {

    userProfileFactory.getUserProfile(userProfile)
        .then(function(result) {
                $scope.userProfile = result;
            },
            function() {
                //Couldn't find it, stick the one passed in out there
                $scope.userProfile = userProfile;
            });

    $scope.ok = function() {

        userProfileFactory.editUserProfile($scope.userProfile)
            .then(function() {
                    // success
                },
                function() {
                    // error
                    alert("could not edit or update profile");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    };
};

function DeleteUserProfileModalController($scope, $uibModalInstance, userProfileFactory, userProfile) {

    userProfileFactory.getUserProfile(userProfile)
        .then(function(result) {
                $scope.userProfile = result;
            },
            function() {
                //Couldn't find it, stick the one passed in out there
                $scope.userProfile = userProfile;
            });

    $scope.ok = function () {

        userProfileFactory.deleteUserProfile(userProfile)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not delete profile");
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
    .factory('userProfileFactory', userProfileFactory);

function userProfileFactory($http, $q) {

    var _userProfile = {};
    var _userProfiles = [];

    var _getUserProfile = function (userProfile) {

        var deferred = $q.defer();

        $http.get('/api/UserProfiles/?profileId=' + userProfile.id + '&' + 'userId=' + userProfile.userId)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _userProfile);
              deferred.resolve(_userProfile);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };

    var _getUserProfiles = function (id) {

        var deferred = $q.defer();

        $http.get('/api/UserProfiles/' + id)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _userProfiles);
              deferred.resolve(_userProfiles);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };

    var _addUserProfile = function (userProfile) {

        userProfile.language = userProfile.selectedLang.name;
        userProfile.country = userProfile.selectedCountry.name;
        userProfile.timeZone = userProfile.selectedTimeZone.name;

        var deferred = $q.defer();

        $http.post('/api/UserProfiles', userProfile)
         .then(function (result) {
             // success
             var newUserProfile = result.data;
             _userProfiles.splice(0, 0, newUserProfile);
             deferred.resolve(newUserProfile);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editUserProfile = function (userProfile) {

        var deferred = $q.defer();

        $http.put('/api/UserProfiles/' + userProfile.id, userProfile)
         .then(function (result) {
             // success
             var editedUserProfile = result.data;

             for (var i = 0; i < _userProfiles.length; i++) {
                 if (_userProfiles[i].id === editedUserProfile.id) {
                     _userProfiles[i] = editedUserProfile;
                     break;
                 }
             }

             deferred.resolve(editedUserProfile);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _deleteUserProfile = function (userProfile) {

        var deferred = $q.defer();

        $http.delete('/api/UserProfiles/' + userProfile.id, userProfile)
         .then(function (result) {

             var deletedUserProfile = result.data;

             for (var i = 0; i < _userProfiles.length; i++) {
                 if (_userProfiles[i].id === deletedUserProfile.id) {
                     _userProfiles.splice(i, 1);
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
        userProfile: _userProfile,
        userProfiles: _userProfiles,
        getUserProfile: _getUserProfile,
        getUserProfiles: _getUserProfiles,
        addUserProfile: _addUserProfile,
        editUserProfile: _editUserProfile,
        deleteUserProfile: _deleteUserProfile
    };
};