"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
var index_1 = require("./index");
var index_2 = require("../Service/index");
var index_3 = require("../../Shared/Services/index");
var index_4 = require("../Models/index");
var EstimateTabComponent = /** @class */ (function (_super) {
    __extends(EstimateTabComponent, _super);
    function EstimateTabComponent(injector, sendingSvc, displaySvc) {
        var _this = _super.call(this, '', injector) || this;
        _this.sendingSvc = sendingSvc;
        _this.displaySvc = displaySvc;
        _this.shopResponse = {};
        _this.estimate = _this.shopService.estimate;
        return _this;
    }
    EstimateTabComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.shopService.estimateCreation.subscribe(function (estimate) {
            _this.estimate = estimate;
            _this.updateChart();
        });
    };
    EstimateTabComponent.prototype.price = function () {
        this.shopService.requestEstimate();
    };
    EstimateTabComponent.prototype.updateChart = function () {
        if (this.shopService.shopRequest.InsuranceInfo.IsInsured) {
            this.doghnutChartSVC.chartData = new index_4.ChartData([this.estimate.deductibleAmount, this.estimate.coInsurance, this.estimate.coPay], this.estimate.liabilityAfterInsurance, ["Deductible", "Co-Insurance", "Co-Pay"]);
        }
        else {
            this.doghnutChartSVC.chartData = new index_4.ChartData([this.estimate.discount, this.estimate.liabilityBeforeInsurance], this.estimate.liabilityBeforeInsurance, ['Discount', 'Final Cost']);
        }
        this.doghnutChartSVC.fireUpdate();
    };
    EstimateTabComponent.prototype.showDetails = function () {
        this.displaySvc.showDetails();
    };
    EstimateTabComponent.prototype.print = function () {
        this.sendingSvc.callPrintWebApi(this.estimate.referenceId);
    };
    EstimateTabComponent.prototype.email = function () {
        this.sendingSvc.callEmailWebApi(this.shopService.shopRequest.PatientInfo.Email, this.estimate.referenceId);
    };
    EstimateTabComponent = __decorate([
        core_1.Component({
            selector: 'estimate-tab',
            templateUrl: './app/EstimationWizard/Tabs/estimate.tab.component.html',
            providers: [index_2.SendingService]
        }),
        __metadata("design:paramtypes", [core_1.Injector, index_2.SendingService, index_3.DisplaySettings])
    ], EstimateTabComponent);
    return EstimateTabComponent;
}(index_1.BaseTabComponent));
exports.EstimateTabComponent = EstimateTabComponent;

//# sourceMappingURL=estimate.tab.component.js.map
