(function() {
    'use strict';

    angular
        .module('app')
        .factory('globalConfigService', globalConfigService);

    function globalConfigService() {

        // Declare global config values
        var incidentTypes = [
            {
                "id": 1,
                "name": 'Technological Hazard'
            },
            {
                "id": 2,
                "name": 'Natural Disaster'
            }
        ];
        var incidentLevels = [1, 2, 3, 4, 5];
        var incidentStatuses = ['Active', 'Inactive'];

        // Export values
        var getIncidentTypes = function() {
            return incidentTypes;
        };

        var getIncidentLevels = function() {
            return incidentLevels;
        };

        var getIncidentStatuses = function() {
            return incidentStatuses;
        };

        var service = {
            getIncidentTypes: getIncidentTypes,
            getIncidentLevels: getIncidentLevels,
            getIncidentStatuses: getIncidentStatuses
        };

        return service;
    }
})();