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
var index_1 = require("../../Shared/Services/index");
var SendingService = /** @class */ (function () {
    function SendingService(httpWrapperSvc, sessionSvc, toastSvc) {
        this.httpWrapperSvc = httpWrapperSvc;
        this.sessionSvc = sessionSvc;
        this.toastSvc = toastSvc;
        this.baseUrl = 'Sending/';
    }
    SendingService.prototype.callPrintWebApi = function (referenceId) {
        if (referenceId !== null && referenceId !== undefined) {
            var apiUrl = this.baseUrl + 'CallPrintWebApi?referenceId=' + referenceId;
            var results = this.httpWrapperSvc.get(apiUrl);
            window.open(apiUrl, "_blank");
            return results;
        }
        else {
            this.toastSvc.error("You must complete the estimate before you can print.");
            return "";
        }
    };
    SendingService.prototype.callEmailWebApi = function (email, referenceId) {
        var _this = this;
        if (referenceId !== null && referenceId !== undefined && email !== null && email !== undefined) {
            var apiUrl = this.baseUrl + 'CallEmailWebApi?referenceId=' + referenceId + "&email=" + email;
            try {
                this.httpWrapperSvc.get(apiUrl).subscribe(function (data) {
                    _this.toastSvc.success("Email successfully sent to " + email + ".");
                });
                return "Sent";
            }
            catch (e) {
                this.toastSvc.error("Error occurred while sending email.");
            }
        }
        this.toastSvc.info("Email must be provided to send email.");
        return "";
    };
    SendingService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [index_1.HttpWrapperService, index_1.SessionManager, index_1.Toast])
    ], SendingService);
    return SendingService;
}());
exports.SendingService = SendingService;

//# sourceMappingURL=sending.service.js.map
