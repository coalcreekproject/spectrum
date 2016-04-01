angular
    .module('app.eoc')
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
    $scope.data = [
        {
            "id": 1,
            "title": "FR-7800B Respirator",
            "nodes": [
                {
                    "id": 11,
                    "title": "High Efficiency Cartridge FR-57/453-03-02R06",
                    "nodes": [
                        {
                            "id": 111,
                            "title": "FR-M40 Cartridge Substitute (FR-15-CBRN)",
                            "nodes": []
                        }
                    ]
                },
                {
                    "id": 12,
                    "title": "Surefire 6P Flashlight",
                    "nodes": [
                                            {
                                                "id": 111,
                                                "title": "CR123 Batteries (x2)",
                                                "nodes": []
                                            }]
                },
                {
                    "id": 12,
                    "title": "Surefire Flashlight",
                    "nodes": [
                        {
                            "id": 111,
                            "title": "CR123 Batteries (x6)",
                            "nodes": []
                        }
                    ]
                }
            ]
        },
        {
            "id": 2,
            "title": "Motorola APX Radio",
            "nodes": [
                {
                    "id": 21,
                    "title": "Rechargable Battery Pack One",
                    "nodes": []
                },
                {
                    "id": 22,
                    "title": "Rechargable Battery Pack Two",
                    "nodes": []
                },
                {
                    "id": 23,
                    "title": "PTT Keyset",
                    "nodes": []
                }
            ]
        },
        {
            "id": 3,
            "title": "Supplemental 196A - Canine Handler Equipment",
            "nodes": [
                {
                    "id": 31,
                    "title": "Harness",
                    "nodes": []
                },
                {
                    "id": 31,
                    "title": "35 FT reflective lead",
                    "nodes": []
                },
                {
                    "id": 31,
                    "title": "50 FT reflective lead",
                    "nodes": []
                }
            ]
        }
    ];
}