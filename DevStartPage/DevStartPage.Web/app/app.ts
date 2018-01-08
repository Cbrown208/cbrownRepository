import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { FancySpinnerService } from './Shared/Widgets/index';

@Component({
    selector: 'my-app',
    templateUrl: './app/app.html'
})
export class AppComponent {
    userprofile;
    title = 'Testing Command Center';
    configs = ['Facility Settings 1', 'Facility Settings 2', 'Facility Settings 3', 'Facility Settings 4'];
    currentConfig = this.configs[1];

    constructor(private spinnerSvc: FancySpinnerService) { 
    }
}
