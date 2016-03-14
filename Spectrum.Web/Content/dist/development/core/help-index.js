'use strict';

angular.module('app').controller('HelpController', helpController).config(config);

function config($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    $stateProvider.state('index', {
        url: "",
        templateUrl: "/Templates/Help/Faq",
        data: { pageTitle: 'index' }
    });
}

function helpController() {}
