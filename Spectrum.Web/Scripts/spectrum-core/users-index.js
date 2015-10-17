//var app = angular.module('app', ['ngAnimate', 'ngTouch', 'ui.bootstrap', 'oitozero.ngSweetAlert']);


angular
    .module('app')
    .controller('UserPanelController', userPanelController);

//app.controller('UserPanelController', ['$scope', '$http', '$modal', 'userFactory', 'sweetAlert',
function userPanelController($scope, $http, $modal, userFactory) {

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
            templateUrl: '/Templates/User/addUserModal.html',
            controller: AddUserModalController
        });
    };

    $scope.edit = function (user) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/editUserModal.html',
            controller: EditUserModalController,
            resolve: {
                user: function() {
                    return angular.copy(user);
                }
            }
        });
    };

    $scope.profiles = function (user) {
        //var user = angular.copy(row.entity);
        window.location = "/UserProfile/Index/" + user.Id;
    };


    $scope.delete = function (user) {
            var modalInstance = $modal.open({
                templateUrl: '/Templates/User/deleteUserModal.html',
                controller: DeleteUserModalController,
                resolve: {
                    user: function () {
                        return angular.copy(user);
                    }
                }
            });
        };
};

//Hard coded hack to make Angular 1.4 and accompanying UI library
// dismiss modals properly.  This is a known bug, 
// keep an eye on Angular-UI
//function clearModalJqHack() {
//    $('div.modal').removeClass('fade').addClass('hidden');
//    $('body').removeClass('modal-open');
//    $('.modal-backdrop').remove(); //problem
//}


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