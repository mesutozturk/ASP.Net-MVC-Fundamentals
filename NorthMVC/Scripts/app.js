/// <reference path="angular.js" />

var app = angular.module("north", []);

app.controller("sepetCtrl", function ($scope, $http) {
    $scope.sepet = [];
    $scope.total = 0;
    $scope.sepeteekle = function (id) {
        $http.get("http://localhost:22928/Product/urundetay/" + id).success(function (urun) {
            if (urun.success == true) {
                var urunvarmi = false;
                for (var i = 0; i < $scope.sepet.length; i++) {
                    if ($scope.sepet[i].id == urun.data.id) {
                        urunvarmi = true;
                        $scope.sepet[i].count++;
                    }
                }
                if (!urunvarmi) {
                    urun.data.count = 1;
                    $scope.sepet.push(urun.data);
                }
                sepettopla($scope.sepet);
            }
        });
        function sepettopla(sepet) {
            $scope.total = 0;
            for (var i = 0; i < sepet.length; i++) {
                $scope.total += sepet[i].count * sepet[i].price;
            }
        }
    }
    $scope.sepetitemizle = function () {
        $scope.sepet = [];
        $scope.total = 0;
    };
});