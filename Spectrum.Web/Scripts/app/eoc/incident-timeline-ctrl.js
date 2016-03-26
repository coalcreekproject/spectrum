(function() {
    'use strict';

    angular
        .module('app.eoc')
        .controller('IncidentTimelineCtrl', IncidentTimelineCtrl)
        .config(config);

    function config($locationProvider, $stateProvider, $urlRouterProvider, $compileProvider) {

        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);

        // Set default state
        $urlRouterProvider.otherwise('/dashboard');

        $stateProvider
            .state('index', {
                url: '',
                templateUrl: '/Eoc/Templates/IncidentTimeline/IncidentTimelineIndex',
                controller: 'IncidentTimelineCtrl',
                controllerAs: 'vm'
            });
    }

    IncidentTimelineCtrl.$inject = ['$scope', 'incidentData'];

    function IncidentTimelineCtrl($scope, incidentData) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Incident Timeline';
        vm.name = 'Sean';
        vm.events = [];

        function getIncidents()
        {
            $scope.loading = true;
            return incidentData.getIncidents()
                .then(function (incidents)
                {
                    mapIncidentsToEvents(angular.copy(incidents));
                    $scope.loading = false;
                },
                    function ()
                    {
                        $scope.loading = false;
                        console.log('Error loading incidents.  See log.');
                    });
        }

        function mapIncidentsToEvents(incidents)
        {
            if (incidents.length !== 0) {
                incidents.forEach(addToEventsScope);
            }

            function addToEventsScope(element) {

                var badgeClass, badgeIconClass;
                switch (element.level)
                {
                    case 1:
                        badgeClass = 'danger';
                        badgeIconClass = 'glyphicon-exclamation-sign';
                        break;
                    case 2:
                        badgeClass = 'warning';
                        badgeIconClass = 'glyphicon-warning-sign';
                        break;
                    case 3:
                        badgeClass = 'info';
                        badgeIconClass = 'glyphicon-eye-open';
                        break;
                    case 4:
                        badgeClass = 'success';
                        badgeIconClass = 'glyphicon-pencil';
                        break;
                    case 5:
                        badgeClass = 'primary';
                        badgeIconClass = 'glyphicon-pencil';
                        break;
                    default:
                        badgeClass = 'primary';
                        badgeIconClass = 'glyphicon-pencil';
                }

                var event = {
                    badgeClass: badgeClass,
                    badgeIconClass: badgeIconClass,
                    title: element.incidentName,
                    level: element.level,
                    status: element.status,
                    createDate: element.createDate
                }
                vm.events.push(event);
            }
        }

        function activate()
        {
            return getIncidents();
        }

        // Activate page on load
        activate();

    }
})();