"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var PricingService = /** @class */ (function () {
    function PricingService(category, cptCode, description, unitPrice, units, modifier, revenueCode, serviceType, patientType, serviceId, syskey, cdmCode) {
        this.Category = category;
        this.CptCode = cptCode;
        this.Description = description;
        this.UnitPrice = unitPrice;
        this.Units = units;
        this.Modifier = modifier;
        this.RevenueCode = revenueCode;
        this.ServiceType = serviceType;
        this.PatientType = patientType;
        this.ServiceId = serviceId;
        this.Syskey = syskey;
        this.CdmCode = cdmCode;
    }
    return PricingService;
}());
exports.PricingService = PricingService;
var IcdDiagnosis = /** @class */ (function () {
    function IcdDiagnosis(code, sequence) {
        this.DiagnosisCode = code;
        this.Sequence = sequence;
    }
    return IcdDiagnosis;
}());
exports.IcdDiagnosis = IcdDiagnosis;
var IcdProcedure = /** @class */ (function () {
    function IcdProcedure(code, sequence) {
        this.ProcedureCode = code;
        this.Sequence = sequence;
    }
    return IcdProcedure;
}());
exports.IcdProcedure = IcdProcedure;

//# sourceMappingURL=PricingService.model.js.map
