import { Component, ViewChild } from '@angular/core';
import { DisplaySettings } from "../../Services/display.settings";

@Component({
    selector: 'spinner-modal',
    templateUrl: 'spinner.html'
})
export class SpinnerComponent {
    showSpinner: boolean = false;
    isDelayedRunning = false;

    @ViewChild('spinnerModal')
    spinnerModal;

    constructor(public displaySettings: DisplaySettings) {
        let that = this;
        that.displaySettings.spinnerStateChanged.subscribe((data) => {
            that.showSpinner = data;

            if (data) {
                that.spinnerModal.show();
            } else {
                that.spinnerModal.hide();
            }
        });
    }
}