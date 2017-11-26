(function () {
    'use strict';

    var app = angular.module("myApp", []);

    var MainController = function ($scope, $http) {

        $scope.message = "Hello Angular";

        $http.get("api/Home").then(function (response) {
            $scope.items = response.data;
        });
    }
    app.controller("MainController", ["$scope", "$http", MainController]);

})();