"use strict";

(function () {

    angular.module("app.core").directive("minimalizaMenu", minimalizaMenu);

    minimalizaMenu.$inject = ["$rootScope"];

    function minimalizaMenu($rootScope) {
        return {
            restrict: "EA",
            template: '<div class="header-link hide-menu" ng-click="minimalize()"><i class="fa fa-bars"></i></div>',
            controller: function controller($scope, $element) {

                $scope.minimalize = function () {
                    if ($(window).width() < 769) {
                        $("body").toggleClass("show-sidebar");
                    } else {
                        $("body").toggleClass("hide-sidebar");
                    }
                };
            }
        };
    };
})();
