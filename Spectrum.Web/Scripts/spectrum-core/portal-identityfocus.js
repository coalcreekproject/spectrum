angular
    .module('app')
    .controller('identityFocusController', identityFocusController);

function identityFocusParameters() {
    this.userId = null;
    this.organizationId = null;
};

function identityFocusController($scope, $http, $modal, currentUserFactory) {

    $scope.data = currentUserFactory;

    currentUserFactory.getUser()
        .then(function() {
                //success
            },
            function() {
                //error
                alert("Sorry!", "There was a problem loading this user.  Please try again later.", "error");
            });

    $scope.changeFocus = function() {
        var modalInstance = $modal.open({  //modalInstance = orphan?
            templateUrl: '/Templates/Portal/ChangeUserFocusModal',
            controller: changeIdentityFocusModalController
        });
    }
}

function changeIdentityFocusModalController($scope, $http, $modalInstance, currentUserFactory) {

    $scope.userViewModel = null;

    $scope.ok = function (userViewModel) {
        





        
        $modalInstance.close();
    };

    $scope.cancel = function() {
        $modalInstance.dismiss('cancel');
    };
}

function currentUserFactory($http, $q) {

    var _currentUser;

    var _getCurrentUser = function (id) {

        var deferred = $q.defer();

        $http.get('/api/IdentityFocus/' + id)
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

        $http.put('/api/IdentityFocus/', currentUser)
         .then(function (result) {
             // success
             var editedUser = result.data;

             currentUser.SelectedOrganizationId = editedUser.SelectedOrganizationId;
             currentUser.SelectedOrganizationName = editedUser.SelectedOrganizationName;
             currentUser.SelectedRoleId = editedUser.SelectedRoleId;
             currentUser.SelectedRoleName = editedUser.SelectedRoleName;
             currentUser.SelectedPositionId = editedUser.SelectedPositionId;
             currentUser.SelectedPositionName = editedUser.SelectedPositionName;

             deferred.resolve(editedUser);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    return {
        user: _currentUser,
        getUser: _getCurrentUser,
        editUser: _editCurrentUser
    };
};
