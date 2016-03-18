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

    PositionLogMgmtCtrl.$inject = ["$scope", "$stateParams", "$uibModal", "incidentData"];

    function PositionLogMgmtCtrl($scope, $stateParams, $uibModal, incidentData) {

        // TODO: Move this to config
        var POS_LOG_TEMPLATE_PATH = "Eoc/Templates/PositionLog/";

        var vm = this;

        vm.incident = getIncident;
        vm.openAddModal = openAddModal;
        vm.incidentId = $stateParams.incidentId;
        vm.name = "Wilkins";
        vm.search = "";
        vm.clear = clear;

        function clear() {
            vm.search = "";
        }

        function activate() {
            if (vm.incidentId) {
                getIncident(vm.incidentId);
            }
            else {
                console.log("Error saving parameter: incidentId.");
            }
        }

        function getIncident(incidentId) {
            $scope.loading = true;
            return incidentData.getIncident(incidentId)
                .then(function(incident) {
                        vm.incident = incident;
                        $scope.loading = false;
                    },
                    function() {
                        $scope.loading = false;
                        console.log("Error loading Incident.  See log.");
                    });
        }

        function openAddModal() {
            var modalDialog = $uibModal.open({
                templateUrl: POS_LOG_TEMPLATE_PATH + "PositionLogAddModal",
                controller: addModalInstance,
                resolve: {
                    incidentId: function() {
                        return vm.incidentId;
                    }
                }
            });
            modalDialog.result
                .then(function() {
                    activate();
                }).catch(function() {
                    // Do nothing
                });
        }

        // Activate the page
        activate();

        // Add modal instance
        addModalInstance.$inject = ["$scope", "$uibModalInstance", "incidentId"];

        function addModalInstance($scope, $uibModalInstance, incidentId) {

            $scope.debugEnabled = false;
            $scope.logItem = {};

            $scope.dateOptions = {
                formatYear: "yy",
                maxDate: new Date(2020, 5, 22),
                startingDay: 1
            };

            $scope.datePopUp = {
                opened: false
            };

            $scope.openDatePopUp = function() {
                $scope.datePopUp.opened = true;
            };

            $scope.saveChanges = function(logItem) {

                // TODO: Better validation
                if (logItem.occurred && logItem.date && logItem.remarks) {

                    // Shape the model
                    var incidentLogInputViewModel = {
                        "id": incidentId,
                        "log": {
                            "logId": 0,
                            "logName": logItem.occurred,
                            "logDate": logItem.date,
                            "logEntry": logItem.remarks
                        }
                    };

                    // Add the incident log
                    incidentData.addIncidentLog(incidentLogInputViewModel)
                        .then(function() {
                            $uibModalInstance.close();
                        }, function(result) {
                            console.log(result.message);
                        });
                }
                return;
            };

            $scope.close = function() {
                $uibModalInstance.dismiss("cancel");
            };
        }
    }

})();