angular.module('browserApp').service('pathService', pathService);

pathService.$inject = ['$http'];

function pathService($http) {
    var serviceScope = this;

    serviceScope.postIEnumeriable = function (path) {
        return $http.post("http://localhost:59443/api/home", JSON.stringify(path));
    }

    serviceScope.postFilesCount = function (path) {
        return $http.post("http://localhost:59443/api/meta", JSON.stringify(path));
    }
}