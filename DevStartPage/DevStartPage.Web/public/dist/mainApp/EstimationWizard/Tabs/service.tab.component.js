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
var index_2 = require("../Service/index");
var index_3 = require("../Models/index");
var _ = require("lodash");
var index_4 = require("../../Shared/Widgets/index");
var session_manager_1 = require("../../Shared/Services/session.manager");
var ServiceTabComponent = /** @class */ (function (_super) {
    __extends(ServiceTabComponent, _super);
    function ServiceTabComponent(injector, servicesSvc, sessionManager) {
        var _this = _super.call(this, '', injector) || this;
        _this.servicesSvc = servicesSvc;
        _this.sessionManager = sessionManager;
        _this.shopRequestPricingServices = [];
        _this.shopRequestIcdDiagnoses = [];
        _this.shopRequestIcdProcedures = [];
        _this.getPricingServices = function (selectedService) {
            var pricingProcedures = new Array();
            pricingProcedures = selectedService.PriceProcedures.map(function (proc) {
                var pricingService = new index_3.PricingService(selectedService.SelectedCategoryId, proc.ProcedureCode, proc.Description, proc.PerUnitCharge, proc.Units, proc.Modifiers, proc.RevenueCode, selectedService.ServiceType, selectedService.PatientType, '', null, proc.CdmCode);
                return pricingService;
            });
            return pricingProcedures;
        };
        _this.getDiagnoses = function (selectedService) {
            var pricingDiagnoses = new Array();
            if (selectedService.IcdDiagnoses != null) {
                pricingDiagnoses = selectedService.IcdDiagnoses.map(function (icdDiagnosis) {
                    return new index_3.IcdDiagnosis(icdDiagnosis.DiagnosisCode, icdDiagnosis.Sequence);
                });
            }
            return pricingDiagnoses;
        };
        _this.getIcdProcedures = function (selectedService) {
            var pricingICdProcedures = new Array();
            if (selectedService.IcdProcedures != null) {
                pricingICdProcedures = selectedService.IcdProcedures.map(function (icdproc) {
                    return new index_3.IcdProcedure(icdproc.ProcedureCode, icdproc.Sequence);
                });
            }
            return pricingICdProcedures;
        };
        _this.shopRequestPricingServices = _this.shopService.shopRequest.SelectedService;
        _this.sessionSubscribe = _this.sessionManager.sessionUpdation.subscribe(function (data) {
            _this.servicesSvc.getFacilityConfigurationDetailsbynThriveId().then(function (data) {
                _this.facilityConfiguration = data;
                _this.selectedCategory = "-1";
                if (!_this.facilityConfiguration) {
                    _this.toastSvc.error('Facility not setup yet.');
                }
            });
        });
        return _this;
    }
    ServiceTabComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.servicesSvc.getFacilityConfigurationDetails().then(function (data) {
            _this.facilityConfiguration = data;
            _this.selectedCategory = "-1";
            if (!_this.facilityConfiguration) {
                _this.toastSvc.error('Facility not setup yet.');
            }
        });
    };
    ServiceTabComponent.prototype.changeCategory = function (categoryId) {
        this.searchParams = "";
        this.shopRequestPricingServices = [];
        this.shopRequestIcdDiagnoses = [];
        this.shopRequestIcdProcedures = [];
        if (this.facilityConfiguration.CategoryServiceMappingDetails !== null) {
            this.selectedFacilityServices = _.uniqBy(this.facilityConfiguration.CategoryServiceMappingDetails
                .filter(function (x) { return x.SelectedCategoryId == categoryId; }), 'Syskey');
        }
    };
    ServiceTabComponent.prototype.searchService = function () {
        var searchParam = this.searchParams ? this.searchParams.toUpperCase() : "";
        this.selectedFacilityServices = _.uniqBy(this.facilityConfiguration.CategoryServiceMappingDetails
            .filter(function (x) { return x.DisplayName.toUpperCase().indexOf(searchParam) > -1; }), 'Syskey');
        this.selectedCategory = "-1";
    };
    ;
    ServiceTabComponent.prototype.selectService = function (selectedRadioRow, selectedService, elementId) {
        var element = document.getElementById(elementId);
        var trs = element.parentElement.parentElement.getElementsByTagName("tr");
        var count = trs.length;
        for (var i = 0; i < count; i++) {
            trs[i].className = "cursor-crosshair";
        }
        element.parentElement.className = "bg-success trshadow";
        this.populateShoprequestService(selectedService);
    };
    ;
    ServiceTabComponent.prototype.populateShoprequestService = function (selectedService) {
        console.log(selectedService);
        this.shopService.selectedServiceCategory = _.find(this.facilityConfiguration.FacilityCategoryDetails, function (o) { return o.Id == selectedService.SelectedCategoryId; });
        this.primaryCptCode = selectedService.ServiceCode;
        this.selectedServiceName = selectedService.DisplayName;
        this.selectedServiceSysKey = selectedService.SysKey;
        this.selectedServiceId = selectedService.ServiceId;
        this.patientType = selectedService.PatientType;
        this.serviceType = selectedService.ServiceType;
        this.selectedServiceCategoryName =
            (this.shopService.selectedServiceCategory == null)
                ? "-" : this.shopService.selectedServiceCategory.Name;
        this.shopRequestPricingServices.splice(0);
        this.shopRequestIcdDiagnoses.splice(0);
        this.shopRequestIcdProcedures.splice(0);
        this.shopRequestPricingServices = this.getPricingServices(selectedService);
        this.shopRequestIcdDiagnoses = this.getDiagnoses(selectedService);
        this.shopRequestIcdProcedures = this.getIcdProcedures(selectedService);
    };
    ServiceTabComponent.prototype.completeService = function () {
        if (this.validateService().length === 0) {
            this.shopService.shopRequest.SelectedService = this.shopRequestPricingServices;
            this.shopService.shopRequest.IcdDiagnoses = this.shopRequestIcdDiagnoses;
            this.shopService.shopRequest.IcdProcedures = this.shopRequestIcdProcedures;
            this.shopService.shopRequest.PatientType = this.patientType;
            this.shopService.shopRequest.ServiceType = this.serviceType;
            this.shopService.shopRequest.primaryCptCode = this.primaryCptCode;
            this.shopService.shopRequest.SelectedServiceName = this.selectedServiceName;
            this.shopService.shopRequest.SelectedServiceSyskey = this.selectedServiceSysKey;
            this.shopService.shopRequest.SelectedServiceId = this.selectedServiceId;
            this.shopService.shopRequest.SelectedServiceCategory = this.selectedServiceCategoryName;
            this.navSvc.links.Service.complete();
            this.navSvc.activate(this.navSvc.links.Insurance);
        }
    };
    ;
    ServiceTabComponent.prototype.validateService = function () {
        var errors = [];
        if (this.shopRequestPricingServices.length === 0) {
            errors.push("Please select a service.");
            this.toastSvc.error('Please select a service.');
        }
        return errors;
    };
    ServiceTabComponent.prototype.openDialog = function () {
        this.messageDialogComponent.show(this.sessionManager.facilityOptions.ServicePageDisclaimer);
    };
    __decorate([
        core_1.ViewChild('messageDialogComponent'),
        __metadata("design:type", index_4.MessageDialogComponent)
    ], ServiceTabComponent.prototype, "messageDialogComponent", void 0);
    ServiceTabComponent = __decorate([
        core_1.Component({
            selector: 'service-tab',
            templateUrl: './app/EstimationWizard/Tabs/service.tab.component.html',
            providers: [index_2.ServicesService]
        }),
        __metadata("design:paramtypes", [core_1.Injector, index_2.ServicesService, session_manager_1.SessionManager])
    ], ServiceTabComponent);
    return ServiceTabComponent;
}(index_1.BaseTabComponent));
exports.ServiceTabComponent = ServiceTabComponent;

//# sourceMappingURL=service.tab.component.js.map
