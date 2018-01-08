import { Injectable } from '@angular/core';
import { Subject, Observable, Subscription, Observer } from 'rxjs/Rx';
import { FancySpinner } from '../../Models/index';

@Injectable()
export class FancySpinnerService {
    spinnerSubject: Subject<FancySpinner> = new Subject<FancySpinner>();
    constructor() {
    }

    start(id?) {
        this.spinnerSubject.next(new FancySpinner(true, id));
    }

    stop(id?) {
        this.spinnerSubject.next(new FancySpinner(false, id));
    }
}