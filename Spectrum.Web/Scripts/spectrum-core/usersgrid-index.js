angular
    .module('app')
    .controller('UserGridController', userGridController);

function userGridController($scope, $http, $location, $modal, $state, uiGridConstants, userFactory) {

    $scope.data = userFactory;

    $scope.usersGrid = {
        enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
        enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
        enableSorting: true,
        enableRowSelection: true,
        enableRowHeaderSelection: false,
        multiSelect: false,
        columnDefs: [
            { field: 'Id', visible: false },
            { field: 'UserName' },
            { field: 'Email' },
            { name: 'Options', cellTemplate: '<button class="btn btn-sm btn-default" ng-click="grid.appScope.edit(row)">Edit</button>' +
                '<button class="btn btn-sm btn-default" ng-click="grid.appScope.userprofiles(row)">Profiles</button>' +
                '<button class="btn btn-sm btn-default" ng-click="grid.appScope.roles(row)">Roles</button>' +
                '<button class="btn btn-sm btn-default" ng-click="grid.appScope.delete(row)">Delete</button>'
            }
        ],
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    userFactory.getUsers()
        .then(function(users) {
                // success
                $scope.usersGrid.data = users;
            },
            function() {
                // error
                alert("Sorry, there was a problem loading users.  Please try again later.");
            });
      //.then(function () {
      //    $scope.isBusy = false;
      //});

    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/AddUserModal',
            controller: AddUserModalController
        });
    };


    $scope.edit = function (row) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/editUserModal',
            controller: EditUserModalController,
            resolve: {
                user: function() {
                    return angular.copy(row.entity);
                }
            }
        });
    };

    $scope.delete = function(row) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/deleteUserModal',
            controller: DeleteUserModalController,
            resolve: {
                user: function() {
                    return angular.copy(row.entity);
                }
            }
        });
    };

    $scope.roles = function (row) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/AssignUserRolesModal',
            controller: UserRolesModalController,
            resolve: {
                user: function () {
                    return angular.copy(row.entity);
                }
            }
        });
    };

    $scope.userprofiles = function (row) {
        $state.go('userprofiles', { 'userId': row.entity.Id });
    };
};

function AddUserModalController($scope, $modalInstance, userFactory) {

    $scope.ok = function(user) {

        userFactory.addUser(user)
            .then(function() {
                    // success
                    //$scope.gridOptions1.data = users;
                },
                function() {
                    // error
                    alert("could not save user");
                });

        $modalInstance.close();
    };

    $scope.cancel = function() {
        $modalInstance.dismiss('cancel');
    };
};

function EditUserModalController($scope, $modalInstance, userFactory, user) {

    $scope.user = user;

    $scope.ok = function () {

        userFactory.editUser(user)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not edit or update user");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

function DeleteUserModalController($scope, $modalInstance, userFactory, user) {

    $scope.user = user;

    $scope.ok = function () {

        userFactory.deleteUser(user)
            .then(function () {
                // success
                //$scope.gridOptions1.data = users;
            },
                function () {
                    // error
                    alert("could not delete user");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

/**
 * Pass function into module
 */
angular
    .module('app')
    .factory('userFactory', userFactory);

function userFactory($http, $q) {

    var _users = [];

    var _getUsers = function () {

        var deferred = $q.defer();

        $http.get('/api/Users')
          .then(function (result) {
              // Successful
              angular.copy(result.data, _users);
              deferred.resolve(_users);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };


    var _addUser = function (newUser) {

        var deferred = $q.defer();

        $http.post('/api/Users', newUser)
         .then(function (result) {
             // success
             var newlyCreatedUser = result.data;
             _users.splice(0, 0, newlyCreatedUser);
             deferred.resolve(newlyCreatedUser);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editUser = function(user) {

        var deferred = $q.defer();

        $http.put('/api/Users/' + user.Id, user)
         .then(function (result) {
             // success
             var editedUser = result.data;

             for (var i = 0; i < _users.length; i++) {
                 if (_users[i].Id === editedUser.Id) {
                     _users[i].UserName = editedUser.UserName;
                     _users[i].Email = editedUser.Email;
                     break;
                 }
             }

             deferred.resolve(editedUser);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _deleteUser = function(user) {

        var deferred = $q.defer();

        $http.delete('/api/Users/' + user.Id, user)
         .then(function (result) {

             var deletedUser = result.data;

             for (var i = 0; i < _users.length; i++) {
                 if (_users[i].Id === deletedUser.Id) {
                     _users.splice(i, 1);
                     break;
                 }
             }

             deferred.resolve();
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    return {
        users: _users,
        getUsers: _getUsers,
        addUser: _addUser,
        editUser: _editUser,
        deleteUser: _deleteUser
    };
};

