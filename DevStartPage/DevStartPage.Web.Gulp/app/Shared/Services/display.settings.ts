import { Injectable, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class DisplaySettings {
    spinnerStateChanged: Subject<boolean> = new Subject<boolean>();
    showEstimateDetails: boolean = false;

    constructor() {
    }

    startSpinner() {
        this.spinnerStateChanged.next(true);
    }

    stopSpinner() {
        this.spinnerStateChanged.next(false);
    }

    showDetails() {
        this.showEstimateDetails = !this.showEstimateDetails;
    }
}