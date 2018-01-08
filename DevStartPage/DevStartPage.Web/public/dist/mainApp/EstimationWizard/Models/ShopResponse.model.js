"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var index_1 = require("./index");
var PricingResponse = /** @class */ (function () {
    function PricingResponse() {
        this.result = new index_1.PriceResult();
    }
    return PricingResponse;
}());
exports.PricingResponse = PricingResponse;
var ShopResponse = /** @class */ (function () {
    function ShopResponse() {
        this.errors = new Array();
        this.pricingResponse = new PricingResponse();
        this.referenceId = "";
        this.shopRequest = new index_1.ShopRequest();
        this.status = -1;
    }
    return ShopResponse;
}());
exports.ShopResponse = ShopResponse;

//# sourceMappingURL=ShopResponse.model.js.map
