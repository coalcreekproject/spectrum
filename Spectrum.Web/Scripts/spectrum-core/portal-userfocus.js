(function(app) {

    "use strict";

    app.controller("userFocusController", userFocusController);

    userFocusController.$inject = ["$scope", "$http", "$uibModal"];

    function userFocusController($scope, $http, $uibModal) {

        $scope.changeFocus = function() {
            $uibModal.open({
                templateUrl: "/Templates/Portal/ChangeUserFocusModal",
                controller: changeUserFocusModalController
            });
        };

        // Change User Focus Modal
        changeUserFocusModalController.$inject = ["$scope", "$uibModalInstance"];

        function changeUserFocusModalController($scope, $uibModalInstance) {

            $scope.ok = function(user) {
                //TODO: Call the UserFocus service and change the user
                $uibModalInstance.close();
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss("cancel");
            };
        }
    }

})(angular.module("app"));