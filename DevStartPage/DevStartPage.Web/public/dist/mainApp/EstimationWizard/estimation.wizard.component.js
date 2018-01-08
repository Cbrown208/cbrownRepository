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
var index_1 = require("./index");
var index_2 = require("../Shared/Widgets/index");
var index_3 = require("../Shared/Services/index");
var router_1 = require("@angular/router");
var index_4 = require("../Shared/Services/index");
var EstimationWizardComponent = /** @class */ (function () {
    function EstimationWizardComponent(injector, navService, spinnerSvc, initialService, router, sessionManager) {
        this.navService = navService;
        this.spinnerSvc = spinnerSvc;
        this.initialService = initialService;
        this.router = router;
        this.sessionManager = sessionManager;
        this.navLinks = {};
        this.shopRequest = {};
        this.navLinks = this.navService.links;
        this.fancySpinnerSVC = injector.get(index_2.FancySpinnerService);
    }
    EstimationWizardComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.navService.activate(this.navLinks.Home);
        this.initialService.startSession()
            .subscribe(function (response) {
            _this.sessionManager.triggerSessionUpdate();
        });
        this.fancySpinnerSVC.start("wizard");
        this.initialService.getFacilityOptions().subscribe(function (response) {
            _this.sessionManager.setFacilityOptions(response);
        });
    };
    EstimationWizardComponent = __decorate([
        core_1.Component({
            selector: 'estimation-wizard',
            templateUrl: './app/EstimationWizard/estimation.wizard.component.html'
        }),
        __metadata("design:paramtypes", [core_1.Injector,
            index_1.WizardNavigationService,
            index_2.FancySpinnerService,
            index_3.InitialService,
            router_1.Router,
            index_4.SessionManager])
    ], EstimationWizardComponent);
    return EstimationWizardComponent;
}());
exports.EstimationWizardComponent = EstimationWizardComponent;

//# sourceMappingURL=estimation.wizard.component.js.map
