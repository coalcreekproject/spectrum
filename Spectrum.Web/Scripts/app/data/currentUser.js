(function() {

    angular
        .module("app.data")
        .factory("currentUserFactory", currentUserFactory);

    currentUserFactory.$inject = ["$http", "$q"];

    function currentUserFactory($http, $q) {

        var _currentUser = {
            Id: null,
            UserName: null,
            Email: null,
            SelectedOrganizationId: null,
            SelectedOrganizationName: null,
            SelectedRoleId: null,
            SelectedRoleName: null,
            SelectedPositionId: null,
            SelectedPositionName: null,
            UserOrganizations: null,
            UserRoles: null,
            UserProfiles: null,
            UserPositions: null
        };

        var _getCurrentUser = function() {

            var deferred = $q.defer();

            $http.get("/api/IdentityFocus/")
                .then(function(result) {
                        // Successful
                        angular.copy(result.data, _currentUser);
                        deferred.resolve(_currentUser);
                    },
                    function() {
                        // Error
                        deferred.reject();
                    });

            return deferred.promise;
        };

        var _editCurrentUser = function(currentUser) {

            var deferred = $q.defer();

            $http.put("/api/IdentityFocus/", currentUser)
                .then(function(result) {
                        // success
                        var editedUser = result.data;

                        //currentUser.SelectedOrganizationId = editedUser.SelectedOrganizationId;
                        //currentUser.SelectedOrganizationName = editedUser.SelectedOrganizationName;
                        //currentUser.SelectedRoleId = editedUser.SelectedRoleId;
                        //currentUser.SelectedRoleName = editedUser.SelectedRoleName;
                        //currentUser.SelectedPositionId = editedUser.SelectedPositionId;
                        //currentUser.SelectedPositionName = editedUser.SelectedPositionName;

                        deferred.resolve(editedUser);
                    },
                    function() {
                        // error
                        deferred.reject();
                    });

            return deferred.promise;
        };

        return {
            currentUser: _currentUser,
            getCurrentUser: _getCurrentUser,
            editCurrentUser: _editCurrentUser
        };
    };
})();