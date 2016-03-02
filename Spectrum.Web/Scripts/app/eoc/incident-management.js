(function() {

    angular
        .module("app.eoc")
        .controller("IncidentMgmtCtrl", IncidentMgmtCtrl);

    function IncidentMgmtCtrl() {

        var vm = this;

        vm.name = "Sean";
        vm.incidents = [
            {
                incidentName: "Wild Canyon Fire",
                type: "Natural Disaster",
                level: 5,
                status: "Active"
            },
            {
                "incidentName": "Mile High Island",
                "type": "Technological Hazard",
                "level": 1,
                "status": "Active"
            },
            {
                incidentName: "New County Floods",
                type: "Natural Disaster",
                level: 2,
                status: "Inactive"
            }
        ];
    }

})();