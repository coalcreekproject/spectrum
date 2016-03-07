angular
    .module('app')
    .controller('CommunityController', communityController)
    .config(config);

function config($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    $stateProvider
        .state('index', {
            url: "",
            templateUrl: "/Templates/Community/CommunityIndex",
            data: { pageTitle: 'index' }
        });
    }

function communityController() {

}