import { Injectable } from '@angular/core';
import { Toast } from "../../Shared/Services/index";
import { Observable, Subject } from 'rxjs/Rx';
import { HttpWrapperService } from '../../Shared/Services/index';
import { Http, Response } from '@angular/http';

@Injectable()
export class DownloadsService {
	updateMessage: Subject<boolean>;

	constructor(private http: HttpWrapperService,
		private toast: Toast) {
		this.updateMessage = new Subject<boolean>();

	}
	public GetFileDownloadList() {
		var apiUrl = `Downloads/GetFileList`;
		return this.http.get(apiUrl);
	}
}