angular
    .module('app')
    .controller('CheckListsController', CheckListsController)
    .config(config);

function config($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(true);

    // Set default state
    //$urlRouterProvider.otherwise("/dashboard");

    $stateProvider
        .state('index', {
            url: "",
            templateUrl: "/Eoc/Templates/CheckList/CheckListIndex",
            data: { pageTitle: 'index' }
        });
}

function userRoleParameters() {
    this.userId = null;
    this.organizationId = null;
};

function CheckListsController($scope) {
    // Handle actions
    $scope.remove = function (scope) {
        scope.remove();
    };
    $scope.toggle = function (scope) {
        scope.toggle();
    };
    $scope.moveLastToTheBeginning = function () {
        var a = $scope.data.pop();
        $scope.data.splice(0, 0, a);
    };
    $scope.newSubItem = function (scope) {
        var nodeData = scope.$modelValue;
        nodeData.nodes.push({
            id: nodeData.id * 10 + nodeData.nodes.length,
            title: nodeData.title + '.' + (nodeData.nodes.length + 1),
            nodes: []
        });
    };
    $scope.collapseAll = function () {
        $scope.$broadcast('collapseAll');
    };
    $scope.expandAll = function () {
        $scope.$broadcast('expandAll');
    };

    // Nestable list example data
    $scope.data = [{
        "id": 1,
        "title": "Sem fringilla",
        "nodes": [
            {
                "id": 11,
                "title": "Nisl lacus et, ultricies",
                "nodes": [
                    {
                        "id": 111,
                        "title": "Congue hac",
                        "nodes": []
                    }
                ]
            },
            {
                "id": 12,
                "title": "Consectetuer orci mollis",
                "nodes": []
            }
        ],
    }, {
        "id": 2,
        "title": "Gravida morbi non",
        "nodes": [
            {
                "id": 21,
                "title": "Lorem aliquam",
                "nodes": []
            },
            {
                "id": 22,
                "title": "Inceptos nibh",
                "nodes": []
            }
        ],
    }, {
        "id": 3,
        "title": "Pede hymenaeos",
        "nodes": [
            {
                "id": 31,
                "title": "Magnis morbi orci",
                "nodes": []
            },
            {
                "id": 32,
                "title": "Ad tortor, auctor dui",
                "nodes": []
            },
            {
                "id": 33,
                "title": "Orci magnis, mauris",
                "nodes": []
            }
        ],
    }];
}