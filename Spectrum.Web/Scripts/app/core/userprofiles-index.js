angular
    .module('app')
    .controller('UserProfileController', userProfileController)
    .service('UserProfileParameters', userProfileParameters);

function userProfileParameters() {
    this.userId = null;
};

function userProfileController($scope, $http, $window, $uibModal, $stateParams, userProfileFactory) {

    $scope.userId = $stateParams.userId;
    userProfileParameters.userId = $scope.userId;

    $uibModal.scope = $scope;
    $scope.data = userProfileFactory;

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
            controller: AddUserProfileModalController
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

function AddUserProfileModalController($scope, $uibModalInstance, userProfileFactory) {

    $scope.ok = function (userProfile) {

        userProfile.UserId = userProfileParameters.userId;

        userProfileFactory.addUserProfile(userProfile)
            .then(function () {
                // success
                // TODO: Saved message
                $uibModalInstance.close();
            },
                function () {
                    // error
                    alert("could not save the profile.");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};

function EditUserProfileModalController($scope, $uibModalInstance, userProfileFactory, userProfile) {

    $scope.userProfile = userProfile;

    $scope.ok = function () {

        userProfileFactory.editUserProfile(userProfile)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not edit or update profile");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};

function DeleteUserProfileModalController($scope, $uibModalInstance, userProfileFactory, userProfile) {

    $scope.userProfile = userProfile;

    $scope.ok = function () {

        userProfileFactory.deleteUserProfile(userProfile)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not delete user");
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

    var _userProfiles = [];

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

    var _addUserProfile = function (newUserProfile) {

        var deferred = $q.defer();

        $http.post('/api/UserProfiles', newUserProfile)
         .then(function (result) {
             // success
             var newlyCreatedUserProfile = result.data;
             _userProfiles.splice(0, 0, newlyCreatedUserProfile);
             deferred.resolve(newlyCreatedUserProfile);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editUserProfile = function (userProfile) {

        var deferred = $q.defer();

        $http.put('/api/UserProfiles/' + userProfile.Id, userProfile)
         .then(function (result) {
             // success
             var editedUserProfile = result.data;

             for (var i = 0; i < _userProfiles.length; i++) {
                 if (_userProfiles[i].Id === editedUserProfile.Id) {
                     _userProfiles[i].UserId = editedUserProfile.UserId;
                     _userProfiles[i].OrganizationId = editedUserProfile.OrganizationId;

                     _userProfiles[i].Default = editedUserProfile.Default;
                     _userProfiles[i].ProfileName = editedUserProfile.ProfileName;
                     _userProfiles[i].Title = editedUserProfile.Title;
                     _userProfiles[i].FirstName = editedUserProfile.FirstName;
                     _userProfiles[i].MiddleName = editedUserProfile.MiddleName;
                     _userProfiles[i].LastName = editedUserProfile.LastName;
                     _userProfiles[i].NickName = editedUserProfile.NickName;

                     _userProfiles[i].SecondaryEmail = editedUserProfile.SecondaryEmail;
                     _userProfiles[i].SecondaryPhoneNumber = editedUserProfile.SecondaryPhoneNumber;

                     _userProfiles[i].TimeZone = editedUserProfile.TimeZone;
                     _userProfiles[i].DstAdjust = editedUserProfile.DstAdjust;
                     _userProfiles[i].Language = editedUserProfile.Language;

                     _userProfiles[i].Photo = editedUserProfile.Photo;
                     _userProfiles[i].Position = editedUserProfile.Position;
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

        $http.delete('/api/UserProfiles/' + userProfile.Id, userProfile)
         .then(function (result) {

             var deletedUserProfile = result.data;

             for (var i = 0; i < _userProfiles.length; i++) {
                 if (_userProfiles[i].Id === deletedUserProfile.Id) {
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
        userProfiles: _userProfiles,
        getUserProfiles: _getUserProfiles,
        addUserProfile: _addUserProfile,
        editUserProfile: _editUserProfile,
        deleteUserProfile: _deleteUserProfile
    };
};