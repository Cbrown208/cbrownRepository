import { Component, OnInit, Injector } from '@angular/core';
import { FancySpinnerService } from '../Shared/Widgets/index';
import { StartPageService } from './Services/startpage.service';
import { Router } from '@angular/router';

@Component({
	selector: 'startpage-page',
	templateUrl: 'startpage.component.html',
	styleUrls: ['../../assets/Content/styles/Site.css'],
	providers: [StartPageService]
})

export class StartPageComponent implements OnInit {
	fancySpinnerSVC: FancySpinnerService;
	ipAddress = "";
	currentDate;
	
	constructor(injector: Injector,
		private spinnerSvc: FancySpinnerService,
		private router: Router,
		private startPageService: StartPageService) {
		this.startPageService.GetIpAddress().subscribe((data) => {
			this.ipAddress = data;
		});
		setInterval(() => { this.currentDate = new Date(); }, 1);
		this.fancySpinnerSVC = injector.get(FancySpinnerService);
	}

	ngOnInit() {

	}
}