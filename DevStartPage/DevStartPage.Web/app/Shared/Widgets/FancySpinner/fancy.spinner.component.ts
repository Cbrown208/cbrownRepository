import { Component, ViewChild, OnInit, OnDestroy, Input } from '@angular/core';
import { FancySpinner } from '../../Models/index';
import { Subject, Observable, Subscription, Observer } from 'rxjs/Rx';
import { FancySpinnerService } from './fancy.spinner.service';

@Component({
    selector: 'fancy-spinner',
    templateUrl: './app/Shared/Widgets/FancySpinner/fancy.spinner.html'
})
export class FancySpinnerComponent implements OnInit, OnDestroy {
    @Input() id: string = '';
    Show: boolean = false;
    SpinnerSubscription: Subscription;

    constructor(private spinnerSvc: FancySpinnerService) {
    }

    ngOnInit() {
        this.SpinnerSubscription = this.spinnerSvc.spinnerSubject.subscribe((item) => {
            if (this.id == item.Id) {
                this.Show = item.Show;
            }
        });
    }

    ngOnDestroy() {
        if (this.SpinnerSubscription) {
            this.Show = false;
            this.SpinnerSubscription.unsubscribe();
        }
    }
}