"use strict";

(function () {

    "use strict";

    angular.module("app.data").service("incidentData", incidentData);

    incidentData.$inject = ["$http", "$q"];

    function incidentData($http, $q) {

        var INCIDENT_API = "/Eoc/api/Incidents";

        var incidents = [];
        var getIncidents = function getIncidents() {

            var deferred = $q.defer();

            $http.get(INCIDENT_API).then(function (result) {
                angular.copy(result.data, incidents);
                deferred.resolve(incidents);
            }, function () {
                deferred.reject();
            });

            return deferred.promise;
        };

        var addIncident = function addIncident(incident) {

            var deferred = $q.defer();

            $http.post(INCIDENT_API, incident).then(function (result) {
                deferred.resolve(result);
            }, function () {
                deferred.reject();
            });

            return deferred.promise;
        };

        var editIncident = function editIncident(data) {

            var deferred = $q.defer();

            var incident = {
                "id": data.id,
                "incidentName": data.name,
                "level": data.selectedLevel,
                "status": data.selectedStatus,
                "type": data.selectedType.name
            };

            $http.put(INCIDENT_API, incident).then(function (result) {
                deferred.resolve(result);
            }, function () {
                deferred.reject();
            });

            return deferred.promise;
        };

        var deleteIncident = function deleteIncident(incidentId) {

            var deferred = $q.defer();

            $http.delete(INCIDENT_API + "/" + incidentId).then(function (result) {
                deferred.resolve(result);
            }, function () {
                deferred.reject();
            });

            return deferred.promise;
        };

        // TODO: Move to global settings service
        var incidentTypes = [{
            "id": 1,
            "name": "Technological Hazard"
        }, {
            "id": 2,
            "name": "Natural Disaster"
        }, {
            "id": 3,
            "name": "Fast Food Hazard"
        }];
        var getIncidentTypes = function getIncidentTypes() {
            return incidentTypes;
        };

        // TODO: Move to global settings service
        var incidentLevels = [1, 2, 3, 4, 5];
        var getIncidentLevels = function getIncidentLevels() {
            return incidentLevels;
        };

        // TODO: Move to global settings service
        var incidentStatuses = ["Active", "Inactive", "Just Fucking Around"];
        var getIncidentStatuses = function getIncidentStatuses() {
            return incidentStatuses;
        };

        return {
            getIncidents: getIncidents,
            addIncident: addIncident,
            editIncident: editIncident,
            deleteIncident: deleteIncident,
            getIncidentTypes: getIncidentTypes,
            getIncidentLevels: getIncidentLevels,
            getIncidentStatuses: getIncidentStatuses
        };
    }
})();
