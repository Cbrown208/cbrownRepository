"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var EligibilityRequest = /** @class */ (function () {
    function EligibilityRequest(clientId, nThriveId, planCode, serviceTypeCode, serviceDate, dob, subscriberId, groupNumber, firstName, lastName, gender, relationShip, patientType) {
        this.ClientId = clientId,
            this.NThriveId = nThriveId,
            this.PlanCode = planCode,
            this.ServiceTypeCode = serviceTypeCode,
            this.ServiceFromDate = serviceDate,
            this.DOB = dob,
            this.SubscriberId = subscriberId,
            this.GroupNumber = groupNumber,
            this.FirstName = firstName,
            this.LastName = lastName,
            this.Gender = gender,
            this.RelationShip = relationShip,
            this.MedicalRecordNumber = "",
            this.PatientType = patientType,
            this.SuffixName = "";
    }
    return EligibilityRequest;
}());
exports.EligibilityRequest = EligibilityRequest;
;

//# sourceMappingURL=EligibilityRequest.model.js.map
