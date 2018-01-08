import { Component, Input, OnInit } from '@angular/core';
import { Toast } from '../Shared/Services/toast.service';
import { DisplaySettings } from "../Shared/Services/display.settings";

@Component({
	selector: 'my-widgetsTest',
	templateUrl: './app/Test/test.component.html'
})
export class TestComponent {
	name: string = "Widgets Test Component";
	constructor(public displaySettings: DisplaySettings) {
	}

	startSpinner() {
		this.displaySettings.startSpinner();
		setTimeout(() => { this.displaySettings.stopSpinner(); }, 3000);
	}
}