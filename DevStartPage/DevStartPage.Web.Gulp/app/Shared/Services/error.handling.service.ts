import { Injectable } from '@angular/core';
import { Http, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { Toast } from './toast.service';
import { DisplaySettings } from "./display.settings";
import { HttpErrorMessages } from '../UserMessages/index';
import { HttpStatusCode } from '../Enums/index';
import { Router } from '@angular/router';
import { FancySpinnerService } from '../Widgets/index';

@Injectable()
export class ErrorHandlingService {
    constructor(private http: Http,
        private toast: Toast,
        private displaySettings: DisplaySettings,
        private router: Router,
        private fancySpinnerSvc: FancySpinnerService) {
    }

    handleError(error: any) {
        //Stop the spinner immediately
        this.fancySpinnerSvc.stop('wizard');

        if (!error) {
            this.toast.error(HttpErrorMessages.Unknown.Title, HttpErrorMessages.Unknown.Message);
            return Observable.throw(error);
        }

        if (error.status === HttpStatusCode.Unauthorized) {
            //Dont throw Error, Redirect to Unauthorized Page
            this.router.navigateByUrl('/unauthorized');
            return Observable.throw(error);
        }

        if (error.status === HttpStatusCode.InternalServerError) {
            var errorBody = error.json() || {};
            var errorMessage = errorBody.Message || HttpErrorMessages.InternalServerError.Message;        
            this.toast.error(errorMessage);
            return Observable.throw(error);
        }

        if(error instanceof Response) {
            let message = error.json().error || 'Server Error';
            return Observable.throw(message);
        }
        
        return Observable.throw(error);
    }
}