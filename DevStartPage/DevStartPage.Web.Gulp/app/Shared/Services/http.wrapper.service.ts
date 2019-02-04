import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { ErrorHandlingService } from "./error.handling.service";

@Injectable()
export class HttpWrapperService {
    constructor(private http: Http, private errorHandler: ErrorHandlingService) {
    }

	get(apiUrl: string): Observable<any> {
		return this.http.get(apiUrl)
			.map(response => response.json())
			.catch(error => this.errorHandler.handleError(error));
	}

	post(apiUrl: string, requestObj) {
		return this.http.post(apiUrl, requestObj)
			.map(response => response.json())
			.catch(error => this.errorHandler.handleError(error));
	}
}