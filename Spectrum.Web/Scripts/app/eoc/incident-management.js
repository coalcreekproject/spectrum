(function() {
    angular
        .module('app.eoc')
        .controller('IncidentMgmtCtrl', IncidentMgmtCtrl)
        .config(config);

    function config($locationProvider, $stateProvider, $urlRouterProvider, $compileProvider) {

        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);

        // Set default state
        //$urlRouterProvider.otherwise("/dashboard");

        $stateProvider
            .state('index', {
                url: '',
                templateUrl: '/Eoc/Templates/IncidentManagement/IncidentManagementIndex',
                controller: 'IncidentMgmtCtrl',
                controllerAs: 'im'
            })
            //.state('position-log', {
            //    url: "/position-log/:incidentId",
            //    templateUrl: "/Eoc/Templates/PositionLog/PositionLogIndex",
            //    controller: 'PositionLogCtrl',
            //    controllerAs: 'pl',
            //    data: {
            //        pageTitle: 'PositionLog'
            //    }
            //});
            .state('position-log', {
                url: '/manage/:incidentId',
                templateUrl: '/Eoc/Templates/PositionLog/PositionLogManagement',
                controller: 'PositionLogMgmtCtrl',
                controllerAs: 'plm'
            })
            .state('timeline', {
                url: '/timeline/:incidentId',
                templateUrl: '/Eoc/Templates/IncidentTimeline/IncidentTimelineIndex',
                controller: 'IncidentTimelineCtrl',
                controllerAs: 'vm'
            });
    }

    IncidentMgmtCtrl.$inject = ['$scope', '$state', 'incidentData', '$uibModal'];

    function IncidentMgmtCtrl($scope, $state, incidentData, $uibModal) {

        var INCIDENT_TEMPLATE_PATH = 'Eoc/Templates/IncidentManagement/';

        // Setting scope object
        var vm = this;

        // Defining variables at top of controller to quickly
        // scan all available operations
        vm.incidents = [];
        vm.search = '';
        vm.clear = clear;
        vm.openModal = openModal;
        vm.editModal = editModal;
        vm.deleteModal = deleteModal;

        // Then define controller functions


        vm.incidentLog = function (incident) {
            $state.go('position-log', { 'incidentId': incident.id });
        };

        vm.timeline = function (incident) {
            $state.go('timeline', { 'incidentId': incident.id });
        };

        function clear() {
            vm.search = '';
        }

        function openModal() {
            var modalDialog = $uibModal.open({
                templateUrl: INCIDENT_TEMPLATE_PATH + 'IncidentManagementAddModal',
                controller: openModalInstance
            });
            modalDialog.result
                .then(function() {
                    getIncidents();
                }).catch(function() {
                    // Do nothing
                });
        }

        function editModal(incident) {
            var modalDialog = $uibModal.open({
                templateUrl: INCIDENT_TEMPLATE_PATH + 'IncidentManagementEditModal',
                controller: editModalInstance,
                resolve: {
                    incidentModalData: function() {
                        return incident;
                    }
                }
            });
            modalDialog.result
                .then(function() {
                    getIncidents();
                }).catch(function() {
                    // Do nothing
                });
        }

        function deleteModal(incident) {
            var modalDialog = $uibModal.open({
                templateUrl: INCIDENT_TEMPLATE_PATH + 'IncidentManagementDeleteModal',
                controller: deleteModalInstance,
                resolve: {
                    incidentModalData: function() {
                        return incident;
                    }
                }
            });
            modalDialog.result
                .then(function() {
                    getIncidents();
                }).catch(function() {
                    // Do nothing
                });
        }

        function activate() {
            return getIncidents();
        }

        function getIncidents() {
            $scope.loading = true;
            return incidentData.getIncidents()
                .then(function(incidents) {
                        vm.incidents = incidents;
                        $scope.loading = false;
                    },
                    function() {
                        $scope.loading = false;
                        console.log('Error loading incidents.  See log.');
                    });
        }

        // Activate the page
        activate();

        // Add modal instance
        openModalInstance.$inject = ['$scope', '$uibModalInstance', 'incidentData'];

        function openModalInstance($scope, $uibModalInstance, incidentData) {

            $scope.debugEnabled = false;
            $scope.incident = {
                name: '',
                selectedType: { "id": 2, "name": 'Natural Disaster' },
                incidentTypes: incidentData.getIncidentTypes(),
                selectedLevel: null,
                incidentLevels: incidentData.getIncidentLevels()
            };

            $scope.saveChanges = function(incident) {

                // We have to shape the model a bit here
                var incidentModel = {
                    userId: null,
                    organizationId: null,
                    incidentName: incident.name,
                    type: incident.selectedType.name,
                    level: incident.selectedLevel,
                    status: 'Active' // always active to begin
                };

                // Add the incident
                incidentData.addIncident(incidentModel)
                    .then(function() {
                        $uibModalInstance.close();
                    }, function(result) {
                        console.log(result.message);
                    });
            };

            $scope.close = function() {
                $uibModalInstance.dismiss('cancel');
            };
        }

        // Edit modal instance
        editModalInstance.$inject = ['$scope', '$uibModalInstance', 'incidentModalData', 'incidentData'];

        function editModalInstance($scope, $uibModalInstance, incidentModalData, incidentData) {

            var incidentTypes = incidentData.getIncidentTypes();
            var incidentType = getIndex(incidentTypes, incidentModalData.type);

            $scope.incident = {
                id: incidentModalData.id,
                incidentTypes: incidentTypes,
                incidentLevels: incidentData.getIncidentLevels(),
                statuses: incidentData.getIncidentStatuses(),
                name: incidentModalData.incidentName,
                selectedLevel: incidentModalData.level.toString(),
                organizationId: incidentModalData.organizationId,
                selectedStatus: incidentModalData.status,
                selectedType: incidentType,
                userId: incidentModalData.userId
            };

            $scope.close = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.saveChanges = function(incident) {
                // Edit the incident
                incidentData.editIncident(incident)
                    .then(function() {
                        $uibModalInstance.close();
                    }, function(result) {
                        console.log(result.message);
                    });
            };

            function getIndex(values, searchVal) {
                for (var i = 0; i < values.length; i++) {
                    if (values[i].name === searchVal) {
                        return values[i];
                    }
                }
                return -1; // not found
            }
        }

        // Delete modal instance
        deleteModalInstance.$inject = ['$scope', '$uibModalInstance', 'incidentModalData'];

        function deleteModalInstance($scope, $uibModalInstance, incidentModalData) {

            $scope.incidentId = incidentModalData.id;
            $scope.incidentName = incidentModalData.incidentName;

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.delete = function(incidentId) {

                // Remove the incident
                incidentData.deleteIncident(incidentId)
                    .then(function() {
                        $uibModalInstance.close();
                    }, function(result) {
                        console.log(result.message);
                    });
            };
        }
    }

})();