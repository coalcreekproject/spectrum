(function(app) {

    "use strict";

    app.controller("UserPanelController", UserPanelController);

    UserPanelController.$inject = ["$scope", "$http", "$uibModal", "$state", "UserService"];

    function UserPanelController($scope, $http, $uibModal, $state, UserService) {

        $uibModal.scope = $scope;

        $scope.data = UserService;

        UserService.getUsers()
        .then(function(users) {
                // success
                //$scope.data = users;
            },
            function() {
                // error
                alert("Sorry!", "There was a problem loading users.  Please try again later.", "error");
            });

        $scope.add = function() {
            $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: "/Templates/User/AddUserModal",
                controller: addUserModalController,
                size: "lg"
        });
    };

        $scope.edit = function(user) {
            $uibModal.open({
                templateUrl: "/Templates/User/EditUserModal",
                controller: editUserModalController,
            resolve: {
                user: function() {
                    return angular.copy(user);
                }
            }
        });
    };

        $scope.delete = function(user) {
            $uibModal.open({
                templateUrl: "/Templates/User/DeleteUserModal",
                controller: deleteUserModalController,
            resolve: {
                    user: function() {
                    return angular.copy(user);
                }
            }
        });
    };

        $scope.roles = function(user) {
            $uibModal.open({
                templateUrl: "/Templates/User/AssignUserRolesModal",
            controller: UserRolesModalController,
            resolve: {
                    user: function() {
                    return angular.copy(user);
                }
            }
        });
    };

        $scope.userprofiles = function(user) {
            $state.go("user-userprofiles", { 'userId': user.Id });
    };

        addUserModalController.$inject = ["$scope", "$uibModalInstance", "UserService"];

        function addUserModalController($scope, $uibModalInstance, UserService)
        {
            $scope.user = {};

    $scope.ok = function(user) {

                UserService.addUser(user)
            .then(function() {
                    // success
                            $uibModalInstance.close();
            },
                function() {
                    // error
                            alert("Could not save user");
                });

                $uibModalInstance.close();
    };

    $scope.cancel = function() {
                $uibModalInstance.dismiss("cancel");
    };
};

        addUserModalController.$inject = ["$scope", "$uibModalInstance", "UserService"];

        function editUserModalController($scope, $uibModalInstance, UserService) {

    $scope.user = user;

            $scope.ok = function() {

                UserService.editUser(user)
                    .then(function() {
                // success
            },
                        function() {
                    // error
                    alert("could not edit or update user");
                });

                $uibModalInstance.close();

    };

            $scope.cancel = function() {
                $uibModalInstance.dismiss("cancel");
    };
};

        addUserModalController.$inject = ["$scope", "$uibModalInstance", "UserService"];

        function deleteUserModalController($scope, $uibModalInstance, UserService) {

    $scope.user = user;

            $scope.ok = function() {

                UserService.deleteUser(user)
                    .then(function() {
                // success
            },
                        function() {
                    // error
                            alert("Could not delete user.");
          });

                $uibModalInstance.close();
    };

            $scope.cancel = function() {
                $uibModalInstance.dismiss("cancel");
    };
    };
    };

})(angular.module("app"));