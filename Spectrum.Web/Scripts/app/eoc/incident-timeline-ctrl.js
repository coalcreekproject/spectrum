(function() {
    'use strict';

    angular
        .module('app.eoc')
        .controller('IncidentTimelineCtrl', IncidentTimelineCtrl)
        .config(config);

    function config($locationProvider, $stateProvider, $urlRouterProvider, $compileProvider) {

        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);
    }

    IncidentTimelineCtrl.$inject = ['$scope', '$stateParams', 'incidentData'];

    function IncidentTimelineCtrl($scope, $stateParams, incidentData) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Incident Timeline';
        vm.name = 'Timeline';
        vm.events = [];
        vm.incidentId = $stateParams.incidentId;

        // optional: not mandatory (uses angular-scroll-animate) 
        vm.animateElementIn = function ($el) {
            $el.removeClass('timeline-hidden');
            $el.addClass('bounce-in');
        };

        // optional: not mandatory (uses angular-scroll-animate)
        vm.animateElementOut = function ($el) {
            $el.addClass('timeline-hidden');
            $el.removeClass('bounce-in');
        };

        function getIncident() {
            $scope.loading = true;
            return incidentData.getIncident(vm.incidentId)
                .then(function(incident) {
                    mapIncidentsToEvents(angular.copy(incident));
                    $scope.loading = false;
                    },
                    function() {
                        $scope.loading = false;
                        console.log('Error loading Incident.  See log.');
                    });
        }

        function mapIncidentsToEvents(incident)
        {
            //Just get the first incident
            //var incident = incidents[1];
            var logs = incident.logs;

            if (logs !== 0) {
                logs.forEach(addToEventsScope);
            }

            function addToEventsScope(element) {

                var badgeClass, badgeIconClass;
                switch (getNumbersForTitles(element))
                {
                    case 1:
                        badgeClass = 'warning';
                        badgeIconClass = 'pe-7s-look';
                        break;
                    case 2:
                        badgeClass = 'danger';
                        badgeIconClass = 'glyphicon glyphicon-fire';
                        break;
                    case 3:
                        badgeClass = 'warning';
                        badgeIconClass = 'fa fa-ambulance';
                        break;
                    case 4:
                        badgeClass = 'info';
                        badgeIconClass = 'fa-hospital';
                        break;
                    case 5:
                        badgeClass = 'info';
                        badgeIconClass = 'fa fa-hospital';
                        break;
                    case 6:
                        badgeClass = 'primary';
                        badgeIconClass = 'fa fa-certificate';
                        break;
                    case 7:
                        badgeClass = 'primary';
                        badgeIconClass = 'fa fa-certificate';
                        break;
                    default:
                        badgeClass = 'info';
                        badgeIconClass = 'glyphicon glyphicon-asterisk';
                }

                var event = {
                    badgeClass: badgeClass,
                    badgeIconClass: badgeIconClass,
                    name: element.logName,
                    title: element.logTitle,
                    entry: element.logEntry,
                    createDate: element.logDate
                }
                vm.events.push(event);
            }
        }

        function getNumbersForTitles(element) {

            var substring = "SAR";
            if (element.logTitle.indexOf(substring) > -1)
                return 1;

            substring = "Fire";
            if (element.logTitle.indexOf(substring) > -1)
                return 2;
    
            substring = "EMT";
            if (element.logTitle.indexOf(substring) > -1)
                return 3;

            substring = "BCH";
            if (element.logTitle.indexOf(substring) > -1)
                return 4;

            substring = "Hospital";
            if (element.logTitle.indexOf(substring) > -1)
                return 5;

            substring = "Police";
            if (element.logTitle.indexOf(substring) > -1)
                return 6;

            substring = "Sheriff";
            if (element.logTitle.indexOf(substring) > -1)
                return 7;

            return -1;
        }

        function activate()
        {
            return getIncident();
        }

        // Activate page on load
        activate();

    }
})();