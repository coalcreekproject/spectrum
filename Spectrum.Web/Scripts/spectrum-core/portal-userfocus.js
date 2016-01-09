angular
    .module('app')
    .controller('userFocusController', userFocusController);

function userFocusParameters() {
    this.userId = null;
    this.organizationId = null;
};

function userFocusController($scope, $http, $modal, $state) {

    $scope.changeFocus = function() {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Portal/ChangeUserFocusModal',
            controller: changeUserFocusModalController
        });
    }
}

function changeUserFocusModalController($scope, $modalInstance) {

    $scope.ok = function(user) {

        // Do some work

        $modalInstance.close();
    };

    $scope.cancel = function() {
        $modalInstance.dismiss('cancel');
    };
}
