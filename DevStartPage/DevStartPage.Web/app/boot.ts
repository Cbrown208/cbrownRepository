import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalModule, TooltipModule } from 'ngx-bootstrap';
import { TabsModule } from "ngx-tabs";
import { DropdownModule } from "ngx-dropdown";
import { NgProgressModule } from "ngx-progressbar";
import { NgxMyDatePickerModule } from 'ngx-mydatepicker';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { ToastyModule } from 'ng2-toasty';
import { TextMaskModule } from 'angular2-text-mask';

import { AppComponent } from './app';
import { DownloadsComponent, DownloadsService } from './Downloads/index';
import { StartPageComponent, StartPageService } from './StartPage/index';


import { SharedModule } from './Shared/shared.module';
import {
    Toast,
    HttpWrapperService,
    ErrorHandlingService,
    InitialService,
    SessionManager,
    MasterDataService
} from './Shared/Services/index';

import { AppRoutingModule } from './app-routing.module';

import { DataTableModule } from 'angular-2-data-table';
import { DndModule } from 'ng2-dnd';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';

import { FancySpinnerComponent, FancySpinnerService } from './Shared/Widgets/index';
import { TestComponent } from './Test/test.component';
import { BasicAutoComponent } from './Test/Grid/NGX/datatable.basicauto';
import { DataGridComponent } from './Test/Grid/datagrid.component';
import { SimpleDndComponent } from './Test/DragNDrop/simple-dnd.component';

import { DatePickerComponent } from './Shared/Widgets/DatePicker/datepicker.component';

const declarations = [
	AppComponent,
	StartPageComponent,
	DownloadsComponent,
	TestComponent,
    BasicAutoComponent,
    DataGridComponent,
    SimpleDndComponent,
    DatePickerComponent,
    FancySpinnerComponent
];

const imports = [BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpModule,
    NgbModule.forRoot(),
    ModalModule.forRoot(),
    TooltipModule.forRoot(),
    SharedModule,
    NgxDatatableModule,
    DataTableModule,
    DndModule.forRoot(),
    DropdownModule,
    NgProgressModule,
    TabsModule,
    NgxMyDatePickerModule,
    ChartsModule,
    ToastyModule.forRoot(),
    TextMaskModule
];

const providers = [SessionManager,
    FancySpinnerService,
    Toast,
    InitialService,
    ErrorHandlingService,
    HttpWrapperService,
	MasterDataService,
	StartPageService,
	DownloadsService
];

@NgModule({
    imports: imports,
    declarations: declarations,
    providers: providers,
    bootstrap: [AppComponent]
})
export class AppModule { }