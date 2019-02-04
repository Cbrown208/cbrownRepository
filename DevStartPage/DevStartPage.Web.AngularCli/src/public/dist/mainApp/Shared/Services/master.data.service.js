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
var state_1 = require("../Models/state");
var MasterDataService = /** @class */ (function () {
    function MasterDataService() {
    }
    MasterDataService.prototype.getStates = function () {
        var stateList = [
            { "Alabama": "AL" },
            { "Alaska": "AK" },
            { "Arizona": "AZ" },
            { "Arkansas": "AR" },
            { "California": "CA" },
            { "Colorado": "CO" },
            { "Connecticut": "CT" },
            { "Delaware": "DE" },
            { "Florida": "FL" },
            { "Georgia": "GA" },
            { "Hawaii": "HI" },
            { "Idaho": "ID" },
            { "Illinois": "IL" },
            { "Indiana": "IN" },
            { "Iowa": "IA" },
            { "Kansas": "KS" },
            { "Kentucky": "KY" },
            { "Louisiana": "LA" },
            { "Maine": "ME" },
            { "Maryland": "MD" },
            { "Massachusetts": "MA" },
            { "Michigan": "MI" },
            { "Minnesota": "MN" },
            { "Mississippi": "MS" },
            { "Missouri": "MO" },
            { "Montana": "MT" },
            { "Nebraska": "NE" },
            { "Nevada": "NV" },
            { "New Hampshire": "NH" },
            { "New Jersey": "NJ" },
            { "New Mexico": "NM" },
            { "New York": "NY" },
            { "North Carolina": "NC" },
            { "North Dakota": "ND" },
            { "Ohio": "OH" },
            { "Oklahoma": "OK" },
            { "Oregon": "OR" },
            { "Pennsylvania": "PA" },
            { "Rhode Island": "RI" },
            { "South Carolina": "SC" },
            { "South Dakota": "SD" },
            { "Tennessee": "TN" },
            { "Texas": "TX" },
            { "Utah": "UT" },
            { "Vermont": "VT" },
            { "Virginia": "VA" },
            { "Washington": "WA" },
            { "West Virginia": "WV" },
            { "Wisconsin": "WI" },
            { "Wyoming": "WY" }
        ];
        var states = stateList.map(function (value, index) {
            var key = Object.keys(value)[0];
            return new state_1.State(value[key], key);
        });
        return states;
    };
    MasterDataService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], MasterDataService);
    return MasterDataService;
}());
exports.MasterDataService = MasterDataService;
//# sourceMappingURL=master.data.service.js.map