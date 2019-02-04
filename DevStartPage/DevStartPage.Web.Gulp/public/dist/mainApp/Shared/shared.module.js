"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var index_1 = require("./Widgets/index");
var ng2_toasty_1 = require("ng2-toasty");
var index_2 = require("./Services/index");
var ngx_bootstrap_1 = require("ngx-bootstrap");
var SharedModule = /** @class */ (function () {
    function SharedModule() {
    }
    SharedModule = __decorate([
        core_1.NgModule({
            imports: [common_1.CommonModule, ng2_toasty_1.ToastyModule.forRoot(), ngx_bootstrap_1.ModalModule.forRoot(),],
            declarations: [index_1.NavbarComponent, index_1.FooterComponent, index_1.HeaderComponent, index_1.SpinnerComponent, index_1.MessageDialogComponent],
            exports: [index_1.NavbarComponent, index_1.FooterComponent, index_1.HeaderComponent, index_1.SpinnerComponent, index_1.MessageDialogComponent],
            providers: [ng2_toasty_1.ToastyService, index_2.DisplaySettings, index_2.Toast, index_2.HttpWrapperService, index_2.SessionManager]
        })
    ], SharedModule);
    return SharedModule;
}());
exports.SharedModule = SharedModule;

//# sourceMappingURL=shared.module.js.map
