angular
    .module('app')
    .controller('identityFocusController', identityFocusController);

function userFocusParameters() {
    this.userId = null;
    this.organizationId = null;
};

function identityFocusController($scope, $http, $modal) {

    $scope.foo = ["one","two","three"];
    //service to hydrate from the ViewModel


    $scope.changeFocus = function() {
        var modalInstance = $modal.open({
            templateUrl: '/Templates/Portal/ChangeUserFocusModal',
            controller: changeIdentityFocusModalController,
            resolve : { foo: function() { return $scope.foo; } }
        });
    }
}

function changeIdentityFocusModalController($scope, $http, $modalInstance, foo) {

 

    $scope.ok = function (identityFocusViewModel) {

        var model = identityFocusViewModel;

        // Do some work
        $http.post("/Portal/ChangeIdentityFocus", { data: model }).error(function (responseData) {
            console.log("Error !" + responseData);
        });
        
        $modalInstance.close();
    };

    $scope.cancel = function() {
        $modalInstance.dismiss('cancel');
    };
}
