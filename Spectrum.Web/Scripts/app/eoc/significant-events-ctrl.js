(function() {
    angular
        .module('app.eoc')
        .controller('SignificantEventsCtrl', SignificantEventsCtrl)
        .config(config);

    config.$inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider', '$compileProvider'];

    function config($locationProvider, $stateProvider, $urlRouterProvider, $compileProvider) {

        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);

    }

    SignificantEventsCtrl.$inject = ['$scope', '$stateParams', '$uibModal', 'incidentData'];

    function SignificantEventsCtrl($scope, $stateParams, $uibModal, incidentData) {

        // TODO: Move this to config
        var POS_LOG_TEMPLATE_PATH = 'Eoc/Templates/SignificantEvents/';

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
                templateUrl: POS_LOG_TEMPLATE_PATH + 'SignificantEventsAddModal',
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

        function openEditModal(eventItem) {
            var modalDialog = $uibModal.open({
                templateUrl: POS_LOG_TEMPLATE_PATH + 'SignificantEventsEditModal',
                controller: editModalInstance,
                resolve: {
                    editModalData: function() {
                        var editItem = {
                            "incidentId": vm.incidentId,
                            "eventItem": eventItem
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

        function openDeleteModal(eventItem) {
            var modalDialog = $uibModal.open({
                templateUrl: POS_LOG_TEMPLATE_PATH + 'SignificantEventsDeleteModal',
                controller: deleteModalInstance,
                resolve: {
                    deleteItemData: function() {
                        var deleteItem = {
                            "incidentId": vm.incidentId,
                            "eventItem": eventItem
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
            $scope.eventItem = {};

            // Timepicker defaults
            $scope.eventItem.selectedTime = new Date();
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

            $scope.saveChanges = function(eventItem) {

                // TODO: Better validation
                if (isValidEventInput(eventItem)) {

                    var incidentEventInputViewModel = createIncidenEventInputModel($scope.incidentId, eventItem);

                    // Add the incident log
                    incidentData.addIncidentEvent(incidentEventInputViewModel)
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
            $scope.eventItem = {
                "eventId": editModalData.eventItem.eventId,
                "date": new Date(editModalData.eventItem.eventDate),
                "selectedTime": new Date(editModalData.eventItem.eventDate),
                "title": editModalData.eventItem.eventTitle,
                "occurred": editModalData.eventItem.eventName,
                "remarks": editModalData.eventItem.eventEntry
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

            $scope.saveChanges = function(eventItem) {

                // TODO: Better validation
                if (isValidEventInput(eventItem)) {

                    var incidentEventInputViewModel = createIncidenEventInputModel($scope.incidentId, eventItem);

                    // Edit the incident log
                    incidentData.editIncidentEvent(incidentEventInputViewModel)
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
            $scope.eventName = deleteItemData.eventItem.eventName;
            $scope.eventId = deleteItemData.eventItem.eventId;

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.delete = function() {
                // Remove the incident
                incidentData.deleteIncidentEvent($scope.incidentId, $scope.eventId)
                    .then(function() {
                        $uibModalInstance.close();
                    }, function(result) {
                        console.log(result.message);
                    });
            };
        }
    }

    function isValidEventInput(eventItemInput) {
        if (eventItemInput.title && eventItemInput.occurred &&
            eventItemInput.date && eventItemInput.selectedTime
            && eventItemInput.remarks) {
            return true;
        }
        return false;
    }

    function createIncidenEventInputModel(incidentId, eventItemInput) {

        // Format dates
        var year = eventItemInput.date.getFullYear();
        var month = eventItemInput.date.getMonth();
        var date = eventItemInput.date.getDate();
        var hour = eventItemInput.selectedTime.getHours();
        var mins = eventItemInput.selectedTime.getMinutes();
        var setEventDate = new Date(year, month, date, hour, mins).toUTCString();

        // New event or updating?
        var eventId = eventItemInput.eventId ? eventItemInput.eventId : 0;

        // Shape the input view model
        var inputViewModel = {
            "id": incidentId,
            "event": {
                "eventId": eventId,
                "eventTitle": eventItemInput.title,
                "eventName": eventItemInput.occurred,
                "eventDate": setEventDate,
                "eventEntry": eventItemInput.remarks
            }
        };

        return inputViewModel;
    }

})();