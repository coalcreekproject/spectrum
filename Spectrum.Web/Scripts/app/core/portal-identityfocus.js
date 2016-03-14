(function () {
    angular
        .module("app")
        .controller("identityFocusController", identityFocusController);

    identityFocusController.$inject = ["$scope", "$http", "$uibModal", "currentUserFactory"];

    function identityFocusController($scope, $http, $uibModal, currentUserFactory) {

        // Controller declarations
        $scope.currentUser = {};
        $scope.changeFocus = changeFocus;

        // Activate page
        activate();

        function activate() {
            getCurrentUser();
        }

        function getCurrentUser() {
            currentUserFactory.getCurrentUser()
                .then(function (result) {
                    $scope.currentUser = result;
                },
                    function () {
                        //error NOTE: need to change this to SignalR or some messaging engine.
                        alert("Sorry! There was a problem loading the current user.  Please try again later.", "error");
                    });
        }

        function changeFocus() {

            var changeFocusDialog = $uibModal.open({
                templateUrl: "/Templates/Portal/ChangeUserFocusModal",
                controller: changeIdentityFocusModalController,
                resolve: {
                    currentUser: function () {
                        return $scope.currentUser;
                    }
                }
            });
            changeFocusDialog.result.finally(function () {
                getCurrentUser();
            });
        }
    }

    changeIdentityFocusModalController.$inject = ["$scope", "$http", "$uibModalInstance", "currentUserFactory", "currentUser"];

    function changeIdentityFocusModalController($scope, $http, $uibModalInstance, currentUserFactory, currentUser) {

        $scope.currentUser = currentUser;
        $scope.organizations = currentUser.userOrganizations;
        $scope.roles = currentUser.userRoles;
        $scope.positions = currentUser.userPositions;

        $scope.ok = function (currentUserData) {

            // Edit the current user
            currentUserFactory.editCurrentUser(currentUserData)
                .then(function () {
                    $uibModalInstance.close();
                },
                    function () {
                        alert("Sorry! There was a problem setting the identity focus.  Please try again later.", "error");
                    });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss("cancel");
        };
    };
})
();