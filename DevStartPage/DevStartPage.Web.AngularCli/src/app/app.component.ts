import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { FancySpinnerService } from './Shared/Widgets/index';
import { CountdownTimerModule } from 'ngx-countdown-timer';

@Component({
	selector: 'my-app',
	templateUrl: 'app.component.html'
})
export class AppComponent {
	userprofile;
	title = 'Testing Command Center';
	configs = ['Facility Settings 1', 'Facility Settings 2', 'Facility Settings 3', 'Facility Settings 4'];
	currentConfig = this.configs[1];

	constructor(private spinnerSvc: FancySpinnerService) {
	}
}
