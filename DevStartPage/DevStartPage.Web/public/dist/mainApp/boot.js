"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/http");
var ng_bootstrap_1 = require("@ng-bootstrap/ng-bootstrap");
var ngx_bootstrap_1 = require("ngx-bootstrap");
var ngx_tabs_1 = require("ngx-tabs");
var ngx_dropdown_1 = require("ngx-dropdown");
var ngx_progressbar_1 = require("ngx-progressbar");
var ngx_mydatepicker_1 = require("ngx-mydatepicker");
var ng2_charts_1 = require("ng2-charts/ng2-charts");
var ng2_toasty_1 = require("ng2-toasty");
var angular2_text_mask_1 = require("angular2-text-mask");
var app_1 = require("./app");
var index_1 = require("./Downloads/index");
var index_2 = require("./StartPage/index");
var shared_module_1 = require("./Shared/shared.module");
var index_3 = require("./Shared/Services/index");
var app_routing_module_1 = require("./app-routing.module");
var angular_2_data_table_1 = require("angular-2-data-table");
var ng2_dnd_1 = require("ng2-dnd");
var ngx_datatable_1 = require("@swimlane/ngx-datatable");
var index_4 = require("./Shared/Widgets/index");
var test_component_1 = require("./Test/test.component");
var datatable_basicauto_1 = require("./Test/Grid/NGX/datatable.basicauto");
var datagrid_component_1 = require("./Test/Grid/datagrid.component");
var simple_dnd_component_1 = require("./Test/DragNDrop/simple-dnd.component");
var datepicker_component_1 = require("./Shared/Widgets/DatePicker/datepicker.component");
var declarations = [
    app_1.AppComponent,
    index_2.StartPageComponent,
    index_1.DownloadsComponent,
    test_component_1.TestComponent,
    datatable_basicauto_1.BasicAutoComponent,
    datagrid_component_1.DataGridComponent,
    simple_dnd_component_1.SimpleDndComponent,
    datepicker_component_1.DatePickerComponent,
    index_4.FancySpinnerComponent
];
var imports = [platform_browser_1.BrowserModule,
    forms_1.FormsModule,
    app_routing_module_1.AppRoutingModule,
    http_1.HttpModule,
    ng_bootstrap_1.NgbModule.forRoot(),
    ngx_bootstrap_1.ModalModule.forRoot(),
    ngx_bootstrap_1.TooltipModule.forRoot(),
    shared_module_1.SharedModule,
    ngx_datatable_1.NgxDatatableModule,
    angular_2_data_table_1.DataTableModule,
    ng2_dnd_1.DndModule.forRoot(),
    ngx_dropdown_1.DropdownModule,
    ngx_progressbar_1.NgProgressModule,
    ngx_tabs_1.TabsModule,
    ngx_mydatepicker_1.NgxMyDatePickerModule,
    ng2_charts_1.ChartsModule,
    ng2_toasty_1.ToastyModule.forRoot(),
    angular2_text_mask_1.TextMaskModule
];
var providers = [index_3.SessionManager,
    index_4.FancySpinnerService,
    index_3.Toast,
    index_3.InitialService,
    index_3.ErrorHandlingService,
    index_3.HttpWrapperService,
    index_3.MasterDataService,
    index_2.StartPageService,
    index_1.DownloadsService
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: imports,
            declarations: declarations,
            providers: providers,
            bootstrap: [app_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;

//# sourceMappingURL=boot.js.map
