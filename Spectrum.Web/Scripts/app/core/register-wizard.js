angular
    .module('app')
    .controller('registerWizardController', registerWizardController)
    .config(config);

config.$inject = ["$stateProvider", "$urlRouterProvider", "$compileProvider"];

function config($stateProvider, $urlRouterProvider, $compileProvider) {

    $stateProvider
        .state('index', {
            url: "",
            templateUrl: "/Templates/Registration/RegisterIndex",
            data: { pageTitle: 'index' }
        });
}

function registerWizardController($scope, $http, $state) {

    // Initial user
    $scope.user = {
        username: 'Mark Smith',
        email: 'mark@company.com',
        password: 'secret_password',
        approve: false
    }

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
        // Show notification
        alert({
            title: "Thank you!",
            text: "You approved our example form!",
            type: "success"
        });

        // 'Redirect' to step 1
        $scope.step = 1;

    }

}