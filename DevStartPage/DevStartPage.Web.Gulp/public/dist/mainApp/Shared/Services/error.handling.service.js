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
var http_1 = require("@angular/http");
var Rx_1 = require("rxjs/Rx");
var toast_service_1 = require("./toast.service");
var display_settings_1 = require("./display.settings");
var index_1 = require("../UserMessages/index");
var index_2 = require("../Enums/index");
var router_1 = require("@angular/router");
var index_3 = require("../Widgets/index");
var ErrorHandlingService = /** @class */ (function () {
    function ErrorHandlingService(http, toast, displaySettings, router, fancySpinnerSvc) {
        this.http = http;
        this.toast = toast;
        this.displaySettings = displaySettings;
        this.router = router;
        this.fancySpinnerSvc = fancySpinnerSvc;
    }
    ErrorHandlingService.prototype.handleError = function (error) {
        //Stop the spinner immediately
        this.fancySpinnerSvc.stop('wizard');
        if (!error) {
            this.toast.error(index_1.HttpErrorMessages.Unknown.Title, index_1.HttpErrorMessages.Unknown.Message);
            return Rx_1.Observable.throw(error);
        }
        if (error.status === index_2.HttpStatusCode.Unauthorized) {
            //Dont throw Error, Redirect to Unauthorized Page
            this.router.navigateByUrl('/unauthorized');
            return Rx_1.Observable.throw(error);
        }
        if (error.status === index_2.HttpStatusCode.InternalServerError) {
            var errorBody = error.json() || {};
            var errorMessage = errorBody.Message || index_1.HttpErrorMessages.InternalServerError.Message;
            this.toast.error(errorMessage);
            return Rx_1.Observable.throw(error);
        }
        if (error instanceof http_1.Response) {
            var message = error.json().error || 'Server Error';
            return Rx_1.Observable.throw(message);
        }
        return Rx_1.Observable.throw(error);
    };
    ErrorHandlingService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http,
            toast_service_1.Toast,
            display_settings_1.DisplaySettings,
            router_1.Router,
            index_3.FancySpinnerService])
    ], ErrorHandlingService);
    return ErrorHandlingService;
}());
exports.ErrorHandlingService = ErrorHandlingService;

//# sourceMappingURL=error.handling.service.js.map
