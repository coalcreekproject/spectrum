(function() {

    'use strict';

    angular.module('app.data')
        .service('incidentData', incidentData);

    incidentData.$inject = ['$http', '$q', 'apiConfig'];

    function incidentData($http, $q, apiConfig) {

        // GET Incidents
        var incidents = [];
        var getIncidents = function() {

            var deferred = $q.defer();

            $http.get(apiConfig.urls.incidents)
                .then(function(result) {
                        angular.copy(result.data, incidents);
                        deferred.resolve(incidents);
                    },
                    function() {
                        deferred.reject();
                    });

            return deferred.promise;
        };

        // GET Incident
        var incident = {};
        var getIncident = function(incidentId) {
            var deferred = $q.defer();

            $http.get(apiConfig.urls.incident + '/' + incidentId)
                .then(function(result) {
                        angular.copy(result.data, incident);
                        deferred.resolve(incident);
                    },
                    function() {
                        deferred.reject();
                    });

            return deferred.promise;
        };

        // POST:
        var addIncident = function(incident) {

            var deferred = $q.defer();

            $http.post(apiConfig.urls.incidents, incident)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        // PUT:
        var editIncident = function(data) {

            var deferred = $q.defer();

            var model = {
                "id": data.id,
                "incidentName": data.name,
                "level": data.selectedLevel,
                "status": data.selectedStatus,
                "type": data.selectedType.name
            };

            $http.put(apiConfig.urls.incidents, model)
                .then(function(result) {
                        deferred.resolve(result);
                    },
                    function() {
                        deferred.reject();
                    });

            return deferred.promise;
        };

        // DELETE:
        var deleteIncident = function(incidentId) {

            var deferred = $q.defer();

            $http.delete(apiConfig.urls.incidents + '/' + incidentId)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        return {
            getIncident: getIncident,
            getIncidents: getIncidents,
            addIncident: addIncident,
            editIncident: editIncident,
            deleteIncident: deleteIncident
        };
    }

})()