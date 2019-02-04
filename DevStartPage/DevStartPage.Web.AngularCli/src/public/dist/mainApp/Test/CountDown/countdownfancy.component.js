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
var CountDown = /** @class */ (function () {
    function CountDown() {
        var _this = this;
        this.displayString = '';
        this.reached = new core_1.EventEmitter();
        this.display = [];
        this.displayNumbers = [];
        this.wasReached = false;
        setInterval(function () { return _this._displayString(); }, 100);
    }
    CountDown.prototype._displayString = function () {
        if (typeof this.units === 'string') {
            this.units = this.units.split('|');
        }
        var givenDate = new Date(this.end);
        var now = new Date();
        var dateDifference = givenDate - now;
        if (dateDifference < 100 && dateDifference > 0 && !this.wasReached) {
            this.wasReached = true;
            this.reached.next(now);
        }
        var lastUnit = this.units[this.units.length - 1], unitConstantForMillisecs = {
            year: (((1000 * 60 * 60 * 24 * 7) * 4) * 12),
            month: ((1000 * 60 * 60 * 24 * 7) * 4),
            weeks: (1000 * 60 * 60 * 24 * 7),
            days: (1000 * 60 * 60 * 24),
            hours: (1000 * 60 * 60),
            minutes: (1000 * 60),
            seconds: 1000
        }, unitsLeft = {}, returnText = '', returnNumbers = '', totalMillisecsLeft = dateDifference, i, unit;
        for (i in this.units) {
            if (this.units.hasOwnProperty(i)) {
                unit = this.units[i].trim();
                if (unitConstantForMillisecs[unit.toLowerCase()] === false) {
                    //$interval.cancel(countDownInterval);
                    throw new Error('Cannot repeat unit: ' + unit);
                }
                if (unitConstantForMillisecs.hasOwnProperty(unit.toLowerCase()) === false) {
                    throw new Error('Unit: ' + unit + ' is not supported. Please use following units: year, month, weeks, days, hours, minutes, seconds, milliseconds');
                }
                unitsLeft[unit] = totalMillisecsLeft / unitConstantForMillisecs[unit.toLowerCase()];
                if (lastUnit === unit) {
                    unitsLeft[unit] = Math.ceil(unitsLeft[unit]);
                }
                else {
                    unitsLeft[unit] = Math.floor(unitsLeft[unit]);
                }
                totalMillisecsLeft -= unitsLeft[unit] * unitConstantForMillisecs[unit.toLowerCase()];
                unitConstantForMillisecs[unit.toLowerCase()] = false;
                returnNumbers += ' ' + unitsLeft[unit] + ' | ';
                returnText += ' ' + unit;
            }
        }
        if (this.text === null || !this.text) {
            this.text = {
                Year: 'Year',
                Month: 'Month',
                Weeks: 'Weeks',
                Days: 'Days',
                Hours: 'Hours',
                Minutes: 'Minutes',
                Seconds: 'Seconds',
                MilliSeconds: 'Milliseconds'
            };
        }
        this.displayString = returnText
            .replace('Year', this.text.Year + ' | ')
            .replace('Month', this.text.Month + ' | ')
            .replace('Weeks', this.text.Weeks + ' | ')
            .replace('Days', this.text.Days + ' | ')
            .replace('Hours', this.text.Hours + ' | ')
            .replace('Minutes', this.text.Minutes + ' | ')
            .replace('Seconds', this.text.Seconds);
        this.displayNumbers = returnNumbers.split('|');
        this.display = this.displayString.split('|');
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], CountDown.prototype, "units", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], CountDown.prototype, "end", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", String)
    ], CountDown.prototype, "displayString", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], CountDown.prototype, "text", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], CountDown.prototype, "divider", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", core_1.EventEmitter)
    ], CountDown.prototype, "reached", void 0);
    CountDown = __decorate([
        core_1.Component({
            // moduleId: module.id,
            selector: 'countdown',
            templateUrl: 'countdown.html',
        }),
        __metadata("design:paramtypes", [])
    ], CountDown);
    return CountDown;
}());
exports.CountDown = CountDown;
//# sourceMappingURL=countdownfancy.component.js.map