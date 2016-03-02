angular
    .module('app')
    .controller('identityFocusController', identityFocusController);

function userFocusParameters() {
    this.userId = null;
    this.organizationId = null;
};

function identityFocusController($scope, $http, $modal) {

    $scope.changeFocus = function() {
        var modalInstance = $modal.open({  //modalInstance = orphan?
            templateUrl: '/Templates/Portal/ChangeUserFocusModal',
            controller: changeIdentityFocusModalController
        });
    }
}

function changeIdentityFocusModalController($scope, $http, $modalInstance) {

    $scope.userViewModel = {
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

    $scope.ok = function (userViewModel) {

        var model = userViewModel;

        // Do some work
        $http.post("/Portal/ChangeIdentityFocus", { userViewModel: userViewModel }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
        
        $modalInstance.close();
    };

    $scope.cancel = function() {
        $modalInstance.dismiss('cancel');
    };
}
