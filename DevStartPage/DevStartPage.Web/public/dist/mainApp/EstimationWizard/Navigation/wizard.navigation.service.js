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
var index_1 = require("../Models/index");
var index_2 = require("../Models/index");
var WizardNavigationService = /** @class */ (function () {
    function WizardNavigationService() {
        this.linkCollection = [];
        this.links = {};
        this.linkCollection = [
            new index_1.WizardLink('Home', 'glyphicon glyphicon-home pas-glyph'),
            new index_1.WizardLink('Contact', 'glyphicon glyphicon-user pas-glyph'),
            new index_1.WizardLink('Service', 'pfeicon-hospital_services_icon pas-glyph x-serviceNavButton'),
            new index_1.WizardLink('Insurance', 'pfeicon-insurance pas-glyph x-insuranceNavButton'),
            new index_1.WizardLink('Estimate', 'pfeicon-document pas-glyph x-estimateNavButton')
        ];
        this.links = {
            Home: this.linkCollection[0],
            Contact: this.linkCollection[1],
            Service: this.linkCollection[2],
            Insurance: this.linkCollection[3],
            Estimate: this.linkCollection[4]
        };
    }
    WizardNavigationService.prototype.activate = function (link) {
        if (this.isPreviousTabComplete(link)) {
            this.hideAllTabs();
            this.resetActiveToPending();
            this.linkCollection
                .filter(function (item) { return item.Title === link.Title; })
                .forEach(function (item) {
                item.TabHidden = false;
                item.Current = true;
                if (item.State === index_2.TabState.Pending) {
                    item.activate();
                }
            });
        }
    };
    WizardNavigationService.prototype.isPreviousTabComplete = function (link) {
        var index = this.linkCollection.findIndex(function (item) { return item.Title === link.Title; });
        index = index - 1;
        if (index >= 0) {
            var previousLink = this.linkCollection[index];
            if (previousLink.State !== index_2.TabState.Complete) {
                return false;
            }
        }
        return true;
    };
    WizardNavigationService.prototype.hideAllTabs = function () {
        this.linkCollection
            .forEach(function (item) {
            item.TabHidden = true;
            item.Current = false;
        });
    };
    WizardNavigationService.prototype.resetActiveToPending = function () {
        this.linkCollection
            .filter(function (item) { return item.State === index_2.TabState.Active; })
            .forEach(function (item) { return item.inActivate(); });
    };
    WizardNavigationService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], WizardNavigationService);
    return WizardNavigationService;
}());
exports.WizardNavigationService = WizardNavigationService;

//# sourceMappingURL=wizard.navigation.service.js.map
