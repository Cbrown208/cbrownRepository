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
var index_2 = require("../../Shared/Services/index");
var index_3 = require("../../Shared/Widgets/index");
var index_4 = require("../../Shared/Widgets/index");
var PatientInformationTabComponent = /** @class */ (function (_super) {
    __extends(PatientInformationTabComponent, _super);
    function PatientInformationTabComponent(injector, masterDataSvc) {
        var _this = _super.call(this, '', injector) || this;
        _this.masterDataSvc = masterDataSvc;
        _this.patientInformation = new index_1.PatientInformation();
        _this.mask = ['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/];
        _this.patientInformation = _this.shopService.shopRequest.PatientInfo;
        _this.states = _this.masterDataSvc.getStates();
        return _this;
    }
    PatientInformationTabComponent.prototype.ngOnInit = function () {
        var date = new Date();
        this.datePickerComponent.setDisableDate(date);
        this.datePickerComponent.setDefaultDate(this.patientInformation.DateOfBirth);
    };
    PatientInformationTabComponent.prototype.validatePatientInformation = function () {
        if (this.patientInformation.FirstName.length === 0) {
            this.toastSvc.error(index_1.PatientInformationValidation.FirstName);
            return false;
        }
        if (this.patientInformation.LastName.length === 0) {
            this.toastSvc.error(index_1.PatientInformationValidation.LastName);
            return false;
        }
        if (this.patientInformation.DateOfBirth.length === 0) {
            this.toastSvc.error(index_1.PatientInformationValidation.DateofBirth);
            return false;
        }
        if (this.patientInformation.Gender.length === 0) {
            this.toastSvc.error(index_1.PatientInformationValidation.Gender);
            return false;
        }
        if (this.patientInformation.Email.length === 0) {
            this.toastSvc.error(index_1.PatientInformationValidation.Email);
            return false;
        }
        return true;
    };
    PatientInformationTabComponent.prototype.openDialog = function () {
        this.messageDialogComponent.show(this.sessionManager.facilityOptions.ContactPageDisclaimer);
    };
    PatientInformationTabComponent.prototype.valideZipCode = function () {
        var input = this.patientInformation.Zip;
        if (!input)
            return;
        var regExp = /(^\d{5}$)|(^\d{5}-\d{4}$)/;
        if (!input.match(regExp)) {
            this.toastSvc.warning(index_1.PatientInformationValidation.InvalidZipCode);
            this.patientInformation.Zip = "";
        }
    };
    ;
    PatientInformationTabComponent.prototype.validateEmail = function () {
        var input = this.patientInformation.Email;
        if (!input)
            return;
        var regExp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!input.match(regExp)) {
            this.toastSvc.warning(index_1.PatientInformationValidation.InvalidEmail);
            this.patientInformation.Email = "";
        }
    };
    ;
    PatientInformationTabComponent.prototype.setSelectedDate = function (date) {
        this.patientInformation.DateOfBirth = date;
    };
    PatientInformationTabComponent.prototype.gotoNext = function () {
        if (this.validatePatientInformation()) {
            this.navSvc.links.Contact.complete();
            this.navSvc.activate(this.navSvc.links.Service);
        }
    };
    __decorate([
        core_1.ViewChild('messageDialogComponent'),
        __metadata("design:type", index_3.MessageDialogComponent)
    ], PatientInformationTabComponent.prototype, "messageDialogComponent", void 0);
    __decorate([
        core_1.ViewChild('datePicker'),
        __metadata("design:type", index_4.DatePickerComponent)
    ], PatientInformationTabComponent.prototype, "datePickerComponent", void 0);
    PatientInformationTabComponent = __decorate([
        core_1.Component({
            selector: 'patientInformation-tab',
            templateUrl: './app/EstimationWizard/Tabs/patientInformation.tab.component.html'
        }),
        __metadata("design:paramtypes", [core_1.Injector, index_2.MasterDataService])
    ], PatientInformationTabComponent);
    return PatientInformationTabComponent;
}(index_1.BaseTabComponent));
exports.PatientInformationTabComponent = PatientInformationTabComponent;

//# sourceMappingURL=patientInformation.tab.component.js.map
