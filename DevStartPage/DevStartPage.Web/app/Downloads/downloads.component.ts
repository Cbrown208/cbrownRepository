import { Component, OnInit, Injector } from '@angular/core';
import { FancySpinnerService } from '../Shared/Widgets/index';
import { Router } from '@angular/router';

@Component({
	selector: 'downloads-page',
	templateUrl: './app/Downloads/downloads.component.html'
})

export class DownloadsComponent implements OnInit {
	navLinks: any = {};
	shopRequest: any = {};
	fancySpinnerSVC: FancySpinnerService;

	constructor(injector: Injector,
		private spinnerSvc: FancySpinnerService,
		private router: Router,
		) {
		this.fancySpinnerSVC = injector.get(FancySpinnerService);
	}

	ngOnInit() {
	}
}