import { Component, Input, OnInit, ViewChild, OnDestroy } from '@angular/core';
import persons from './data-table-demo1-data';
import { DataTable, DataTableResource } from 'angular-2-data-table';
import { FancySpinnerService } from '../../Shared/Widgets/index';

@Component({
    selector: 'data-table1',
    templateUrl: './app/Test/Grid/datagrid.html'
})

export class DataGridComponent {
       
    itemResource = new DataTableResource(persons);
    items = [];
    itemCount = 0;

    constructor(private spinnerSvc: FancySpinnerService) {
        this.itemResource.count().then(count => this.itemCount = count);
    }

    reloadItems(params) {
        this.itemResource.query(params).then(items => this.items = items);
    }

    // special properties:

    rowClick(rowEvent) {
        console.log('Clicked: ' + rowEvent.row.item.name);
    }

    rowDoubleClick(rowEvent) {
        alert('Double clicked: ' + rowEvent.row.item.name);
    }

    rowTooltip(item) { return item.jobTitle; }

        
    start() {
        this.spinnerSvc.start('grid');
    }

    stop() {
        this.spinnerSvc.stop('grid');
    }
}