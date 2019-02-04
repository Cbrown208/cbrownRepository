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
var BasicAutoComponent = /** @class */ (function () {
    function BasicAutoComponent() {
        this.rows = [];
        this.loadingIndicator = true;
        this.reorderable = true;
        this.columns = [
            { prop: 'name' },
            { name: 'Gender' },
            { name: 'Company' }
        ];
        this.rows = [
            { name: 'Austin', gender: 'Male', company: 'Swimlane' },
            { name: 'Dany', gender: 'Male', company: 'KFC' },
            { name: 'Molly', gender: 'Female', company: 'Burger King' },
        ];
    }
    BasicAutoComponent = __decorate([
        core_1.Component({
            selector: 'basic-auto-demo',
            templateUrl: 'basic-auto-table.html'
        }),
        __metadata("design:paramtypes", [])
    ], BasicAutoComponent);
    return BasicAutoComponent;
}());
exports.BasicAutoComponent = BasicAutoComponent;
//# sourceMappingURL=datatable.basicauto.js.map