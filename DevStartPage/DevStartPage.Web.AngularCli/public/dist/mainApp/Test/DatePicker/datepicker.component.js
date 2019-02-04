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
// other imports here...
var DatePickerComponent = /** @class */ (function () {
    function DatePickerComponent() {
        this.myOptions = {
            // other options...
            dateFormat: 'dd.mm.yyyy'
        };
        // Initialized to specific date (09.10.2018)
        this.model = { date: { year: 2018, month: 10, day: 9 } };
    }
    // optional date changed callback
    DatePickerComponent.prototype.onDateChanged = function (event) {
        // date selected
    };
    DatePickerComponent = __decorate([
        core_1.Component({
            selector: 'DatePicker',
            templateUrl: 'app/Test/DatePicker/datepicker.component.html'
        }),
        __metadata("design:paramtypes", [])
    ], DatePickerComponent);
    return DatePickerComponent;
}());
exports.DatePickerComponent = DatePickerComponent;

//# sourceMappingURL=datepicker.component.js.map
