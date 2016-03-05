angular
    .module('app')
    .controller('FileManagerController', fileManagerController)
    .config(config);

function config($locationProvider, $stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    $stateProvider
        .state('index', {
            url: "",
            templateUrl: "/Templates/FileManager/FileManagerIndex",
            data: { pageTitle: 'index' }
        });
    }

function fileManagerController() {
    //var defaults = config.$get();
    //config.set({
    //    appName: 'my angular-filemanager',
    //    allowedActions: angular.extend(defaults.allowedActions, {
    //        remove: true
    //    })
    //});
}