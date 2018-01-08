"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var PriceResult = /** @class */ (function () {
    function PriceResult() {
        this.contractUrl == "";
        this.copayAmount = 0;
        this.deductibleAmount = 0;
        this.discountAmount = 0;
        this.drg = 0;
        this.exclusionAmount = 0;
        this.expectedPayment = 0;
        this.grossCharges = 0;
        this.messageCode = "";
        this.messageComment = "";
        this.paidAmount = 0;
        this.patientLiabilityAmount = 0;
        this.serviceType = "";
        this.stopCapitalizationAmount = "";
        this.stopLossAmount = 0;
        this.wasStopCapitalizationHit = false;
        this.wasStopLossHit = false;
    }
    return PriceResult;
}());
exports.PriceResult = PriceResult;

//# sourceMappingURL=PriceResult.model.js.map
