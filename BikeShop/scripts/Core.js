angular.module("bikeshop", ['ngRoute'])
       .config(['$routeProvider', function ($routeProvider) {
           $routeProvider.when("/", { templateUrl: 'Inventory' })
                         .when("/NewBike", { templateUrl: 'Inventory/BikeForm' })
                         .when("/EditBike/:BikeId", { templateUrl: 'Inventory/BikeForm' })
                         .otherwise("/");
       }])
       .controller("IndexController", ['$scope', '$http', function ($scope, $http) {
           $scope.Message = "Test";
       }])
       .controller("InventoryController", ['$scope', '$http', function ($scope, $http) {
           var reloadInventory = function () {
               $http.post('Inventory/GetInventory')
                   .success(function (data) {
                       $scope.Bikes = data.Value;
                   });
           };


           $scope.DeleteBike = function (bikeId) {
               if (confirm("Are you sure you want to delete this bike?")) {
                   $http.post("Inventory/DeleteBike", { id: bikeId })
                      .success(function () {
                          reloadInventory();
                      });
               }
           };

           reloadInventory();
       }])
       .controller("BikeController", ['$scope', '$http', '$routeParams', '$window', function ($scope, $http, $routeParams, $window) {
           if ($routeParams.BikeId) {
               $scope.Title = "Update Bike";

               $http.post("Inventory/GetBikeInfo", { id: $routeParams.BikeId })
                   .success(function (data) {
                       var bikeData = data.Value;

                       $scope.Bike = {
                           Id: $routeParams.BikeId,
                           Description: bikeData.Description,
                           Rating: bikeData.Rating,
                           Price: bikeData.Price,
                           Quantity: bikeData.Quantity,
                           Type: bikeData.Type
                       };
                   });
           } else {
               $scope.Title = "New Bike";

               $scope.Bike = {
                   Id: 0
               };
           }

           $scope.SaveBike = function () {
               $scope.Errors = [];

               $http.post("Inventory/SaveBike", $scope.Bike)
                   .success(function (data) {
                       if (data.Value) {
                           $window.location.href = "/";
                       } else {
                           $scope.Errors = data.Errors;
                       }
                   });

           };
       }]);