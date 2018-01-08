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
var angular2_recaptcha_1 = require("angular2-recaptcha");
var index_2 = require("../Service/index");
var index_3 = require("../../Shared/Services/index");
var HomeTabComponent = /** @class */ (function (_super) {
    __extends(HomeTabComponent, _super);
    function HomeTabComponent(injector, servicesSvc, initialService) {
        var _this = _super.call(this, '', injector) || this;
        _this.servicesSvc = servicesSvc;
        _this.initialService = initialService;
        _this.isFacilitySelectable = false;
        _this.shouldshowDisclaimer = false;
        return _this;
    }
    HomeTabComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isAgreeDisable = true;
        this.initialService.getSiteKey().subscribe(function (response) {
            _this.sessionManager.setSiteKey(response);
            if (!_this.sessionManager.session.isRecaptchaSvc) {
                _this.isAgreeDisable = false;
            }
        }, function () {
            _this.isAgreeDisable = true;
            _this.fancySpinnerSVC.stop("wizard");
        });
        this.fancySpinnerSVC.start("wizard");
        this.servicesSvc.getFacilityConfigurationDetails().then(function (data) {
            _this.facilityConfiguration = data;
            if (!_this.facilityConfiguration) {
                _this.fancySpinnerSVC.stop("wizard");
                _this.toastSvc.error('Facility not setup yet.');
            }
            if (_this.facilityConfiguration.NThriveId === 0) {
                _this.isFacilitySelectable = true;
                _this.shouldshowDisclaimer = false;
                _this.servicesSvc.getPfeEnabledFacilityListDetails().then(function (data) {
                    _this.fancySpinnerSVC.stop("wizard");
                    _this.enabledFacilities = data;
                    _this.selectedFacilityId = "-1";
                    if (!_this.facilityConfiguration) {
                        _this.toastSvc.error('Client not setup yet.');
                    }
                });
            }
            else {
                _this.fancySpinnerSVC.stop("wizard");
            }
        });
    };
    HomeTabComponent.prototype.changeFacilityId = function (nThriveId) {
        var _this = this;
        if (nThriveId != -1) {
            this.initialService.updateSession(nThriveId).subscribe(function (response) {
                _this.getFacilityConfiguration();
                _this.getFacilityOptions();
                _this.sessionManager.triggerSessionUpdate();
            });
        }
    };
    HomeTabComponent.prototype.getFacilityConfiguration = function () {
        var _this = this;
        this.servicesSvc.getFacilityConfigurationDetailsbynThriveId().then(function (data) {
            _this.facilityConfiguration = data;
            if (!_this.facilityConfiguration) {
                _this.toastSvc.error('Facility is not setup yet.');
                _this.shouldshowDisclaimer = false;
                return;
            }
            _this.shouldshowDisclaimer = true;
        }).catch(function () { return _this.hideDisclaimer(); });
    };
    HomeTabComponent.prototype.hideDisclaimer = function () {
        this.shouldshowDisclaimer = false;
        this.toastSvc.error('Facility is not setup yet.');
    };
    HomeTabComponent.prototype.complete = function () {
        var isDummyToken = false;
        if (this.captcha && this.sessionManager.session.isRecaptchaSvc) {
            var token = this.captcha.getResponse();
            if (!token) {
                this.isAgreeDisable = true;
                this.toastSvc.error('Please confirm you are not a Robot.', '', true);
                return;
            }
        }
        else {
            isDummyToken = true;
        }
        if (!this.facilityConfiguration) {
            this.toastSvc.error("No Facilities are setup. If you think this is an error please contact the hospital administrator.");
            return;
        }
        if (this.selectedFacilityId === "-1" && this.isFacilitySelectable) {
            this.toastSvc.error("Please Select a Facility.");
            return;
        }
        this.navSvc.links.Home.complete();
        this.navSvc.activate(this.navSvc.links.Contact);
        if (!isDummyToken)
            this.captcha.reset();
    };
    HomeTabComponent.prototype.handleCorrectCaptcha = function (event) {
        this.isAgreeDisable = false;
    };
    HomeTabComponent.prototype.getFacilityOptions = function () {
        var _this = this;
        this.initialService.getFacilityOptions().subscribe(function (options) {
            _this.sessionManager.setFacilityOptions(options);
        });
    };
    __decorate([
        core_1.ViewChild(angular2_recaptcha_1.ReCaptchaComponent),
        __metadata("design:type", angular2_recaptcha_1.ReCaptchaComponent)
    ], HomeTabComponent.prototype, "captcha", void 0);
    HomeTabComponent = __decorate([
        core_1.Component({
            selector: 'home-tab',
            templateUrl: './app/EstimationWizard/Tabs/home.tab.component.html',
            providers: [index_2.ServicesService, index_3.InitialService]
        }),
        __metadata("design:paramtypes", [core_1.Injector, index_2.ServicesService, index_3.InitialService])
    ], HomeTabComponent);
    return HomeTabComponent;
}(index_1.BaseTabComponent));
exports.HomeTabComponent = HomeTabComponent;

//# sourceMappingURL=home.tab.component.js.map
