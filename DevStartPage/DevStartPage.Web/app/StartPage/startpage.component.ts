import { Component, OnInit, Injector } from '@angular/core';
import { FancySpinnerService } from '../Shared/Widgets/index';
import { Router } from '@angular/router';

@Component({
	selector: 'startpage-page',
	templateUrl: './app/StartPage/startpage.component.html',
	styleUrls: ['../../Content/styles/Site.css']
})

export class StartPageComponent implements OnInit {
	navLinks: any = {};
	shopRequest: any = {};
	fancySpinnerSVC: FancySpinnerService;

	constructor(injector: Injector,
		private spinnerSvc: FancySpinnerService,
		private router: Router) {
		this.fancySpinnerSVC = injector.get(FancySpinnerService);
	}

	ngOnInit() {
	}
}