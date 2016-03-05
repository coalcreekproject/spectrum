angular
    .module('app')
    .controller('OrganizationPositionController', organizationPositionController);

function organizationPositionParameters() {
    this.organizationId = null;
};

function organizationPositionController($scope, $http, $window, $modal, $stateParams, organizationPositionFactory) {

    $scope.organizationId = $stateParams.organizationId;
    organizationPositionParameters.organizationId = $scope.organizationId;

    $modal.scope = $scope;
    $scope.data = organizationPositionFactory;

    organizationPositionFactory.getOrganizationPositions($scope.organizationId)
        .then(function (organizationPositions) {
            // success
            //$scope.data = organization;
        },
            function () {
                // error
                alert("Sorry! There was a problem loading organization positions.  Please try again later.");
            });

    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/addOrganizationPositionModal',
            controller: AddOrganizationPositionModalController
        });
    };

    $scope.edit = function (organizationPosition) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/editOrganizationPositionModal',
            controller: EditOrganizationPositionModalController,
            resolve: {
                organizationPosition: function () {
                    return angular.copy(organizationPosition);
                }
            }
        });
    };

    $scope.delete = function (organizationPosition) {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Organization/deleteOrganizationPositionModal',
            controller: DeleteOrganizationPositionModalController,
            resolve: {
                organizationPosition: function () {
                    return angular.copy(organizationPosition);
                }
            }
        });
    };
};

function AddOrganizationPositionModalController($scope, $modalInstance, organizationPositionFactory) {

    $scope.ok = function (organizationPosition) {

        organizationPosition.OrganizationId = organizationPositionParameters.organizationId;

        organizationPositionFactory.addOrganizationPositions(organizationPosition)
            .then(function () {
                // success
                $modalInstance.close();
            },
                function () {
                    // error
                    alert("could not save organization position");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

function EditOrganizationPositionModalController($scope, $modalInstance, organizationPositionFactory, organizationPosition) {

    $scope.organizationPosition = organizationPosition;

    $scope.ok = function () {

        organizationPositionFactory.editOrganizationPositions(organizationPosition)
            .then(function () {
                // success
            },
                function () {
                    // error
                    alert("could not edit or update organization position");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

function DeleteOrganizationPositionModalController($scope, $modalInstance, organizationPositionFactory, organizationPosition) {

    $scope.organizationPosition = organizationPosition;

    $scope.ok = function () {

        organizationPositionFactory.deleteOrganizationPositions(organizationPosition)
            .then(function () {
                // success

            },
                function () {
                    // error
                    alert("could not delete organization position");
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
    .factory('organizationPositionFactory', organizationPositionFactory);

function organizationPositionFactory($http, $q) {

    var _organizationPositions = [];

    var _getOrganizationPositions = function (id) {

        var deferred = $q.defer();

        $http.get('/api/Positions/' + id)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _organizationPositions);
              deferred.resolve(_organizationPositions);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };
    
    var _addOrganizationPosition = function (newOrganizationPosition) {

        var deferred = $q.defer();

        $http.post('/api/Positions', newOrganizationPosition)
         .then(function (result) {
             // success
             var newlyCreatedOrganizationPosition = result.data;
             _organizationPositions.splice(0, 0, newOrganizationPosition);
             deferred.resolve(newlyCreatedOrganizationPosition);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _editOrganizationPosition = function (organizationPosition) {

        var deferred = $q.defer();

        $http.put('/api/Positions/' + organizationPosition.Id, organizationPosition)
         .then(function (result) {
             // success
             var editedOrganizationPosition = result.data;

             for (var i = 0; i < _organizationPositions.length; i++) {
                 if (_organizationPositions[i].Id === editedOrganizationPosition.Id) {
                     _organizationPositions[i].Name = editedOrganizationPosition.Name;
                     _organizationPositions[i].Description = editedOrganizationPosition.Description;

                     break;
                 }
             }

             deferred.resolve(editedOrganizationPosition);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _deleteOrganizationPosition = function (organizationPosition) {

        var deferred = $q.defer();

        $http.delete('/api/Positions/' + organizationPosition.Id, organizationPosition)
         .then(function (result) {

             var deletedOrganizationPosition = result.data;

             for (var i = 0; i < _organizationPositions.length; i++) {
                 if (_organizationPositions[i].Id === deletedOrganizationPosition.Id) {
                     _organizationPositions.splice(i, 1);
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
        organizationPositions: _organizationPositions,
        getOrganizationPositions: _getOrganizationPositions,
        addOrganizationPositions: _addOrganizationPosition,
        editOrganizationPositions: _editOrganizationPosition,
        deleteOrganizationPositions: _deleteOrganizationPosition
    };
};