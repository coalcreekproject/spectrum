(function() {

    'use strict';

    angular
        .module("app.data")
        .factory("organizationFactory", organizationFactory);

    //organizationFactory.$inject["$http", "$q"];

    function organizationFactory($http, $q) {

        var organizations = [];
        var organizationTypes = [];

        function getOrganizationTypes() {

            var deferred = $q.defer();

            $http.get('/api/OrganizationType')
                .then(function (result) {
                    // Successful
                    angular.copy(result.data, organizationTypes);
                    deferred.resolve(organizationTypes);
                },
                    function () {
                        // Error
                        deferred.reject();
                    });

            return deferred.promise;
        };


        function getOrganizations() {

            var deferred = $q.defer();

            $http.get('/api/Organizations')
                .then(function (result) {
                    // Successful
                    angular.copy(result.data, organizations);
                    deferred.resolve(organizations);
                },
                    function () {
                        // Error
                        deferred.reject();
                    });

            return deferred.promise;
        };

        function addOrganization(newOrganization) {

            var deferred = $q.defer();

            $http.post('/api/Organizations', newOrganization)
                .then(function (result) {
                    // success
                    var newlyCreatedOrganization = result.data;
                    organizations.splice(0, 0, newlyCreatedOrganization);
                    deferred.resolve(newlyCreatedOrganization);
                },
                    function () {
                        // error
                        deferred.reject();
                    });

            return deferred.promise;
        };

        function editOrganization(organization) {

            var deferred = $q.defer();

            $http.put('/api/Organizations/' + organization.id, organization)
                .then(function (result) {
                    // success
                    var editedOrganization = result.data;

                    for (var i = 0; i < organizations.length; i++) {
                        if (organizations[i].id === editedOrganization.id) {
                            organizations[i].name = editedOrganization.name;
                            organizations[i].organizationTypeId = editedOrganization.organizationTypeId;
                            organizations[i].organizationTypeName = editedOrganization.organizationType.name;

                            break;
                        }
                    }

                    deferred.resolve(editedOrganization);
                },
                    function () {
                        // error
                        deferred.reject();
                    });

            return deferred.promise;
        };

        function deleteOrganization(organization) {

            var deferred = $q.defer();

            $http.delete('/api/Organizations/' + organization.id, organization)
                .then(function (result) {

                    var deletedOrganization = result.data;

                    for (var i = 0; i < organizations.length; i++) {
                        if (organizations[i].id === deletedOrganization.id) {
                            organizations.splice(i, 1);
                            break;
                        }
                    }

                    deferred.resolve();
                },
                    function () {
                        // error
                        deferred.reject();
                    });

            return deferred.promise;
        };
        return {
            organizationTypes: organizationTypes,
            getOrganizationTypes: getOrganizationTypes,
            organizations: organizations,
            getOrganizations: getOrganizations,
            addOrganizations: addOrganization,
            editOrganizations: editOrganization,
            deleteOrganizations: deleteOrganization
        };
    }
})();