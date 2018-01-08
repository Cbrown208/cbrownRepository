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
var index_1 = require("../Shared/Widgets/index");
var router_1 = require("@angular/router");
var StartPageComponent = /** @class */ (function () {
    function StartPageComponent(injector, spinnerSvc, router) {
        this.spinnerSvc = spinnerSvc;
        this.router = router;
        this.navLinks = {};
        this.shopRequest = {};
        this.fancySpinnerSVC = injector.get(index_1.FancySpinnerService);
    }
    StartPageComponent.prototype.ngOnInit = function () {
    };
    StartPageComponent = __decorate([
        core_1.Component({
            selector: 'startpage-page',
            templateUrl: './app/StartPage/startpage.component.html',
            styleUrls: ['../../Content/styles/Site.css']
        }),
        __metadata("design:paramtypes", [core_1.Injector,
            index_1.FancySpinnerService,
            router_1.Router])
    ], StartPageComponent);
    return StartPageComponent;
}());
exports.StartPageComponent = StartPageComponent;

//# sourceMappingURL=startpage.component.js.map
