(function () {
    'use strict';

    angular.module('browserApp', []);

    angular.module('browserApp').controller('browsingCtrl', browsingCtrl);

    browsingCtrl.$inject = ['pathService'];

    function browsingCtrl(pathService) {
        var vm = this;

        vm.changePath = function (CurrentPath) {
            pathService.postIEnumeriable(CurrentPath).then(function (responce) {
                vm.items = responce.data;
            });
            pathService.postFilesCount(CurrentPath).then(function (responce) {
                vm.lessTen = responce.data.CountFilesLess;
                vm.middle = responce.data.CountFilesMiddle;
                vm.moreHundred = responce.data.CountFilesMore;
            });

            vm.currentPath = CurrentPath;
        }
        vm.currentPath = 'root';
        vm.items = vm.changePath(vm.currentPath);

        vm.lessTen = 0;
        vm.middle = 0;
        vm.moreHundred = 0;
    }
})();