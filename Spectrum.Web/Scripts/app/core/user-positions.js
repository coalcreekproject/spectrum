angular
    .module('app')
    .controller('UserPositionsModalController', UserPositionsModalController);

function userPositionParameters() {
    this.userId = null;
    this.organizationId = null;
};

function UserPositionsModalController($scope, $uibModalInstance, userPositionFactory, user) {

    $scope.user = user;
    $scope.userId = user.id;
    userPositionParameters.userId = $scope.userId;
    
    $scope.models = {
        selected: null,
        lists: { "Available": [], "Assigned": [] }
    };

    $scope.data = userPositionFactory;

    userPositionFactory.getUserPositions(userPositionParameters.userId)
        .then(function(userPositions) {
                // success
                $scope.userPositions = userPositions;
                $scope.models.lists.Available = userPositions.item1;
                $scope.models.lists.Assigned = userPositions.item2;
            },
            function() {
                // error
                alert("could not get user positions");
            });

    $scope.ok = function() {

        userPositionFactory.editUserPositions($scope.models.lists.Assigned, $scope.user)
            .then(function() {
                    // success
                },
                function() {
                    // error
                    alert("could not save positions");
                });

        $uibModalInstance.close();
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
};


angular
    .module('app')
    .factory('userPositionFactory', userPositionFactory);

function userPositionFactory($http, $q) {

    var _availablePositions = [];
    var _userPositions = [];

    var _getAvailablePositions = function (id) {

        var deferred = $q.defer();

        $http.get('/api/Positions/' + id)
            .then(function (result) {
                    // Successful
                    angular.copy(result.data, _availablePositions);
                    deferred.resolve(_availablePositions);
                },
                function () {
                    // Error
                    deferred.reject();
                });

        return deferred.promise;
    };

    var _getUserPositions = function(id) {

        var deferred = $q.defer();

        $http.get('/api/UserPositions/' + id)
            .then(function(result) {
                    // Successful
                    angular.copy(result.data, _userPositions);
                    deferred.resolve(_userPositions);
                },
                function() {
                    // Error
                    deferred.reject();
                });

        return deferred.promise;
    };

    var _editUserPositions = function(positionList, user) {

        user.UserPositions = [];
        user.UserPositions = positionList;

        var deferred = $q.defer();

        $http.put('/api/UserPositions/', user)
            .then(function(result) {
                    // success
                    deferred.resolve(result);
                },
                function() {
                    // error
                    deferred.reject();
                });

        return deferred.promise;
    };

    return {
        availablePositions: _availablePositions,
        getAvailablePositions: _getAvailablePositions,
        userPositions: _userPositions,
        getUserPositions: _getUserPositions,
        editUserPositions: _editUserPositions
    };
}