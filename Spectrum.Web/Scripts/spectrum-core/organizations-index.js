angular
    .module('app')
    .controller('OrganizationController', organizationController);

function organizationController($scope, $http, $modal, userFactory, sweetAlert) {

    $modal.scope = $scope;

    $scope.data = userFactory;

    userFactory.getOrganizations()
        .then(function (organizations) {
            // success
            //$scope.data = users;
        },
            function () {
                // error
                sweetAlert.swal("Sorry!", "There was a problem loading organizations.  Please try again later.", "error");
            });

    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/addOrganizationModal.html',
            controller: AddOrganizationModalController
        });
    };

    $scope.edit = function (organization) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/editOrganizationModal.html',
            controller: EditOrganizationModalController,
            resolve: {
                user: function () {
                    return angular.copy(organization);
                }
            }
        });
    };

    $scope.profiles = function (organization) {
        //var user = angular.copy(row.entity);
        window.location = "/OrganizationProfiles/Index/" + organization.Id;
    };


    $scope.delete = function (organization) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/User/deleteOrganizationModal.html',
            controller: DeleteUserModalController,
            resolve: {
                user: function () {
                    return angular.copy(organization);
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


function AddOrganizationModalController($scope, $modalInstance, userFactory) {

    $scope.ok = function (user) {

        userFactory.addUser(user)
            .then(function () {
                // success
                $modalInstance.close();
                //clearModalJqHack();
            },
                function () {
                    // error
                    alert("could not save organization");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
        //clearModalJqHack();
    };
};

function EditOrganizationModalController($scope, $modalInstance, userFactory, user) {

    $scope.user = user;

    $scope.ok = function () {

        userFactory.editUser(user)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not edit or update organization");
                });

        $modalInstance.close();
        //clearModalJqHack();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
        //clearModalJqHack();
    };
};

function DeleteOrganizationModalController($scope, $modalInstance, organizationFactory, user) {

    $scope.organization = organization;

    $scope.ok = function () {

        organizationFactory.deleteOrganization(organization)
            .then(function () {
                // success

            },
                function () {
                    // error
                    alert("could not delete organization");
                });

        $modalInstance.close();
        //clearModalJqHack();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
        //clearModalJqHack();
    };
};

/**
 * Pass function into module
 */
angular
    .module('app')
    .factory('organizationFactory', organizationFactory);

function organizationFactory($http, $q) {

    var _organizations = [];

    var _getOrganizations = function () {

        var deferred = $q.defer();

        $http.get('/api/Organizations')
          .then(function (result) {
              // Successful
              angular.copy(result.data, _organizations);
              deferred.resolve(_organizations);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };


    var _addOrganization = function (newOrganization) {

        var deferred = $q.defer();

        $http.post('/api/Organizations', newOrganization)
         .then(function (result) {
             // success
             var newlyCreatedOrganization = result.data;
             _users.splice(0, 0, newlyCreatedOrganization);
             deferred.resolve(newlyCreatedOrganization);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editOrganization = function (organization) {

        var deferred = $q.defer();

        $http.put('/api/Organizations/' + organization.Id, organization)
         .then(function (result) {
             // success
             var editedOrganization = result.data;

             for (var i = 0; i < _organizations.length; i++) {
                 if (_organizations[i].Id === editedOrganization.Id) {
                     _organizations[i].UserName = editedOrganization.UserName;
                     _organizations[i].Email = editedOrganization.Email;
                     break;
                 }
             }

             deferred.resolve(editedOrganization);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _deleteOrganization = function (organization) {

        var deferred = $q.defer();

        $http.delete('/api/Organizations/' + organization.Id, organization)
         .then(function (result) {

             var deletedOrganization = result.data;

             for (var i = 0; i < _organizations.length; i++) {
                 if (_organizations[i].Id === deletedOrganization.Id) {
                     _organizations.splice(i, 1);
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
        organizations: _organizations,
        getOrganizations: _getOrganizations,
        addOrganizations: _addOrganization,
        editOrganizations: _editOrganization,
        deleteOrganizations: _deleteOrganization
    };
};