angular
    .module('app')
    .controller('identityFocusController', identityFocusController);

function identityFocusParameters() {
    this.userId = null;
    this.organizationId = null;
};

function identityFocusController($scope, $http, $modal, currentUserFactory) {

    //$modal.scope = $scope;
    $scope.data = currentUserFactory;

    currentUserFactory.getCurrentUser()
        .then(function(result) {
                $scope.currentUser = result;
            },
            function() {
                //error NOTE: need to change this to SignalR or some messaging engine.
                alert("Sorry! There was a problem loading the current user.  Please try again later.", "error");
            });

    $scope.changeFocus = function() {
        var modalInstance = $modal.open({  //modalInstance = orphan?
            templateUrl: '/Templates/Portal/ChangeUserFocusModal',
            controller: changeIdentityFocusModalController,
            resolve: {
                currentUser: function() {
                    return angular.copy($scope.currentUser);
                }
            }
        });
    }
}

function changeIdentityFocusModalController($scope, $http, $modalInstance, currentUserFactory, currentUser) {

    $scope.currentUser = currentUser;

    $scope.ok = function (userViewModel) {

        //do some work with the new factory
        currentUserFactory.editCurrentUser(userViewModel)
            .then(function(result) {
                    $scope.currentUser = result;
                },
                function() {
                    //error
                    alert("Sorry! There was a problem loading the current user.  Please try again later.", "error");
                });


        $modalInstance.close();
    };

    $scope.cancel = function() {
        $modalInstance.dismiss('cancel');
    };
}

/**
 * Pass function into module
 */
angular
    .module('app')
    .factory('currentUserFactory', currentUserFactory);

function currentUserFactory($http, $q) {

    var _currentUser = {
        Id: null,
        UserName: null,
        Email: null,
        SelectedOrganizationId: null,
        SelectedOrganizationName: null,
        SelectedRoleId: null,
        SelectedRoleName: null,
        SelectedPositionId: null,
        SelectedPositionName: null,
        UserOrganizations: null,
        UserRoles: null,
        UserProfiles: null,
        UserPositions: null
    };

    var _getCurrentUser = function () {

        var deferred = $q.defer();

        $http.get('/api/IdentityFocus/')
          .then(function (result) {
              // Successful
              angular.copy(result.data, _currentUser);
              deferred.resolve(_currentUser);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };

    var _editCurrentUser = function (currentUser) {

        var deferred = $q.defer();

        $http.post('/api/IdentityFocus/', currentUser)
         .then(function (result) {
             // success
             var editedUser = result.data;

             //currentUser.SelectedOrganizationId = editedUser.SelectedOrganizationId;
             //currentUser.SelectedOrganizationName = editedUser.SelectedOrganizationName;
             //currentUser.SelectedRoleId = editedUser.SelectedRoleId;
             //currentUser.SelectedRoleName = editedUser.SelectedRoleName;
             //currentUser.SelectedPositionId = editedUser.SelectedPositionId;
             //currentUser.SelectedPositionName = editedUser.SelectedPositionName;

             deferred.resolve(editedUser);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    return {
        currentUser: _currentUser,
        getCurrentUser: _getCurrentUser,
        editCurrentUser: _editCurrentUser
    };
};
