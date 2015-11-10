angular
    .module('app')
    .controller('UserPanelController', userPanelController)
    .config(config);

function config($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    // Set default state
    //$urlRouterProvider.otherwise("/dashboard");

    $stateProvider
        .state('index', {
            url: "",
            templateUrl: "/Templates/User/UserPanelIndex",
            data: {
                pageTitle: 'index'
            }
        })
        .state('profiles', {
            url: "/profiles/:userId",
            templateUrl: "/Templates/User/UserProfileIndex",
            params: { userId: null},
            data: { pageTitle: 'profiles' }
        });
}

function userPanelController($scope, $http, $modal, $state, userFactory) {

    $modal.scope = $scope;

    $scope.data = userFactory;

    userFactory.getUsers()
        .then(function(users) {
                // success
                //$scope.data = users;
            },
            function() {
                // error
                alert("Sorry!", "There was a problem loading users.  Please try again later.", "error");
            });

    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/AddUserModal',
            controller: AddUserModalController
        });
    };

    $scope.edit = function (user) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/EditUserModal',
            controller: EditUserModalController,
            resolve: {
                user: function() {
                    return angular.copy(user);
                }
            }
        });
    };

    $scope.delete = function (user) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/DeleteUserModal',
            controller: DeleteUserModalController,
            resolve: {
                user: function () {
                    return angular.copy(user);
                }
            }
        });
    };

    $scope.profiles = function (user) {
        $state.go('profiles', { 'userId': user.Id });
    };
};

function AddUserModalController($scope, $modalInstance, userFactory) {

    $scope.ok = function(user) {

        userFactory.addUser(user)
            .then(function() {
                    // success
                $modalInstance.close();
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