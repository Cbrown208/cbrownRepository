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
var rxjs_1 = require("rxjs");
var index_2 = require("../../Shared/Services/index");
var DownloadsService = /** @class */ (function () {
    function DownloadsService(http, toast) {
        this.http = http;
        this.toast = toast;
        this.updateMessage = new rxjs_1.Subject();
    }
    DownloadsService.prototype.GetFileDownloadList = function () {
        var apiUrl = "Downloads/GetFileList";
        return this.http.get(apiUrl);
    };
    DownloadsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [index_2.HttpWrapperService,
            index_1.Toast])
    ], DownloadsService);
    return DownloadsService;
}());
exports.DownloadsService = DownloadsService;
//# sourceMappingURL=downloads.service.js.map