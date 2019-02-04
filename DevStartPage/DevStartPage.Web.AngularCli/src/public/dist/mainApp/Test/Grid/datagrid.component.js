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
var data_table_demo1_data_1 = require("./data-table-demo1-data");
var angular5_data_table_1 = require("angular5-data-table");
var index_1 = require("../../Shared/Widgets/index");
var DataGridComponent = /** @class */ (function () {
    function DataGridComponent(spinnerSvc) {
        var _this = this;
        this.spinnerSvc = spinnerSvc;
        this.itemResource = new angular5_data_table_1.DataTableResource(data_table_demo1_data_1.default);
        this.items = [];
        this.itemCount = 0;
        this.itemResource.count().then(function (count) { return _this.itemCount = count; });
    }
    DataGridComponent.prototype.reloadItems = function (params) {
        var _this = this;
        this.itemResource.query(params).then(function (items) { return _this.items = items; });
    };
    // special properties:
    DataGridComponent.prototype.rowClick = function (rowEvent) {
        console.log('Clicked: ' + rowEvent.row.item.name);
    };
    DataGridComponent.prototype.rowDoubleClick = function (rowEvent) {
        alert('Double clicked: ' + rowEvent.row.item.name);
    };
    DataGridComponent.prototype.rowTooltip = function (item) { return item.jobTitle; };
    DataGridComponent.prototype.start = function () {
        this.spinnerSvc.start('grid');
    };
    DataGridComponent.prototype.stop = function () {
        this.spinnerSvc.stop('grid');
    };
    DataGridComponent = __decorate([
        core_1.Component({
            selector: 'data-table1',
            templateUrl: 'datagrid.html'
        }),
        __metadata("design:paramtypes", [index_1.FancySpinnerService])
    ], DataGridComponent);
    return DataGridComponent;
}());
exports.DataGridComponent = DataGridComponent;
//# sourceMappingURL=datagrid.component.js.map