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
var index_1 = require("../Models/index");
var index_2 = require("../index");
var index_3 = require("../../Shared/Services/index");
var index_4 = require("../Service/index");
var index_5 = require("../../Shared/Widgets/FancySpinner/index");
var BaseTabComponent = /** @class */ (function () {
    function BaseTabComponent(name, injector) {
        this.Name = '';
        this.State = index_1.TabState.Pending;
        this.Hidden = true;
        this.onComplete = new core_1.EventEmitter();
        this.Name = name;
        this.toastSvc = injector.get(index_3.Toast);
        this.navSvc = injector.get(index_2.WizardNavigationService);
        this.sessionManager = injector.get(index_3.SessionManager);
        this.shopService = injector.get(index_4.ShopService);
        this.fancySpinnerSVC = injector.get(index_5.FancySpinnerService);
        this.doghnutChartSVC = injector.get(index_4.DoghnutChartService);
    }
    BaseTabComponent.prototype.show = function () {
        this.Hidden = false;
    };
    BaseTabComponent.prototype.activate = function () {
        this.show();
    };
    BaseTabComponent.prototype.hide = function () {
        this.Hidden = true;
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Boolean)
    ], BaseTabComponent.prototype, "Hidden", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", core_1.EventEmitter)
    ], BaseTabComponent.prototype, "onComplete", void 0);
    BaseTabComponent = __decorate([
        core_1.Component({
            selector: 'base-tab',
            templateUrl: ''
        }),
        __metadata("design:paramtypes", [String, core_1.Injector])
    ], BaseTabComponent);
    return BaseTabComponent;
}());
exports.BaseTabComponent = BaseTabComponent;

//# sourceMappingURL=base.tab.component.js.map
