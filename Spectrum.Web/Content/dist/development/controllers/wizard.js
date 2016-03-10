'use strict';

/**
 *
 * wizardCtrl
 *
 */

angular.module('app').controller('wizardOneCtrl', wizardOneCtrl);

function wizardOneCtrl($scope, notify, sweetAlert) {

    // Initial user
    $scope.user = {
        username: 'Mark Smith',
        email: 'mark@company.com',
        password: 'secret_password',
        approve: false
    };

    // Initial step
    $scope.step = 1;

    // Wizard functions
    $scope.wizard = {
        show: function show(number) {
            $scope.step = number;
        },
        next: function next() {
            $scope.step++;
        },
        prev: function prev() {
            $scope.step--;
        }
    };

    $scope.submit = function () {
        // Show notification
        sweetAlert.swal({
            title: "Thank you!",
            text: "You approved our example form!",
            type: "success"
        });

        // 'Redirect' to step 1
        $scope.step = 1;
    };
}
