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
var wizard_navigation_service_1 = require("./wizard.navigation.service");
var WizardNavigationComponent = /** @class */ (function () {
    function WizardNavigationComponent(navService) {
        this.navService = navService;
        this.linkCollection = [];
        this.linkCollection = this.navService.linkCollection;
    }
    WizardNavigationComponent.prototype.activate = function (link) {
        this.navService.activate(link);
    };
    WizardNavigationComponent = __decorate([
        core_1.Component({
            selector: 'wizard-nav',
            templateUrl: './app/EstimationWizard/Navigation/wizard.navigation.component.html'
        }),
        __metadata("design:paramtypes", [wizard_navigation_service_1.WizardNavigationService])
    ], WizardNavigationComponent);
    return WizardNavigationComponent;
}());
exports.WizardNavigationComponent = WizardNavigationComponent;

//# sourceMappingURL=wizard.navigation.component.js.map
