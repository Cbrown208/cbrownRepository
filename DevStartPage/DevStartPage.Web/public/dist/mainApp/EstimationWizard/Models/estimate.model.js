"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Estimate = /** @class */ (function () {
    function Estimate(referenceId, totalCost, discount, liabilityBeforeInsurance, liabilityAfterInsurance, coPay, coInsurance, deductibleAmount) {
        this.referenceId = referenceId || '';
        this.totalCost = totalCost || 0;
        this.discount = discount || 0;
        this.liabilityBeforeInsurance = liabilityBeforeInsurance || 0;
        this.coPay = coPay || 0;
        this.coInsurance = coInsurance || 0;
        this.deductibleAmount = deductibleAmount || 0;
        this.liabilityAfterInsurance = liabilityAfterInsurance || 0;
    }
    return Estimate;
}());
exports.Estimate = Estimate;

//# sourceMappingURL=estimate.model.js.map
