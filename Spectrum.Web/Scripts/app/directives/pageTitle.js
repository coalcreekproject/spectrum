(function() {

    angular.module("app.core")
        .directive("pageTitle", pageTitle);

    pageTitle.$inject = ["$rootScope", "$timeout"];

    function pageTitle($rootScope, $timeout) {
        return {
            link: function(scope, element) {

                const listener = function(event, toState, toParams, fromState, fromParams) {
                    // Default title
                    var title = "Spectrum Operational | AngularJS Responsive WebApp";
                    // Create your own title pattern
                    if (toState.data && toState.data.pageTitle)
                        title = `Spectrum Operational | ${toState.data.pageTitle}`;
                    $timeout(function() {
                        element.text(title);
                    });
                };
                $rootScope.$on("$stateChangeStart", listener);
            }
        };
    };

})();