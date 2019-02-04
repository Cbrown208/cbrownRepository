import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpWrapperService } from './http.wrapper.service';

@Injectable()
export class InitialService {
    constructor(private http: HttpWrapperService) { }

    startSession() {
        return this.http.get(`Initial/GetInfo`);
    }

    updateSession(nthriveId: number) {
        let apiUrl = `Home/UpdateSession?nthriveId=${nthriveId}`;
        return this.http.get(apiUrl);
    }

    getSiteKey() {
        return this.http.get(`Home/GetSiteKey`);
    }

    callApi() {
        let apiUrl = `Initial/CallWebApi`;
        return this.http.get(apiUrl).toPromise();
    }

    getFacilityOptions(){
        let apiUrl = `Initial/GetFacilityOptions`;
        return this.http.get(apiUrl);
    }
}