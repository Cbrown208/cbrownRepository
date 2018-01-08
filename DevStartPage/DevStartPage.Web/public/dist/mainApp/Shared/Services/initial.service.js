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
var http_wrapper_service_1 = require("./http.wrapper.service");
var InitialService = /** @class */ (function () {
    function InitialService(http) {
        this.http = http;
    }
    InitialService.prototype.startSession = function () {
        return this.http.get("Initial/GetInfo");
    };
    InitialService.prototype.updateSession = function (nthriveId) {
        var apiUrl = "Home/UpdateSession?nthriveId=" + nthriveId;
        return this.http.get(apiUrl);
    };
    InitialService.prototype.getSiteKey = function () {
        return this.http.get("Home/GetSiteKey");
    };
    InitialService.prototype.callApi = function () {
        var apiUrl = "Initial/CallWebApi";
        return this.http.get(apiUrl).toPromise();
    };
    InitialService.prototype.getFacilityOptions = function () {
        var apiUrl = "Initial/GetFacilityOptions";
        return this.http.get(apiUrl);
    };
    InitialService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_wrapper_service_1.HttpWrapperService])
    ], InitialService);
    return InitialService;
}());
exports.InitialService = InitialService;

//# sourceMappingURL=initial.service.js.map
