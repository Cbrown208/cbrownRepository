import { Injectable } from '@angular/core';
import { ToastyService, ToastyConfig, ToastOptions, ToastData } from 'ng2-toasty';
import { Subject, Observable, Subscription } from 'rxjs/Rx';

export enum ToastType {
    Success,
    Error,
    Warning,
    Info,
    Wait,
    Default
}

@Injectable()
export class Toast {
    toastOptions: ToastOptions;

    constructor(private toastService: ToastyService, private toastyConfig: ToastyConfig) {
        let interval = 1000;
        let timeout = 1000 * 5;
        let subscription: Subscription;
        let seconds = timeout / 1000;

        this.toastOptions = {
            title: '',
            showClose: true,
            timeout: timeout,
            theme: 'bootstrap'
        };
    }

    render(type: ToastType, title: string, msg?: string, isPersistent?: boolean) {
        this.toastOptions.title = title;
        this.toastOptions.msg = msg || '';

        if (isPersistent)
            this.toastOptions.timeout = 50000;

        switch (type) {
            case ToastType.Default: this.toastService.default(this.toastOptions); break;
            case ToastType.Info: this.toastService.info(this.toastOptions); break;
            case ToastType.Success: this.toastService.success(this.toastOptions); break;
            case ToastType.Wait: this.toastService.wait(this.toastOptions); break;
            case ToastType.Error: this.toastService.error(this.toastOptions); break;
            case ToastType.Warning: this.toastService.warning(this.toastOptions); break;
        }
    }

    info(title: string, msg?: string, isPersistent?: boolean) {
        this.render(ToastType.Info, title, msg, isPersistent);
    }

    success(title: string, msg?: string, isPersistent?: boolean) {
        this.render(ToastType.Success, title, msg, isPersistent);
    }

    wait(title: string, msg?: string, isPersistent?: boolean) {
        this.render(ToastType.Wait, title, msg, isPersistent);
    }

    error(title: string, msg?: string, isPersistent?: boolean) {
        this.render(ToastType.Error, title, msg, isPersistent);
    }

    warning(title: string, msg?: string, isPersistent?: boolean) {
        this.render(ToastType.Warning, title, msg, isPersistent);
    }
}