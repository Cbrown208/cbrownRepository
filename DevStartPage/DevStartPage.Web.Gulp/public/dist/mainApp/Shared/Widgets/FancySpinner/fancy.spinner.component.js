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
var fancy_spinner_service_1 = require("./fancy.spinner.service");
var FancySpinnerComponent = /** @class */ (function () {
    function FancySpinnerComponent(spinnerSvc) {
        this.spinnerSvc = spinnerSvc;
        this.id = '';
        this.Show = false;
    }
    FancySpinnerComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.SpinnerSubscription = this.spinnerSvc.spinnerSubject.subscribe(function (item) {
            if (_this.id == item.Id) {
                _this.Show = item.Show;
            }
        });
    };
    FancySpinnerComponent.prototype.ngOnDestroy = function () {
        if (this.SpinnerSubscription) {
            this.Show = false;
            this.SpinnerSubscription.unsubscribe();
        }
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], FancySpinnerComponent.prototype, "id", void 0);
    FancySpinnerComponent = __decorate([
        core_1.Component({
            selector: 'fancy-spinner',
            templateUrl: './app/Shared/Widgets/FancySpinner/fancy.spinner.html'
        }),
        __metadata("design:paramtypes", [fancy_spinner_service_1.FancySpinnerService])
    ], FancySpinnerComponent);
    return FancySpinnerComponent;
}());
exports.FancySpinnerComponent = FancySpinnerComponent;

//# sourceMappingURL=fancy.spinner.component.js.map
