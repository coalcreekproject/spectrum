(function() {
    angular
        .module('app.eoc')
        .controller('PositionLogCtrl', PositionLogCtrl)
        .config(config);

    config.$inject = ['$locationProvider', '$stateProvider', '$urlRouterProvider', '$compileProvider'];

    function config($locationProvider, $stateProvider, $urlRouterProvider, $compileProvider) {
        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);

        $stateProvider
            .state('index', {
                url: '',
                templateUrl: '/Eoc/Templates/PositionLog/PositionLogIndex',
                controller: 'PositionLogCtrl',
                controllerAs: 'pl'
            });
    }

    PositionLogCtrl.$inject = ['$scope', 'incidentData'];

    function PositionLogCtrl($scope, incidentData) {

        var vm = this;

        vm.incidents = [];
        vm.search = '';
        vm.clear = clear;

        function clear() {
            vm.search = '';
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
    }

})();