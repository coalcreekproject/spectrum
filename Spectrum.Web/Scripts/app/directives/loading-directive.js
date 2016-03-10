(function() {
    "use strict";

    angular.module("app")
        .directive("loading", loading);

    function loading() {
        return {
            restrict: "E",
            replace: true,
            template: "<div class=\"spinner\">" +
                "<div class=\"cube1\"></div> " +
                "<div class=\"cube2\"></div>" +
                "</div>",
            link: function(scope, element) {
                scope.$watch("loading", function(val) {
                    if (val) {
                        $(element).show();
                    }
                    else {
                        $(element).hide();
                    }
                });
            }
        };
    };

})();