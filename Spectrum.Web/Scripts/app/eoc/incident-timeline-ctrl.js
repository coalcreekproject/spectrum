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

    IncidentTimelineCtrl.$inject = ['$scope'];

    function IncidentTimelineCtrl($scope) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Incident Timeline';
        vm.name = 'Sean';

        $scope.loading = false;


        activate();

        function activate() {
        }
    }
})();