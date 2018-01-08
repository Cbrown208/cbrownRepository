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
var Rx_1 = require("rxjs/Rx");
var index_1 = require("../../Models/index");
var FancySpinnerService = /** @class */ (function () {
    function FancySpinnerService() {
        this.spinnerSubject = new Rx_1.Subject();
    }
    FancySpinnerService.prototype.start = function (id) {
        this.spinnerSubject.next(new index_1.FancySpinner(true, id));
    };
    FancySpinnerService.prototype.stop = function (id) {
        this.spinnerSubject.next(new index_1.FancySpinner(false, id));
    };
    FancySpinnerService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], FancySpinnerService);
    return FancySpinnerService;
}());
exports.FancySpinnerService = FancySpinnerService;

//# sourceMappingURL=fancy.spinner.service.js.map
