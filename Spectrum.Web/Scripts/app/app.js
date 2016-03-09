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
            "app.portal"
            //"app.eoc"
        ]);

})();