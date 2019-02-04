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
var display_settings_1 = require("../../Services/display.settings");
var SpinnerComponent = /** @class */ (function () {
    function SpinnerComponent(displaySettings) {
        this.displaySettings = displaySettings;
        this.showSpinner = false;
        this.isDelayedRunning = false;
        var that = this;
        that.displaySettings.spinnerStateChanged.subscribe(function (data) {
            that.showSpinner = data;
            if (data) {
                that.spinnerModal.show();
            }
            else {
                that.spinnerModal.hide();
            }
        });
    }
    __decorate([
        core_1.ViewChild('spinnerModal'),
        __metadata("design:type", Object)
    ], SpinnerComponent.prototype, "spinnerModal", void 0);
    SpinnerComponent = __decorate([
        core_1.Component({
            selector: 'spinner-modal',
            templateUrl: 'spinner.html'
        }),
        __metadata("design:paramtypes", [display_settings_1.DisplaySettings])
    ], SpinnerComponent);
    return SpinnerComponent;
}());
exports.SpinnerComponent = SpinnerComponent;
//# sourceMappingURL=spinner.component.js.map