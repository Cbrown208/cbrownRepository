"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var index_1 = require("./index");
var WizardLink = /** @class */ (function () {
    function WizardLink(title, iconClass) {
        this.Title = title;
        this.IconClass = iconClass;
        this.State = index_1.TabState.Pending;
        this.StateClass = 'round-tabs-pending';
        this.TabHidden = true;
        this.Current = false;
    }
    WizardLink.prototype.inActivate = function () {
        this.updateState(index_1.TabState.Pending);
    };
    WizardLink.prototype.activate = function () {
        this.updateState(index_1.TabState.Active);
    };
    WizardLink.prototype.complete = function () {
        this.updateState(index_1.TabState.Complete);
    };
    WizardLink.prototype.updateState = function (state) {
        this.State = state;
        switch (state) {
            case index_1.TabState.Active:
                this.StateClass = 'round-tabs-active';
                break;
            case index_1.TabState.Pending:
                this.StateClass = 'round-tabs-pending';
                break;
            case index_1.TabState.Complete:
                this.StateClass = 'round-tabs-complete';
                break;
        }
    };
    return WizardLink;
}());
exports.WizardLink = WizardLink;

//# sourceMappingURL=wizard.link.model.js.map
