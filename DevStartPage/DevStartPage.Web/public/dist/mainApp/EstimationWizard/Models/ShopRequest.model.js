"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var index_1 = require("./index");
var ShopRequest = /** @class */ (function () {
    function ShopRequest() {
        this.ClientId = '';
        this.SelectedServiceName = '';
        this.SelectedServiceCategory = '';
        this.InsuranceInfo = new index_1.InsuranceInfo();
        this.NThriveId = '';
        this.PatientInfo = new index_1.PatientInformation();
        this.SelectedService = new Array();
        this.PatientType = '';
        this.ServiceType = '';
        this.IcdDiagnoses = new Array();
        this.IcdProcedures = new Array();
        this.primaryCptCode = '';
        this.SelectedServiceSyskey = '';
        this.SelectedServiceId = '';
    }
    return ShopRequest;
}());
exports.ShopRequest = ShopRequest;

//# sourceMappingURL=ShopRequest.model.js.map
