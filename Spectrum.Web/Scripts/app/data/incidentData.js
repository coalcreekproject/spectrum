(function() {

    "use strict";

    angular.module("app.data")
        .service("incidentData", incidentData);

    incidentData.$inject = ["$http", "$q"];

    function incidentData($http, $q) {

        var INCIDENT_API = "/Eoc/api/Incidents";

        var incidents = [];
        var getIncidents = function() {

            var deferred = $q.defer();

            $http.get(INCIDENT_API)
                .then(function(result) {
                        angular.copy(result.data, incidents);
                        deferred.resolve(incidents);
                    },
                    function() {
                        deferred.reject();
                    });

            return deferred.promise;
        };

        var incident = {};
        var getIncident = function(incidentId) {
            var deferred = $q.defer();

            $http.get("/Eoc/api/Incident" + "/" + incidentId)
                .then(function(result) {
                        angular.copy(result.data, incident);
                        deferred.resolve(incident);
                    },
                    function() {
                        deferred.reject();
                    });

            return deferred.promise;
        };

        var incidentLog = {};
        var getIncidentLog = function(incidentId, logId) {
            var deferred = $q.defer();

            $http.get("/Eoc/api/Incident/" + incidentId + "/Log/" + logId)
                .then(function(result) {
                        angular.copy(result.data, incidentLog);
                        deferred.resolve(incidentLog);
                    },
                    function() {
                        deferred.reject();
                    });

            return deferred.promise;
        };

        var addIncident = function(incident) {

            var deferred = $q.defer();

            $http.post(INCIDENT_API, incident)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        var addIncidentLog = function(inputModel) {
            var deferred = $q.defer();

            $http.post("/Eoc/api/Incident/Log", inputModel)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        var deleteIncidentLog = function(incidentId, logId) {
            var deferred = $q.defer();

            $http.delete("/Eoc/api/Incident/" + incidentId + "/Log/" + logId)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        var editIncident = function(data) {

            var deferred = $q.defer();

            var incident = {
                "id": data.id,
                "incidentName": data.name,
                "level": data.selectedLevel,
                "status": data.selectedStatus,
                "type": data.selectedType.name
            };

            $http.put(INCIDENT_API, incident)
                .then(function(result) {
                        deferred.resolve(result);
                    },
                    function() {
                        deferred.reject();
                    });

            return deferred.promise;
        };

        var editIncidentLog = function(inputModel) {
            var deferred = $q.defer();

            $http.put("/Eoc/api/Incident/Log", inputModel)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        var deleteIncident = function(incidentId) {

            var deferred = $q.defer();

            $http.delete(INCIDENT_API + "/" + incidentId)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        // TODO: Move to global settings service
        var incidentTypes = [
            {
                "id": 1,
                "name": "Technological Hazard"
            },
            {
                "id": 2,
                "name": "Natural Disaster"
            }
        ];
        var getIncidentTypes = function() {
            return incidentTypes;
        };

        // TODO: Move to global settings service
        var incidentLevels = [1, 2, 3, 4, 5];
        var getIncidentLevels = function() {
            return incidentLevels;
        };

        // TODO: Move to global settings service
        var incidentStatuses = ["Active", "Inactive"];
        var getIncidentStatuses = function() {
            return incidentStatuses;
        };

        return {
            getIncident: getIncident,
            getIncidents: getIncidents,
            getIncidentLog: getIncidentLog,
            addIncident: addIncident,
            addIncidentLog: addIncidentLog,
            editIncident: editIncident,
            editIncidentLog: editIncidentLog,
            deleteIncident: deleteIncident,
            deleteIncidentLog: deleteIncidentLog,
            getIncidentTypes: getIncidentTypes,
            getIncidentLevels: getIncidentLevels,
            getIncidentStatuses: getIncidentStatuses
        };
    }

})()