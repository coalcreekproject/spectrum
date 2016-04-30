(function() {
    angular
        .module('app.eoc')
        .controller('PositionLogMgmtCtrl', PositionLogMgmtCtrl)
        .config(config);

    config.$inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider', '$compileProvider'];

    function config($locationProvider, $stateProvider, $urlRouterProvider, $compileProvider) {

        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);

        //$stateProvider
        //    .state('posmgmt', {
        //        url: '/manage/:incidentId',
        //        templateUrl: '/Eoc/Templates/PositionLog/PositionLogManagement',
        //        controller: 'PositionLogMgmtCtrl',
        //        controllerAs: 'plm'
        //    });
    }

    PositionLogMgmtCtrl.$inject = ['$scope', '$stateParams', '$uibModal', 'incidentData'];

    function PositionLogMgmtCtrl($scope, $stateParams, $uibModal, incidentData) {

        // TODO: Move this to config
        var POS_LOG_TEMPLATE_PATH = 'Eoc/Templates/PositionLog/';

        var vm = this;

        // Controller definitions
        vm.incident = getIncident;
        vm.openAddModal = openAddModal;
        vm.openEditModal = openEditModal;
        vm.openDeleteModal = openDeleteModal;
        vm.incidentId = $stateParams.incidentId;
        vm.search = '';
        vm.clear = clear;

        // Controller functions
        function clear() {
            vm.search = '';
        }  

        function activate() {
            if (vm.incidentId) {
                getIncident(vm.incidentId);
            }
            else {
                console.log('Error saving parameter: incidentId.');
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
                        console.log('Error loading Incident.  See log.');
                    });
        }

        function openAddModal() {
            var modalDialog = $uibModal.open({
                templateUrl: POS_LOG_TEMPLATE_PATH + 'PositionLogAddModal',
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

        function openEditModal(logItem) {
            var modalDialog = $uibModal.open({
                templateUrl: POS_LOG_TEMPLATE_PATH + 'PositionLogAddModal',
                controller: editModalInstance,
                resolve: {
                    editModalData: function() {
                        var editItem = {
                            "incidentId": vm.incidentId,
                            "logItem": logItem
                        };
                        return editItem;
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

        function openDeleteModal(logItem) {
            var modalDialog = $uibModal.open({
                templateUrl: POS_LOG_TEMPLATE_PATH + 'PositionLogDeleteModal',
                controller: deleteModalInstance,
                resolve: {
                    deleteItemData: function() {
                        var deleteItem = {
                            "incidentId": vm.incidentId,
                            "logItem": logItem
                        };
                        return deleteItem;
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
        addModalInstance.$inject = ['$scope', '$uibModalInstance', 'incidentId'];

        function addModalInstance($scope, $uibModalInstance, incidentId) {

            $scope.debugEnabled = false;

            $scope.incidentId = incidentId;
            $scope.logItem = {};

            // Timepicker defaults
            $scope.logItem.selectedTime = new Date();
            $scope.hstep = 1;
            $scope.mstep = 1;
            $scope.ismeridian = true;
            $scope.toggleMode = function() {
                $scope.ismeridian = !$scope.ismeridian;
            };

            // Datepicker defaults
            $scope.dateOptions = {
                formatYear: 'yy',
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
                if (isValidPositionLogInput(logItem)) {

                    var incidentLogInputViewModel = createIncidenLogInputModel($scope.incidentId, logItem);

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
                $uibModalInstance.dismiss('cancel');
            };
        }

        // Edit modal instance
        editModalInstance.$inject = ['$scope', '$uibModalInstance', 'editModalData'];

        function editModalInstance($scope, $uibModalInstance, editModalData) {

            $scope.incidentId = editModalData.incidentId;
            $scope.logItem = {
                "logId": editModalData.logItem.logId,
                "date": new Date(editModalData.logItem.logDate),
                "selectedTime": new Date(editModalData.logItem.logDate),
                "title": editModalData.logItem.logTitle,
                "occurred": editModalData.logItem.logName,
                "remarks": editModalData.logItem.logEntry
            };

            // Timepicker defaults
            $scope.hstep = 1;
            $scope.mstep = 1;
            $scope.ismeridian = true;
            $scope.toggleMode = function() {
                $scope.ismeridian = !$scope.ismeridian;
            };

            // Datepicker defaults
            $scope.dateOptions = {
                formatYear: 'yy',
                maxDate: new Date(2020, 5, 22),
                startingDay: 1
            };
            $scope.datePopUp = {
                opened: false
            };
            $scope.openDatePopUp = function() {
                $scope.datePopUp.opened = true;
            };

            $scope.close = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.saveChanges = function(logItem) {

                // TODO: Better validation
                if (isValidPositionLogInput(logItem)) {

                    var incidentLogInputViewModel = createIncidenLogInputModel($scope.incidentId, logItem);

                    // Edit the incident log
                    incidentData.editIncidentLog(incidentLogInputViewModel)
                        .then(function() {
                            $uibModalInstance.close();
                        }, function(result) {
                            console.log(result.message);
                        });
                }
                return;
            };
        }

        // Delete modal instance
        deleteModalInstance.$inject = ['$scope', '$uibModalInstance', 'deleteItemData'];

        function deleteModalInstance($scope, $uibModalInstance, deleteItemData) {

            $scope.incidentId = deleteItemData.incidentId;
            $scope.logName = deleteItemData.logItem.logName;
            $scope.logId = deleteItemData.logItem.logId;

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.delete = function() {
                // Remove the incident
                incidentData.deleteIncidentLog($scope.incidentId, $scope.logId)
                    .then(function() {
                        $uibModalInstance.close();
                    }, function(result) {
                        console.log(result.message);
                    });
            };
        }
    }

    function isValidPositionLogInput(logItemInput) {
        if (logItemInput.title && logItemInput.occurred &&
            logItemInput.date && logItemInput.selectedTime
            && logItemInput.remarks) {
            return true;
        }
        return false;
    }

    function createIncidenLogInputModel(incidentId, logItemInput) {

        // Format dates
        var year = logItemInput.date.getFullYear();
        var month = logItemInput.date.getMonth();
        var date = logItemInput.date.getDate();
        var hour = logItemInput.selectedTime.getHours();
        var mins = logItemInput.selectedTime.getMinutes();
        var setLogDate = new Date(year, month, date, hour, mins).toUTCString();

        // New log or updating?
        var logId = logItemInput.logId ? logItemInput.logId : 0;

        // Shape the input view model
        var inputViewModel = {
            "id": incidentId,
            "log": {
                "logId": logId,
                "logTitle": logItemInput.title,
                "logName": logItemInput.occurred,
                "logDate": setLogDate,
                "logEntry": logItemInput.remarks
            }
        };

        return inputViewModel;
    }

})();