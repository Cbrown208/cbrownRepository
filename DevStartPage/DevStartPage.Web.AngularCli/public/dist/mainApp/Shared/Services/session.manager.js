"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var session_model_1 = require("./../Models/session.model");
var Subject_1 = require("rxjs/Subject");
var SessionManager = /** @class */ (function () {
    function SessionManager() {
        var _this = this;
        this.hasSessionReset = new Subject_1.Subject();
        this.getSessionName = function () {
            return _this.session.Name;
        };
        this.getSessionIp = function () {
            return _this.session.Ip;
        };
        this.sessionUpdation = new Subject_1.Subject();
        this.session = new session_model_1.SessionModel();
        this.hasSessionReset = new Subject_1.Subject();
    }
    SessionManager.prototype.setSessionName = function (data) {
        this.session.Name = data;
    };
    ;
    SessionManager.prototype.setSessionIp = function (data) {
        this.session.Ip = data;
    };
    ;
    SessionManager.prototype.triggerSessionUpdate = function () {
        this.sessionUpdation.next(true);
    };
    return SessionManager;
}());
exports.SessionManager = SessionManager;

//# sourceMappingURL=session.manager.js.map
