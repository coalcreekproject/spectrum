(function() {
    "use strict";

    angular.module("app.portal", [

            /*
            * Each module needs access to these
            */
            "app.core",
            "app.data" // needs core
            //"app.components", // (directives)needs core
        ])
        .config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider", "$compileProvider"];

    function config($stateProvider, $urlRouterProvider, $compileProvider) {

        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);

        // Define main state
        $stateProvider
            .state("portal-index", {
                url: "",
                templateUrl: "/Templates/Portal/PortalIndex",
                data: {
                    pageTitle: "Main"
                }
            });
    };

})();