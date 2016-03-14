'use strict';

angular.module('app').controller('FileManagerController', fileManagerController).config(config);

function config($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    $stateProvider.state('index', {
        url: "",
        templateUrl: "/Templates/FileManager/FileManagerIndex",
        data: { pageTitle: 'index' }
    });
}

function fileManagerController() {}
