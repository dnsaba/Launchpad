﻿(function () {
    "use strict";
    var myApp = angular.module("publicApp" + '.routes', []);
    myApp.config(function ($stateProvider, $locationProvider, $urlRouterProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false,
        });
        $urlRouterProvider.otherwise('/home');

        $stateProvider
       
            .state('home', {
                name: 'home',
                url: '/home',
                templateUrl: '/app/public/modules/home/home.html',
                controller: "homeController as homeCtrl"
            });

    });
})();

