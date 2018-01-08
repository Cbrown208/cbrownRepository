"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var index_1 = require("../Models/index");
var InsuranceInfo = /** @class */ (function () {
    function InsuranceInfo() {
        this.referenceNumber = "";
        this.auditRequestLogId = 0;
        this.providerResponseLogId = 0;
        this.SelectedPlanCode = "";
        this.SubscriberId = "";
        this.GroupNumber = "";
        this.DateOfService = "";
        this.Relationship = "";
        this.IsInsured = false;
        this.CoInsurance = null;
        this.Copay = null;
        this.OutOfPocketMax = null;
        this.Deductible = null;
        this.OutOfPocketRemaining = null;
        this.DeductibleRemaining = null;
        this.DeductibleAppliesToOutOfPocketMax = true;
        this.IsManualEstimate = false;
        this.Ordinality = index_1.InsuranceType.Primary;
    }
    return InsuranceInfo;
}());
exports.InsuranceInfo = InsuranceInfo;

//# sourceMappingURL=InsuranceInfo.model.js.map
