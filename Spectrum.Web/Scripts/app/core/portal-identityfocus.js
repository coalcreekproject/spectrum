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
        $scope.organizations = [];
        $scope.roles = [];
        $scope.positions = [];
        var i;

        for (i = 0; i < currentUser.userOrganizations.length; i++) {
            $scope.organizations.push({
                organizationId: currentUser.userOrganizations[i].organizationId,
                name: currentUser.userOrganizations[i].name//,
                //selected: (currentUser.userOrganizations[i].organizationId === currentUser.selectedOrganizationId)
            });
        }

        for (i = 0; i < currentUser.userRoles.length; i++) {
            $scope.roles.push({
                roleId: currentUser.userRoles[i].roleId,
                name: currentUser.userRoles[i].name//,
                //selected: (currentUser.userRoles[i].roleId === currentUser.selectedRoleId)
            });
        }

        for (i = 0; i < currentUser.userPositions.length; i++) {
            $scope.positions.push({
                positionId: currentUser.userPositions[i].positionId,
                name: currentUser.userPositions[i].name//,
                //selected: (currentUser.userPositions[i].positionId === currentUser.SelectedPositionId)
            });
        }

        $scope.currentUser.selectedOrganization = { organizationId: currentUser.selectedOrganizationId, name: currentUser.selectedOrganizationName  };
        $scope.currentUser.selectedRole = { roleId: currentUser.selectedRoleId, name: currentUser.selectedRoleName };
        $scope.currentUser.selectedPosition = { positionId: currentUser.selectedPositionId, name: currentUser.selectedPositionName };

        $scope.ok = function (currentUserData) {

            //Check for nulls here and set to the already selected value if null
            currentUserData.selectedOrganizationId = currentUserData.selectedOrganization.organizationId;
            currentUserData.selectedRoleId = currentUserData.selectedRole.roleId;
            currentUserData.selectedPositionId = currentUserData.selectedPosition.positionId;

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