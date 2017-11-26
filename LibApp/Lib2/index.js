(function () {
    'use strict';

    var app = angular.module("myApp", []);

    var MainController = function ($scope, $http) {

        $scope.vm = {};
        $scope.vm.searchBook = '';
        $scope.message = "Hello Angular";
        $scope.selectedOption = null;

        $http.get("api/Default/Get").then(function (response) {
            $scope.items = response.data;
        });

        $scope.addBook = function () {          
            $http({
                url: '/api/Default/PostAddBook',
                method: "POST",
                data: {
                    'Author': $scope.bookAuthor,
                    'Title': $scope.bookTitle,
                    'IsAvailable': true,
                    'Id': 0                    
                },
                headers: { 'Content-Type': 'application/json' }
            })
               .then(function (response) {
                   // success
                   //alert("New Item Added");
                   $scope.showBooks();
                   $scope.bookAuthor = null;
                   $scope.bookTitle = null;
               });
        };

        $scope.addNewBorrower = function () {
            $http({
                url: '/api/Default/PostAddBorrower',
                method: "POST",
                data: {
                    'FirstName': $scope.firstName,
                    'LastName': $scope.lastName,
                    'Id': 0
                },
                headers: { 'Content-Type': 'application/json' }
            })
               .then(function (response) {                  
                   $scope.showBorrowers();
                   $scope.firstName = null;
                   $scope.lastName = null;
               });
        };

        $scope.resetSelectedValues = function () {
            $scope.selectedBook = null;
            $scope.selectedBorrower = null;
        };


        $scope.borrowBook = function () {
            $http({
                url: '/api/Default/PostBorrowBook',
                method: "POST",
                data: {
                    'BookId': $scope.selectedBook.Id,
                    'BorrowerId': $scope.selectedBorrower.Id,
                    'Id': 0
                },
                headers: { 'Content-Type': 'application/json' }
            })
               .then(function (response) {
                   $scope.showBooks();
               });           
            
        };

        $scope.showBooks = function () {
            $scope.selectedOption = "Books";
            $http.get("api/Default/GetBooks").then(function (response) {
                $scope.books = response.data;
            });

            $http.get("api/Default/GetBorrowers").then(function (response) {
                $scope.borrowers = response.data;
            });;
        }

        $scope.showBorrowers = function () {
            $scope.selectedOption = "Borrowers";
            $http.get("api/Default/GetBorrowers").then(function (response) {
                $scope.borrowers = response.data;
            });
        }

        $scope.showPendingBooks = function () {
            $scope.selectedOption = "PendingBooks";
            $http.get("api/Default/GetPendingBooks").then(function (response) {
                $scope.pendingBooks = response.data;
            });
        }

        $scope.$watch('vm.searchBook', function () {
            var search = $scope.vm.searchBook;
            $http({
                url: '/api/Default/PostSearchBooks',
                method: "POST",
                data: {
                    'SearchText': $scope.vm.searchBook,
                },
                headers: { 'Content-Type': 'application/json' }
            })
                .then(function (response) {
                    $scope.books = response.data;
                });
        });
    }
    app.controller("MainController", ["$scope", "$http", MainController]);

})();