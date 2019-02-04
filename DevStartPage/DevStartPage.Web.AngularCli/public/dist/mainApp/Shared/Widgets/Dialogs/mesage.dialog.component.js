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
var ngx_bootstrap_1 = require("ngx-bootstrap");
var MessageDialogComponent = /** @class */ (function () {
    function MessageDialogComponent() {
        this.message = '';
    }
    MessageDialogComponent.prototype.show = function (message) {
        this.message = message;
        this.modal.show();
    };
    MessageDialogComponent.prototype.close = function () {
        this.modal.hide();
    };
    __decorate([
        core_1.ViewChild('popUpModal'),
        __metadata("design:type", ngx_bootstrap_1.ModalDirective)
    ], MessageDialogComponent.prototype, "modal", void 0);
    MessageDialogComponent = __decorate([
        core_1.Component({
            selector: 'message-dialog',
            templateUrl: 'app/Shared/Widgets/Dialogs/message.dialog.component.html'
        }),
        __metadata("design:paramtypes", [])
    ], MessageDialogComponent);
    return MessageDialogComponent;
}());
exports.MessageDialogComponent = MessageDialogComponent;

//# sourceMappingURL=mesage.dialog.component.js.map
