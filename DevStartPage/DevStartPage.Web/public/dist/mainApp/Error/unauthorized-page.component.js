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
var UnauthorizedPageComponent = /** @class */ (function () {
    function UnauthorizedPageComponent() {
    }
    UnauthorizedPageComponent.prototype.ngOnInit = function () { };
    UnauthorizedPageComponent = __decorate([
        core_1.Component({
            selector: 'unauthorized-page',
            template: "\n        <error-page [errorCode]='401' [errorMessage]=\"'Session Expired'\">\n        Your session has expired, please <a href=\"/home/refresh\">click here</a> to go back to the home page.\n        </error-page>\n    "
        }),
        __metadata("design:paramtypes", [])
    ], UnauthorizedPageComponent);
    return UnauthorizedPageComponent;
}());
exports.UnauthorizedPageComponent = UnauthorizedPageComponent;

//# sourceMappingURL=unauthorized-page.component.js.map
