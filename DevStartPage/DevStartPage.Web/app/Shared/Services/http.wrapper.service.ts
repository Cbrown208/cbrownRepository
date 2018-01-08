import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ErrorHandlingService } from "./error.handling.service";

@Injectable()
export class HttpWrapperService {
    constructor(private http: Http, private errorHandler: ErrorHandlingService) {
    }

    get(apiUrl: string) {
        return this.http.get(apiUrl)
        .map(response => response.text() ? response.json() : null)
        .catch(error => this.errorHandler.handleError(error));
    }

    post(apiUrl: string, payload: any) {
        return this.http.post(apiUrl, payload)
        .map(response => response.text() ? response.json() : null)
        .catch(error => this.errorHandler.handleError(error));
    }
}