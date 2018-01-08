import { Component, ViewChild, OnDestroy } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'message-dialog',
    templateUrl: 'app/Shared/Widgets/Dialogs/message.dialog.component.html'
})
export class MessageDialogComponent {
    private message: string = '';
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