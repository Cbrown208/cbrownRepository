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
var CountdownTimer = /** @class */ (function () {
    function CountdownTimer(el) {
        this.el = el;
        this.zeroTrigger = new core_1.EventEmitter(true);
    }
    CountdownTimer.prototype.ngOnInit = function () {
        var _this = this;
        this.timer = setInterval(function () {
            if (_this.start) {
                _this.displayTime = _this.getTimeDiff(_this.start, true);
            }
            else {
                _this.displayTime = _this.getTimeDiff(_this.end);
            }
        }, 1000);
    };
    CountdownTimer.prototype.ngOnDestroy = function () {
        this.stopTimer();
    };
    CountdownTimer.prototype.getTimeDiff = function (datetime, useAsTimer) {
        if (useAsTimer === void 0) { useAsTimer = false; }
        datetime = new Date(datetime).getTime();
        var now = new Date().getTime();
        if (isNaN(datetime)) {
            return "";
        }
        var milisec_diff = datetime - now;
        if (useAsTimer) {
            milisec_diff = now - datetime;
        }
        // Zero Time Trigger
        if (milisec_diff <= 0) {
            this.zeroTrigger.emit("reached zero");
            return "00:00:00:00";
        }
        var days = Math.floor(milisec_diff / 1000 / 60 / (60 * 24));
        var date_diff = new Date(milisec_diff);
        var day_string = (days) ? this.twoDigit(days) + ":" : "";
        var day_hours = days * 24;
        if (this.timeOnly) {
            var hours = date_diff.getUTCHours() + day_hours;
            return this.twoDigit(hours) +
                ":" + this.twoDigit(date_diff.getMinutes()) + ":"
                + this.twoDigit(date_diff.getSeconds());
        }
        else {
            // Date() takes a UTC timestamp – getHours() gets hours in local time not in UTC. therefore we have to use getUTCHours()
            return day_string + this.twoDigit(date_diff.getUTCHours()) +
                ":" + this.twoDigit(date_diff.getMinutes()) + ":"
                + this.twoDigit(date_diff.getSeconds());
        }
    };
    CountdownTimer.prototype.twoDigit = function (number) {
        return number > 9 ? "" + number : "0" + number;
    };
    CountdownTimer.prototype.stopTimer = function () {
        clearInterval(this.timer);
        this.timer = undefined;
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], CountdownTimer.prototype, "start", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], CountdownTimer.prototype, "end", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], CountdownTimer.prototype, "zeroTrigger", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], CountdownTimer.prototype, "timeOnly", void 0);
    CountdownTimer = __decorate([
        core_1.Component({
            selector: 'countdown-timer',
            template: "{{ displayTime }}"
        }),
        __metadata("design:paramtypes", [core_1.ElementRef])
    ], CountdownTimer);
    return CountdownTimer;
}());
exports.CountdownTimer = CountdownTimer;

//# sourceMappingURL=countdown.component.js.map
