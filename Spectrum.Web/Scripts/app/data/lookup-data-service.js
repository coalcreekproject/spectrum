(function() {

    angular
        .module('app.data')
        .factory('lookupDataService', lookupDataService);

    function lookupDataService($http, $q) {

        var _languages = [];
        var _timeZones = [];
        var _states = [];

        var _getLanguages = function() {

            var deferred = $q.defer();

            $http.get('/api/LanguageData')
                .then(function(result) {
                        // Successful
                        angular.copy(result.data, _languages);
                        deferred.resolve(_languages);
                    },
                    function() {
                        // Error
                        deferred.reject();
                    });
        }

        var _getTimeZones = function() {

            var deferred = $q.defer();

            $http.get('/api/UsTimeZoneData')
                .then(function(result) {
                        // Successful
                        angular.copy(result.data, _timeZones);
                        deferred.resolve(_timeZones);
                    },
                    function() {
                        // Error
                        deferred.reject();
                    });
        }

        var _getStates = function() {

            var deferred = $q.defer();

            $http.get('/api/UsStateData')
                .then(function(result) {
                        // Successful
                        angular.copy(result.data, _states);
                        deferred.resolve(_states);
                    },
                    function() {
                        // Error
                        deferred.reject();
                    });
        }

        return {
            getLanguages: _getLanguages,
            getTimeZones: _getTimeZones,
            getStates: _getStates
        };
    }
})();