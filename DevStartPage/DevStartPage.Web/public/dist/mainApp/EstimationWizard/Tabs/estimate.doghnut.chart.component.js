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
var common_1 = require("@angular/common");
var index_1 = require("./index");
var EstimateDoghnutChartComponent = /** @class */ (function (_super) {
    __extends(EstimateDoghnutChartComponent, _super);
    function EstimateDoghnutChartComponent(injector, _currencyPipe) {
        var _this = _super.call(this, "", injector) || this;
        _this._currencyPipe = _currencyPipe;
        _this.colors = [{ backgroundColor: ["#002642", "#9D2235", "#a4c73c", "#a4add3"] }];
        _this.labels = [];
        _this.data = [];
        _this.type = "doughnut";
        return _this;
    }
    EstimateDoghnutChartComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.doghnutChartSVC.updateChart.subscribe(function () {
            _this.update();
        });
        this.doghnutChartSVC.initChart.subscribe(function () {
            _this.data = [];
            _this.labels = [];
        });
    };
    EstimateDoghnutChartComponent.prototype.init = function () {
        this.options = {
            cutoutPercentage: 60,
            center: {
                text: this._currencyPipe.transform(this.doghnutChartSVC.chartData.totalEstimateCost, "USD", true, "1.2-2"),
                fillStyle: "#666666",
                font: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif"
            }
        };
    };
    EstimateDoghnutChartComponent.prototype.update = function () {
        if (this.shopService.shopResponse.pricingResponse.success) {
            if (this.shopService.shopRequest.InsuranceInfo.IsInsured) {
                this.labels = this.doghnutChartSVC.chartData.labels;
                this.data = this.doghnutChartSVC.chartData.data;
                this.init();
            }
            else {
                this.labels = this.doghnutChartSVC.chartData.labels;
                this.data = this.doghnutChartSVC.chartData.data;
                this.init();
            }
        }
    };
    EstimateDoghnutChartComponent = __decorate([
        core_1.Component({
            selector: "estimate-doghnut-chart",
            template: "<div style=\"display: block\" *ngIf=\"labels.length > 0 && data.length > 0\"> \n                    <canvas baseChart \n                    [data]=\"data\" \n                    [labels]=\"labels\" \n                    [chartType]=\"type\" \n                    [options]=\"options\" \n                    [colors]='colors' \n                    (chartHover)=\"chartHovered($event)\" \n                    (chartClick)=\"chartClicked($event)\">\n                    </canvas>\n                </div>\n    ",
            providers: [common_1.CurrencyPipe]
        }),
        __metadata("design:paramtypes", [core_1.Injector, common_1.CurrencyPipe])
    ], EstimateDoghnutChartComponent);
    return EstimateDoghnutChartComponent;
}(index_1.BaseTabComponent));
exports.EstimateDoghnutChartComponent = EstimateDoghnutChartComponent;

//# sourceMappingURL=estimate.doghnut.chart.component.js.map
