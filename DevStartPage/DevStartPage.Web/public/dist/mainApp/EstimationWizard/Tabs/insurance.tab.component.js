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
var index_2 = require("../Models/index");
var index_3 = require("../Service/index");
var index_4 = require("../../Shared/Widgets/index");
var index_5 = require("../../Shared/Widgets/index");
var _ = require("lodash");
var InsuranceTabComponent = /** @class */ (function (_super) {
    __extends(InsuranceTabComponent, _super);
    function InsuranceTabComponent(injector, insuranceSvc, eligibilitySvc) {
        var _this = _super.call(this, '', injector) || this;
        _this.insuranceSvc = insuranceSvc;
        _this.eligibilitySvc = eligibilitySvc;
        _this.shouldGetPlans = false;
        _this.formaterConstants = index_1.FormaterConstants;
        _this.formatNumberOopRemaining = function (unformatedNumber) {
            if (unformatedNumber == null || unformatedNumber == "" || unformatedNumber == ".00") {
                return null;
            }
            if (unformatedNumber.indexOf(".") == -1 && !(unformatedNumber % 1 !== 0)) {
                return unformatedNumber + ".00";
            }
            return unformatedNumber;
        };
        _this.initializeInsurance();
        _this.sessionSubscribe = _this.sessionManager.sessionUpdation.subscribe(function (data) {
            _this.insuranceInfo.IsManualEstimate = !_this.sessionManager.facilityOptions.EnableEI;
            _this.getPlans();
        });
        _this.estimateCreationSubscriber = _this.shopService.estimateCreation.subscribe(function (response) {
            _this.estimateCreated(response);
        });
        return _this;
    }
    InsuranceTabComponent.prototype.setSelectedDate = function (date) {
        this.insuranceInfo.DateOfService = date;
    };
    InsuranceTabComponent.prototype.initializeInsurance = function () {
        this.shopService.shopRequest.InsuranceInfo = new index_2.InsuranceInfo();
        this.coInsurance = null;
        this.insuranceInfo = this.shopService.shopRequest.InsuranceInfo;
        this.insuranceInfo.IsInsured = true;
        this.insuranceInfo.SelectedPlanCategory = "";
        this.insuranceInfo.SelectedPlanCode = "-1";
        this.insuranceInfo.Relationship = "-1";
        this.insuranceInfo.DeductibleAppliesToOutOfPocketMax = true;
    };
    InsuranceTabComponent.prototype.isNotAlphaNumeric = function (input) {
        var regExp = /^[a-z0-9]+$/i;
        if (input.length > 0)
            return !input.match(regExp);
        return false;
    };
    ;
    InsuranceTabComponent.prototype.openInsuranceDetailPanel = function () {
        this.insuranceInfo.IsInsured = true;
        this.insuranceInfo.SelectedPlanCode = "-1";
        this.insuranceInfo.IsManualEstimate = !this.sessionManager.facilityOptions.EnableEI;
    };
    InsuranceTabComponent.prototype.validateInsuranceName = function () {
        if (this.insuranceInfo.SelectedPlanCode === "-1") {
            this.toastSvc.error(index_1.InsuranceInformationValidation.InsuranceName);
            return false;
        }
        return true;
    };
    InsuranceTabComponent.prototype.validateInsuranceInformation = function () {
        if (!this.insuranceInfo.IsManualEstimate) {
            if (!this.validateInsuranceName()) {
                return false;
            }
            if (this.insuranceInfo.SubscriberId.length === 0) {
                this.toastSvc.error(index_1.InsuranceInformationValidation.SubscriberID);
                return false;
            }
            if (this.isNotAlphaNumeric(this.insuranceInfo.SubscriberId)) {
                this.insuranceInfo.SubscriberId = '';
                this.toastSvc.warning(index_1.InsuranceInformationValidation.ValidSubscriberID);
                return false;
            }
            if (this.insuranceInfo.GroupNumber.length === 0) {
                this.toastSvc.error(index_1.InsuranceInformationValidation.GroupNumber);
                return false;
            }
            if (this.isNotAlphaNumeric(this.insuranceInfo.GroupNumber)) {
                this.insuranceInfo.GroupNumber = '';
                this.toastSvc.warning(index_1.InsuranceInformationValidation.ValidGroupNumber);
                return false;
            }
            if (this.insuranceInfo.DateOfService.length === 0) {
                this.toastSvc.error(index_1.InsuranceInformationValidation.DateofService);
                return false;
            }
            if (this.insuranceInfo.Relationship === "-1") {
                this.toastSvc.error(index_1.InsuranceInformationValidation.RelationShip);
                return false;
            }
        }
        else if (!this.validateInsuranceName()) {
            return false;
        }
        return true;
    };
    InsuranceTabComponent.prototype.displayMessage = function () {
        this.toastSvc.error(index_1.InsuranceInformationValidation.ValidDateofService);
    };
    ;
    InsuranceTabComponent.prototype.doSelfPay = function () {
        var _this = this;
        this.insuranceSvc.getSelfPayPlan().subscribe(function (plan) {
            if (plan) {
                _this.createEstimateWithSelfPay(plan);
            }
            else {
                _this.toastSvc.error('Self Pay plan not available, please contact administrator');
            }
        });
    };
    InsuranceTabComponent.prototype.createEstimateWithSelfPay = function (selfPayPlan) {
        this.insuranceInfo.SelectedPlanCategory = "";
        this.insuranceInfo.SelectedPlanCode = selfPayPlan.PlanCode;
        this.insuranceInfo.IsInsured = false;
        this.insuranceInfo.CoInsurance = null;
        this.insuranceInfo.Copay = null;
        this.insuranceInfo.OutOfPocketMax = null;
        this.insuranceInfo.Deductible = null;
        this.insuranceInfo.OutOfPocketRemaining = null;
        this.insuranceInfo.DeductibleRemaining = null;
        this.insuranceInfo.DeductibleAppliesToOutOfPocketMax = true;
        this.insuranceInfo.IsManualEstimate = false;
        this.createEstimate();
    };
    InsuranceTabComponent.prototype.completeInsurance = function () {
        this.navSvc.links.Estimate.complete();
        this.navSvc.activate(this.navSvc.links.Estimate);
    };
    InsuranceTabComponent.prototype.openDialog = function () {
        this.messageDialogComponent.show(this.sessionManager.facilityOptions.InsurancePageDisclaimer);
    };
    InsuranceTabComponent.prototype.openManulEstimate = function () {
        this.insuranceInfo.IsManualEstimate = !this.insuranceInfo.IsManualEstimate;
    };
    InsuranceTabComponent.prototype.onPlanChange = function (plan) {
        var _this = this;
        if (this.insuranceInfo.SelectedPlanCode !== "-1") {
            var plan_1 = _.find(this.insurancePlans, function (plan) { return plan.PlanCode == _this.insuranceInfo.SelectedPlanCode; });
            this.insuranceInfo.SelectedPlanCategory = plan_1.Product;
        }
    };
    ;
    InsuranceTabComponent.prototype.getPlans = function () {
        var _this = this;
        this.insuranceSvc.getPlans().subscribe(function (plans) {
            _this.insurancePlans = plans;
        });
    };
    InsuranceTabComponent.prototype.getEstimate = function () {
        if (this.validateInsuranceInformation()) {
            if (!this.insuranceInfo.IsManualEstimate) {
                this.getEligibility(); //getEligibility and than call estimate
            }
            else {
                this.createEstimate();
            }
        }
    };
    ;
    InsuranceTabComponent.prototype.getEligibility = function () {
        var _this = this;
        this.eligibilitySvc.getServiceTypeCode(this.shopService.shopRequest.primaryCptCode, this.shopService.shopRequest.PatientType).subscribe(function (serviceTypeCodes) {
            _this.fancySpinnerSVC.start("wizard");
            _this.shopService.shopRequest.ServiceTypeCodes = serviceTypeCodes == null ? "" : serviceTypeCodes.join(",");
            _this.eligibilitySvc.getEligibility(_this.shopService.shopRequest).subscribe(function (eligibilityResponse) {
                _this.fancySpinnerSVC.stop("wizard");
                if (eligibilityResponse.isSuccess) {
                    var result = eligibilityResponse.result;
                    if (eligibilityResponse.result) {
                        _this.shopService.shopRequest.ReferenceNumber = result.referenceNumber;
                        _this.insuranceInfo.auditRequestLogId = result.auditRequestLogId;
                        _this.insuranceInfo.providerResponseLogId = result.providerResponseLogId;
                        _this.coInsurance = result.coInsurance;
                        _this.insuranceInfo.Copay = result.coPayment;
                        _this.insuranceInfo.OutOfPocketMax = result.outOfPocket;
                        _this.insuranceInfo.Deductible = result.deductible;
                        _this.insuranceInfo.OutOfPocketRemaining = result.outOfPocketBalance;
                        _this.insuranceInfo.DeductibleRemaining = result.deductibleBalance;
                        _this.insuranceInfo.DeductibleAppliesToOutOfPocketMax = result.deductibleAppliesToOutOfPocketMax;
                        _this.createEstimate();
                    }
                    return true;
                }
                else {
                    var errorMessage = eligibilityResponse.message && eligibilityResponse.message.length > 0 ? eligibilityResponse.message
                        : 'Error occured while getting eligibility ';
                    errorMessage = errorMessage + ", Please try entering insurance coverage manually.";
                    _this.toastSvc.error(errorMessage);
                    return false;
                }
            });
        });
    };
    ;
    InsuranceTabComponent.prototype.setInformationToShopRequest = function () {
        this.shopService.shopRequest.InsuranceInfo.CoInsurance = this.coInsurance * 0.01;
    };
    InsuranceTabComponent.prototype.createEstimate = function () {
        var _this = this;
        this.setInformationToShopRequest();
        this.doghnutChartSVC.intializeChart();
        this.fancySpinnerSVC.start("wizard");
        this.shopService.requestEstimate().subscribe(function (shopResponse) {
            if (shopResponse.status === 0) {
                var errorMessage = void 0;
                if (shopResponse.errors != null) {
                    errorMessage = shopResponse.errors.length > 0 ? shopResponse.errors[0] : 'Error Occured While Creating Estimate';
                    errorMessage = shopResponse.referenceId + " : " + errorMessage;
                }
                else {
                    errorMessage = 'Error Occured While Creating Estimate';
                }
                _this.toastSvc.error(errorMessage);
            }
            else {
                _this.shopService.shopResponse = shopResponse;
                _this.shopService.fireEstimateDetails(shopResponse);
            }
            _this.fancySpinnerSVC.stop("wizard");
        });
    };
    InsuranceTabComponent.prototype.estimateCreated = function (estimate) {
        this.navSvc.links.Insurance.complete();
        this.navSvc.activate(this.navSvc.links.Estimate);
    };
    InsuranceTabComponent.prototype.ngOnDestroy = function () {
        if (this.sessionSubscribe) {
            this.sessionSubscribe.unsubscribe();
        }
        if (this.estimateCreationSubscriber) {
            this.estimateCreationSubscriber.unsubscribe();
        }
    };
    __decorate([
        core_1.ViewChild('datePicker'),
        __metadata("design:type", index_4.DatePickerComponent)
    ], InsuranceTabComponent.prototype, "datePickerComponent", void 0);
    __decorate([
        core_1.ViewChild('messageDialogComponent'),
        __metadata("design:type", index_5.MessageDialogComponent)
    ], InsuranceTabComponent.prototype, "messageDialogComponent", void 0);
    InsuranceTabComponent = __decorate([
        core_1.Component({
            selector: 'insurance-tab',
            templateUrl: './app/EstimationWizard/Tabs/insurance.tab.component.html',
            providers: [index_3.InsuranceService, index_3.EligibilityService]
        }),
        __metadata("design:paramtypes", [core_1.Injector, index_3.InsuranceService, index_3.EligibilityService])
    ], InsuranceTabComponent);
    return InsuranceTabComponent;
}(index_1.BaseTabComponent));
exports.InsuranceTabComponent = InsuranceTabComponent;

//# sourceMappingURL=insurance.tab.component.js.map
