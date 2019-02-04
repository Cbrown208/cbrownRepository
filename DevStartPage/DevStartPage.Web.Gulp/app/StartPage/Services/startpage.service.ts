import { Injectable } from '@angular/core';
import { Toast } from "../../Shared/Services/index";
import { Observable, Subject } from 'rxjs/Rx';
import { HttpWrapperService } from '../../Shared/Services/index';
import { Http, Response } from '@angular/http';

@Injectable()
export class StartPageService {
	updateMessage: Subject<boolean>;

	constructor(private http: HttpWrapperService,
		private toast: Toast) {
		this.updateMessage = new Subject<boolean>();

	}
	public GetIpAddress() {
		var apiUrl = `Services/GetIpAddress`;
		//return this.http.get(apiUrl)
		//	.map(response => response.json());
		return this.http.get(apiUrl);
	}
}