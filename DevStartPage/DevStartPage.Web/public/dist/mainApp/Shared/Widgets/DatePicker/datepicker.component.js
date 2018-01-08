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
        this.setDate = new core_1.EventEmitter();
        this.dateValidation = new core_1.EventEmitter();
        this.myOptions = {
            // other options...
            dateFormat: 'mm/dd/yyyy',
        };
    }
    DatePickerComponent.prototype.setDisableDate = function (date) {
        var disableSince = this.FormateDate(date);
        this.myOptions.disableSince = disableSince.date;
    };
    DatePickerComponent.prototype.setDefaultDate = function (date) {
        var defaultDate = new Date(date);
        if (date) {
            this.model = this.FormateDate(defaultDate);
        }
    };
    DatePickerComponent.prototype.FormateDate = function (date) {
        return {
            date: {
                year: date.getFullYear(),
                month: date.getMonth() + 1,
                day: date.getDate()
            }
        };
    };
    // optional date changed callback
    DatePickerComponent.prototype.onDateChanged = function (event) {
        // date selected
        this.setDate.emit(event.formatted);
    };
    DatePickerComponent.prototype.validateDate = function (testdate) {
        if (testdate.length === 0)
            return true;
        var dateRegex = /^\d{2}\/\d{2}\/\d{4}$/;
        return dateRegex.test(testdate);
    };
    ;
    DatePickerComponent.prototype.onblur = function (event) {
        if (!this.validateDate(event.srcElement.value)) {
            this.dp.clearDate();
            this.dateValidation.emit();
        }
    };
    ;
    __decorate([
        core_1.Output(),
        __metadata("design:type", core_1.EventEmitter)
    ], DatePickerComponent.prototype, "setDate", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", core_1.EventEmitter)
    ], DatePickerComponent.prototype, "dateValidation", void 0);
    __decorate([
        core_1.ViewChild('dp'),
        __metadata("design:type", Object)
    ], DatePickerComponent.prototype, "dp", void 0);
    DatePickerComponent = __decorate([
        core_1.Component({
            selector: 'DatePicker',
            templateUrl: 'app/Shared/Widgets/DatePicker/datepicker.component.html'
        }),
        __metadata("design:paramtypes", [])
    ], DatePickerComponent);
    return DatePickerComponent;
}());
exports.DatePickerComponent = DatePickerComponent;

//# sourceMappingURL=datepicker.component.js.map
