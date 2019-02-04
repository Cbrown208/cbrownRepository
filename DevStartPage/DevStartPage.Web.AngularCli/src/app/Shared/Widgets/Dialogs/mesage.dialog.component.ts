import { Component, ViewChild, OnDestroy } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'message-dialog',
    templateUrl: 'message.dialog.component.html'
})
export class MessageDialogComponent {
    public message: string = '';
    @ViewChild('popUpModal')
    private modal: ModalDirective;

    constructor() {
    }

    show(message: string) {
        this.message = message;
        this.modal.show();
    }

    close() {
        this.modal.hide();
    }
}