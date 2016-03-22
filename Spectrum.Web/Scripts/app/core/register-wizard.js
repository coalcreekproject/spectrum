angular
    .module('app')
    .controller('registerWizardController', registerWizardController)
    .config(config);

config.$inject = ["$stateProvider"];

function config($stateProvider) {

    $stateProvider
        .state('index', {
            url: "",
            templateUrl: "/Templates/Registration/RegisterIndex",
            data: { pageTitle: 'index' }
        });
}

registerWizardController.$inject = ["$scope", "$http", "$window"];

function registerWizardController($scope, $http, $window) {

    $scope.registerViewModel = {};

    // Initial step
    $scope.step = 1;

    // Wizard functions
    $scope.wizard = {
        show: function (number) {
            $scope.step = number;
        },
        next: function () {
            $scope.step++;
        },
        prev: function () {
            $scope.step--;
        }
    };

    $scope.submit = function () {
        $http({
            method: 'POST',
            url: "/Registration/Register",
            data: $scope.registerViewModel
        }).success(function (responseData) {
            var inspect = responseData;
            $window.location.href = '/Portal/Index';
        }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
    };
}