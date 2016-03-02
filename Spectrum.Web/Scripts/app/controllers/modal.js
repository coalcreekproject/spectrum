/**
 *
 * modalCtrl
 *
 */

angular
    .module("app")
    .controller("modalCtrl", modalCtrl);

function modalCtrl($scope, $modal) {

    $scope.open = function() {
        const modalInstance = $modal.open({
            templateUrl: "views/modal/modal_example.html",
            controller: ModalInstanceCtrl
        });
    };

    $scope.open1 = function() {
        const modalInstance = $modal.open({
            templateUrl: "views/modal/modal_example1.html",
            controller: ModalInstanceCtrl
        });
    };

    $scope.open3 = function(size) {
        const modalInstance = $modal.open({
            templateUrl: "views/modal/modal_example3.html",
            size: size,
            controller: ModalInstanceCtrl
        });
    };

    $scope.open2 = function() {
        const modalInstance = $modal.open({
            templateUrl: "views/modal/modal_example2.html",
            controller: ModalInstanceCtrl,
            windowClass: "hmodal-info"
        });
    };

    $scope.open4 = function() {
        const modalInstance = $modal.open({
            templateUrl: "views/modal/modal_example2.html",
            controller: ModalInstanceCtrl,
            windowClass: "hmodal-warning"
        });
    };

    $scope.open5 = function() {
        const modalInstance = $modal.open({
            templateUrl: "views/modal/modal_example2.html",
            controller: ModalInstanceCtrl,
            windowClass: "hmodal-success"
        });
    };

    $scope.open6 = function() {
        const modalInstance = $modal.open({
            templateUrl: "views/modal/modal_example2.html",
            controller: ModalInstanceCtrl,
            windowClass: "hmodal-danger"
        });
    };
};

function ModalInstanceCtrl($scope, $modalInstance) {

    $scope.ok = function() {
        $modalInstance.close();
    };

    $scope.cancel = function() {
        $modalInstance.dismiss("cancel");
    };
};