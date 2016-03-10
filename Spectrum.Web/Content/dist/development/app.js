"use strict";

(function () {

    "use strict";

    angular.module("app", ["ui.router", // Angular flexible routing
    "ngSanitize", // Angular-sanitize
    "ui.bootstrap", // AngularJS native directives for Bootstrap
    "angular-flot", // Flot chart
    "angles", // Chart.js
    "angular-peity", // Peity (small) charts
    "cgNotify", // Angular notify
    "angles", // Angular ChartJS
    "ngAnimate", // Angular animations
    "ui.map", // Ui Map for Google maps
    "ui.calendar", // UI Calendar
    "summernote", // Summernote plugin
    "ui.tree", // Angular ui Tree
    "bm.bsTour", // Angular bootstrap tour
    "xeditable", // Angular-xeditable
    "ui.select", // AngularJS ui-select
    "ui.sortable", // AngularJS ui-sortable
    "ngTouch", "ui.grid", // Angular UI Grid
    "ui.grid.selection", // Selection tools for Ui Grid
    "dndLists"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider", "$compileProvider"];

    function config($stateProvider, $urlRouterProvider, $compileProvider) {

        // Optimize load start with remove binding information inside the DOM element
        $compileProvider.debugInfoEnabled(true);

        // Set default state
        //$urlRouterProvider.otherwise("/dashboard");

        $stateProvider.state("index", {
            url: "",
            templateUrl: "/Templates/Portal/PortalIndex",
            data: {
                pageTitle: "Main"
            }
        });
    }
})();
