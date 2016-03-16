(function() {
    angular
        .module("app.eoc")
        .controller("PositionLogMgmtCtrl", PositionLogMgmtCtrl)
        .config(config);

    config.$inject = ["$locationProvider", "$stateProvider", "$urlRouterProvider", "$compileProvider"];

    function config($locationProvider, $stateProvider, $urlRouterProvider, $compileProvider) {

        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);

        $stateProvider
            .state("posmgmt", {
                url: "/manage/:incidentId",
                templateUrl: "/Eoc/Templates/PositionLog/PositionLogManagement",
                controller: "PositionLogMgmtCtrl",
                controllerAs: "plm"
            });
    }

    PositionLogMgmtCtrl.$inject = ["$scope", "incidentData"];

    function PositionLogMgmtCtrl($scope, incidentData) {

        var vm = this;

        vm.incidentName = "Mile High Island";
        vm.name = "Willie";
        vm.logs = [
            {
                "id": 1,
                "logName": "Updates on Patient Symptoms",
                "logDate": new Date(),
                "log": "It's been 14 days since I killed me a man."
            },
            {
                "id": 2,
                "logName": "Casualties Starting to Arrive",
                "logDate": new Date(),
                "log": "I once saw a man with no legs."
            }
        ];
        vm.search = "";
        vm.clear = clear;

        function clear() {
            vm.search = "";
        }

        function activate() {
            
        }

        // Activate the page
        activate();
    }

})();