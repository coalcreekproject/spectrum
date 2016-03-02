(function() {

    "use strict";

    angular
        .module("app", [

            /*
             * Our core 3rd party dependencies
             */
            "app.core",

            /*
             * Data services and factories
             */
            "app.data",

            /*
            * Our main modules
            */
            "app.portal",
            "app.eoc"
        ])
        .config(config);

    config.$inject = ["$routeProvider", "$locationProvider"];

    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when("/", {
                templateUrl: "/Home/Home",
                controller: "MainCtrl"
            })
            .when("/Home", {
                templateUrl: "/Home/Home",
                controller: "MainCtrl"
            })
            .when("/Eoc/IncidentManagement", {
                templateUrl: "Eoc/Templates/IncidentManagement/IncidentManagementIndex",
                controller: "IncidentMgmtCtrl",
                controllerAs: "im"
            })
            .when("/Test/view1", {
                template: "<h1>Hi there.</h1>",
                controller: "EocDashboardCtrl",
                controllerAs: "d"
            })
            .otherwise({
                redirectTo: "/Test/view1"
            });

        // Specify HTML5 mode
        $locationProvider.html5Mode(true);
    }

})();