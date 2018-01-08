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
var ngx_progressbar_1 = require("ngx-progressbar");
var ProgressbarComponent = /** @class */ (function () {
    function ProgressbarComponent(pService) {
        this.pService = pService;
    }
    ProgressbarComponent.prototype.ngOnInit = function () {
        this.pService.start();
        //this.http.get(url).subscribe(res)
        //{
        //    this.pService.done();
        //}
    };
    ProgressbarComponent = __decorate([
        core_1.Component({
            selector: 'progress',
            template: '<h3>Sample Progress Bar</h3><ng-progress [positionUsing]="marginLeft" [minimum]="0.15" [maximum]="1" [speed]="200" [showSpinner]="false" [direction]="rightToLeftIncrease"' +
                ' [color]="red" [trickleSpeed]="250" [thick]="false" [ease]="linear"></ng-progress>'
            /**  */
        }),
        __metadata("design:paramtypes", [ngx_progressbar_1.NgProgressService])
    ], ProgressbarComponent);
    return ProgressbarComponent;
}());
exports.ProgressbarComponent = ProgressbarComponent;

//# sourceMappingURL=Progressbar.Component.js.map
