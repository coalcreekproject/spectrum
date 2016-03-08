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

        // TODO: Get real organizations list
        $scope.organizations = [
            {
                id: 1,
                name: "Spectrum Operational"
            },
            {
                id: 2,
                name: "Adams County OEM"
            },
            {
                id: 3,
                name: "Boulder County OEM"
            }
        ];

        // TODO: Get real roles
        $scope.roles = [
            {
                roleId: 1,
                name: "Administrator"
            },
            {
                roleId: 2,
                name: "User"
            }
        ];

        // TODO: Get real positions
        $scope.positions = [
            {
                positionId: 1,
                name: "ESF-000"
            },
            {
                positionId: 2,
                name: "ESF-001"
            },
            {
                positionId: 3,
                name: "FF-911"
            }
        ];

        $scope.currentUser.selectedOrg = { id: currentUser.selectedOrganizationId };
        $scope.currentUser.selectedRole = { roleId: currentUser.selectedRoleId };
        $scope.currentUser.selectedPosition = { positionId: currentUser.positionId ? null : 1 };

        $scope.ok = function (currentUserData) {

            currentUserData.SelectedPositionId = currentUserData.selectedPosition.positionId;

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