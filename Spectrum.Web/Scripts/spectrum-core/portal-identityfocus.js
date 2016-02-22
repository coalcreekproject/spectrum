angular
    .module('app')
    .controller('identityFocusController', identityFocusController);

function userFocusParameters() {
    this.userId = null;
    this.organizationId = null;
};

function identityFocusController($scope, $http, $modal, $state) {

    $scope.changeFocus = function() {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Portal/ChangeUserFocusModal',
            controller: changeIdentityFocusModalController
        });
    }
}

function changeIdentityFocusModalController($scope, $modalInstance) {

    $scope.ok = function(user) {

        // Do some work

        $modalInstance.close();
    };

    $scope.cancel = function() {
        $modalInstance.dismiss('cancel');
    };
}
