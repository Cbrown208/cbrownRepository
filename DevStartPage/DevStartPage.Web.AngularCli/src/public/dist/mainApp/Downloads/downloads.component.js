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
var angular5_data_table_1 = require("angular5-data-table");
var index_1 = require("../Shared/Widgets/index");
var downloads_service_1 = require("./Services/downloads.service");
var router_1 = require("@angular/router");
var DownloadsComponent = /** @class */ (function () {
    function DownloadsComponent(injector, spinnerSvc, router, downloadSvc) {
        var _this = this;
        this.spinnerSvc = spinnerSvc;
        this.router = router;
        this.downloadSvc = downloadSvc;
        this.navLinks = {};
        this.shopRequest = {};
        this.filesList = [];
        this.itemCount = 0;
        this.itemResource = new angular5_data_table_1.DataTableResource([]);
        this.downloadSvc.GetFileDownloadList().subscribe(function (data) {
            _this.filesList = data;
            _this.initGrid(_this.filesList);
        });
        this.fancySpinnerSvc = injector.get(index_1.FancySpinnerService);
    }
    DownloadsComponent.prototype.ngOnInit = function () { };
    DownloadsComponent.prototype.initGrid = function (items) {
        this.itemCount = items.length;
        this.itemResource = new angular5_data_table_1.DataTableResource(items);
        //this.dtDownloadsTable._triggerReload();
    };
    DownloadsComponent.prototype.refreshTable = function () {
        var _this = this;
        this.downloadSvc.GetFileDownloadList().subscribe(function (data) {
            _this.filesList = data;
            _this.initGrid(_this.filesList);
        });
    };
    DownloadsComponent.prototype.reloadItems = function (params) {
        var _this = this;
        this.itemResource.query(params).then(function (items) { return _this.filesList = items; });
    };
    DownloadsComponent.prototype.rowClick = function (rowEvent) {
        console.log('Clicked: ' + rowEvent.row.item.Name);
    };
    DownloadsComponent.prototype.rowDoubleClick = function (rowEvent) {
        window.open(rowEvent.row.item.download);
        alert('Double clicked: ' + rowEvent.row.item.Name);
    };
    DownloadsComponent.prototype.downloadFile = function (item) {
        window.open("src/app/Downloads/Files/" + item.row.item.Name);
    };
    __decorate([
        core_1.ViewChild('dtDownloadsTable'),
        __metadata("design:type", angular5_data_table_1.DataTable)
    ], DownloadsComponent.prototype, "dtDownloadsTable", void 0);
    DownloadsComponent = __decorate([
        core_1.Component({
            selector: 'downloads-page',
            templateUrl: 'downloads.component.html',
            providers: [downloads_service_1.DownloadsService]
        }),
        __metadata("design:paramtypes", [core_1.Injector,
            index_1.FancySpinnerService,
            router_1.Router,
            downloads_service_1.DownloadsService])
    ], DownloadsComponent);
    return DownloadsComponent;
}());
exports.DownloadsComponent = DownloadsComponent;
//# sourceMappingURL=downloads.component.js.map