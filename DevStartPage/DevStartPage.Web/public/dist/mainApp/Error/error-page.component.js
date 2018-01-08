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
var ErrorPageComponent = /** @class */ (function () {
    function ErrorPageComponent() {
        this.errorCode = '';
        this.errorMessage = '';
    }
    ErrorPageComponent.prototype.ngOnInit = function () { };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], ErrorPageComponent.prototype, "errorCode", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], ErrorPageComponent.prototype, "errorMessage", void 0);
    ErrorPageComponent = __decorate([
        core_1.Component({
            selector: 'error-page',
            template: "\n    <div class=\"row\">\n    <div class=\"col-sm-10 col-sm-offset-1 error-page\">\n            <h2>{{errorCode}}: {{errorMessage}}</h2>\n            <p>           \n                <ng-content></ng-content>\n            </p>\n            </div>\n        </div>    \n    ",
            styles: [
                "\n        .error-page {\n           margin: 0px 15px;\n        }\n        .error-page h2 {\n            color: #636363;\n        }\n        "
            ]
        }),
        __metadata("design:paramtypes", [])
    ], ErrorPageComponent);
    return ErrorPageComponent;
}());
exports.ErrorPageComponent = ErrorPageComponent;

//# sourceMappingURL=error-page.component.js.map
