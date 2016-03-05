angular
    .module('app')
    .controller('UserPositionsModalController', UserPositionsModalController);

function userPositionParameters() {
    this.userId = null;
    this.organizationId = null;
};

function UserPositionsModalController($scope, $modalInstance, userPositionFactory, user) {

    $scope.userId = user.Id;
    userPositionParameters.userId = $scope.userId;
    $scope.user = user;

    $scope.models = {
        selected: null,
        lists: { "Available": [], "Assigned": [] }
    };

    // Find the user default profile organization id
    for (var i = 0; i < user.UserProfiles.length; ++i) {
        if (user.UserProfiles[i].Default === true) {
            userPositionParameters.organizationId = user.UserProfiles[i].OrganizationId;
        }
    }

    $scope.data = userPositionFactory;

    userPositionFactory.getAvailablePositions(userPositionParameters.organizationId)
        .then(function(availablePositions) {
                // success
                $scope.availablePositions = availablePositions;
                for (var i = 0; i < $scope.availablePositions.length; ++i) {
                    $scope.models.lists.Available.push({
                        label: $scope.availablePositions[i].Name,
                        object: $scope.availablePositions[i]
                    });
                }
            },
            function() {
                // error
                alert("could not get available positions");
            });


    userPositionFactory.getUserPositions(userPositionParameters.userId)
        .then(function(userPositions) {
                // success
                $scope.userPositions = userPositions;
                for (var i = 0; i < $scope.userPositions.length; ++i) {
                    $scope.models.lists.Assigned.push({
                        label: $scope.userPositions[i].Name,
                        object: $scope.userPositions[i]
                    });
                    var j = 0;
                    while (j < $scope.models.lists.Available.length) {
                        if ($scope.userPositions[i].PositionId === $scope.models.lists.Available[j].object.PositionId) {
                            $scope.models.lists.Available.splice(j, 1);
                        }
                        j++;
                    };
                }
            },
            function() {
                // error
                alert("could not get user positions");
            });

    // Model to JSON for demo purpose
    //$scope.$watch('models', function (model) {
    //    $scope.modelAsJson = angular.toJson(model, true);
    //}, true);

    $scope.ok = function () {

        userPositionFactory.editUserPositions($scope.models.lists.Assigned, $scope.user)
            .then(function () {
                // success
                $modalInstance.close();
            },
                function () {
                    // error
                    alert("could not save positions");
                });

        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
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

        for (var i = 0; i < positionList.length; i++) {
            var userPosition = {
                UserId: user.Id,
                PositionId: positionList[i].object.PositionId,
                OrganizationId: userPositionParameters.organizationId,
                Default: positionList[i].Default
            };
            user.UserPositions.push(userPosition);
        }

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