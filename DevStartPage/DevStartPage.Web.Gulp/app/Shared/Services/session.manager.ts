import { SessionModel } from './../Models/session.model';
import { Subject } from "rxjs/Subject";

export class SessionManager {
	session: SessionModel;
	emitFacilityList: Subject<Object>;
	hasSessionReset = new Subject<SessionModel>();

	constructor() {
		this.session = new SessionModel();
		this.hasSessionReset = new Subject<SessionModel>();
	}

	setSessionName(data: string) {
		this.session.Name = data;
	};

	setSessionIp(data: string) {
		this.session.Ip = data;
	};


	getSessionName = () => {
		return this.session.Name;
	};

	getSessionIp = () => {
		return this.session.Ip;
	};

	sessionUpdation = new Subject<boolean>();
	triggerSessionUpdate() {
		this.sessionUpdation.next(true);
	}
}