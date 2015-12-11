angular
    .module('app')
    .controller('CheckListsController', CheckListsController);

function userRoleParameters() {
    this.userId = null;
    this.organizationId = null;
};

function UserRolesModalController($scope, $modalInstance, userRoleFactory, user) {
}