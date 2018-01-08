"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var index_1 = require("../../Shared/Services/index");
var index_2 = require("../Models/index");
var Subject_1 = require("rxjs/Subject");
var ShopService = /** @class */ (function () {
    function ShopService(httpWrapperSvc) {
        this.httpWrapperSvc = httpWrapperSvc;
        this.shopRequest = new index_2.ShopRequest();
        this.shopResponse = new index_2.ShopResponse();
        this.estimateCreation = new Subject_1.Subject();
        this.estimate = new index_2.Estimate();
    }
    ShopService.prototype.requestEstimate = function () {
        return this.httpWrapperSvc.post('estimate/Price', this.shopRequest);
    };
    ShopService.prototype.fireEstimateDetails = function (shopResponse) {
        var result = shopResponse.pricingResponse.result;
        var liabilityResult;
        if (shopResponse.liabilityResponse) {
            liabilityResult = shopResponse.liabilityResponse.result;
        }
        if (this.shopRequest.InsuranceInfo.IsInsured) {
            this.estimate = new index_2.Estimate(shopResponse.referenceId, result.grossCharges, result.discountAmount, result.expectedPayment, liabilityResult.liabilityAfterInsurance, liabilityResult.coPayApplied, liabilityResult.coInsuranceApplied, liabilityResult.deductibleApplied);
        }
        else {
            this.estimate = new index_2.Estimate(shopResponse.referenceId, result.grossCharges, result.discountAmount, result.expectedPayment);
        }
        this.estimateCreation.next(this.estimate);
    };
    ShopService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [index_1.HttpWrapperService])
    ], ShopService);
    return ShopService;
}());
exports.ShopService = ShopService;

//# sourceMappingURL=shop.service.js.map
