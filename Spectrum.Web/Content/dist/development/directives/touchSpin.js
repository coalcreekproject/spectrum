"use strict";

(function () {

    angular.module("app.core").directive("touchSpin", touchSpin);

    function touchSpin() {
        return {
            restrict: "A",
            scope: {
                spinOptions: "="
            },
            link: function link(scope, element, attrs) {
                scope.$watch(scope.spinOptions, function () {
                    render();
                });
                var render = function render() {
                    $(element).TouchSpin(scope.spinOptions);
                };
            }
        };
    };
})();
