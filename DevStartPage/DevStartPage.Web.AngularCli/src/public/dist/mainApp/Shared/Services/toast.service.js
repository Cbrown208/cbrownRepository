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
var ng2_toasty_1 = require("ng2-toasty");
var ToastType;
(function (ToastType) {
    ToastType[ToastType["Success"] = 0] = "Success";
    ToastType[ToastType["Error"] = 1] = "Error";
    ToastType[ToastType["Warning"] = 2] = "Warning";
    ToastType[ToastType["Info"] = 3] = "Info";
    ToastType[ToastType["Wait"] = 4] = "Wait";
    ToastType[ToastType["Default"] = 5] = "Default";
})(ToastType = exports.ToastType || (exports.ToastType = {}));
var Toast = /** @class */ (function () {
    function Toast(toastService, toastyConfig) {
        this.toastService = toastService;
        this.toastyConfig = toastyConfig;
        var interval = 1000;
        var timeout = 1000 * 5;
        var subscription;
        var seconds = timeout / 1000;
        this.toastOptions = {
            title: '',
            showClose: true,
            timeout: timeout,
            theme: 'bootstrap'
        };
    }
    Toast.prototype.render = function (type, title, msg, isPersistent) {
        this.toastOptions.title = title;
        this.toastOptions.msg = msg || '';
        if (isPersistent)
            this.toastOptions.timeout = 50000;
        switch (type) {
            case ToastType.Default:
                this.toastService.default(this.toastOptions);
                break;
            case ToastType.Info:
                this.toastService.info(this.toastOptions);
                break;
            case ToastType.Success:
                this.toastService.success(this.toastOptions);
                break;
            case ToastType.Wait:
                this.toastService.wait(this.toastOptions);
                break;
            case ToastType.Error:
                this.toastService.error(this.toastOptions);
                break;
            case ToastType.Warning:
                this.toastService.warning(this.toastOptions);
                break;
        }
    };
    Toast.prototype.info = function (title, msg, isPersistent) {
        this.render(ToastType.Info, title, msg, isPersistent);
    };
    Toast.prototype.success = function (title, msg, isPersistent) {
        this.render(ToastType.Success, title, msg, isPersistent);
    };
    Toast.prototype.wait = function (title, msg, isPersistent) {
        this.render(ToastType.Wait, title, msg, isPersistent);
    };
    Toast.prototype.error = function (title, msg, isPersistent) {
        this.render(ToastType.Error, title, msg, isPersistent);
    };
    Toast.prototype.warning = function (title, msg, isPersistent) {
        this.render(ToastType.Warning, title, msg, isPersistent);
    };
    Toast = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [ng2_toasty_1.ToastyService, ng2_toasty_1.ToastyConfig])
    ], Toast);
    return Toast;
}());
exports.Toast = Toast;
//# sourceMappingURL=toast.service.js.map