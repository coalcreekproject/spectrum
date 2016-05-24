(function() {
    'use strict';

    angular.module('app.data')
        .service('incidentLogData', incidentLogData);

    incidentLogData.$inject = ['$http', '$q', 'apiConfig'];

    function incidentLogData($http, $q, apiConfig) {
        var incidentLog = {};

        // GET:
        var getIncidentLog = function(incidentId, logId) {

            var deferred = $q.defer();

            $http.get(apiConfig.urls.incident + incidentId + '/Log/' + logId)
                .then(function(result) {
                        angular.copy(result.data, incidentLog);
                        deferred.resolve(incidentLog);
                    },
                    function() {
                        deferred.reject();
                    });

            return deferred.promise;
        };

        // POST:
        var addIncidentLog = function(inputModel) {
            var deferred = $q.defer();

            $http.post(apiConfig.urls.incident + '/Log', inputModel)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        // PUT:
        var editIncidentLog = function(inputModel) {
            var deferred = $q.defer();

            $http.put(apiConfig.urls.incident + '/Log', inputModel)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        // DELETE:
        var deleteIncidentLog = function(incidentId, logId) {
            var deferred = $q.defer();

            $http.delete(apiConfig.urls.incident + incidentId + '/Log/' + logId)
                .then(function(result) {
                    deferred.resolve(result);
                }, function() {
                    deferred.reject();
                });

            return deferred.promise;
        };

        return {
            getIncidentLog: getIncidentLog,
            addIncidentLog: addIncidentLog,
            editIncidentLog: editIncidentLog,
            deleteIncidentLog: deleteIncidentLog
        };
    }

})()