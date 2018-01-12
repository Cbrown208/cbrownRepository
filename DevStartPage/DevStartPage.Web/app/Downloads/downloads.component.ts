import { Component, OnInit, ViewChild, Injector } from '@angular/core';
import { DataTable, DataTableResource } from 'angular-2-data-table';
import { FancySpinnerService } from '../Shared/Widgets/index';
import { DownloadsService } from './Services/downloads.service';
import { Router } from '@angular/router';

@Component({
	selector: 'downloads-page',
	templateUrl: './app/Downloads/downloads.component.html',
	providers: [DownloadsService]
})

export class DownloadsComponent implements OnInit {
	navLinks: any = {};
	shopRequest: any = {};
	filesList = [];
	itemCount = 0;
	fancySpinnerSvc: FancySpinnerService;
	itemResource = new DataTableResource([]);
	@ViewChild('dtDownloadsTable') dtDownloadsTable: DataTable;

	constructor(injector: Injector,
		private spinnerSvc: FancySpinnerService,
		private router: Router,
		private downloadSvc: DownloadsService) {
		this.downloadSvc.GetFileDownloadList().subscribe(data => {
			this.filesList = data;
			this.initGrid(this.filesList);
		});
		this.fancySpinnerSvc = injector.get(FancySpinnerService);
	}

	ngOnInit() { }

	initGrid(items) {
		this.itemCount = items.length;
		this.itemResource = new DataTableResource(items);
		this.dtDownloadsTable._triggerReload();
	}

	refreshTable() {
		this.downloadSvc.GetFileDownloadList().subscribe(data => {
			this.filesList = data;
			this.initGrid(this.filesList);
		});
	}

	reloadItems(params) {
		this.itemResource.query(params).then(items => this.filesList = items);
	}


	rowClick(rowEvent) {
		console.log('Clicked: ' + rowEvent.row.item.Name);
	}

	rowDoubleClick(rowEvent) {
		window.open(rowEvent.row.item.download);
		alert('Double clicked: ' + rowEvent.row.item.Name);
	}

	downloadFile(item) {
		window.open("app/Downloads/Files/" + item);
	}
}