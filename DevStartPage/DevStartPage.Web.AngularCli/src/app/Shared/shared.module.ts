import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent, FooterComponent, HeaderComponent, SpinnerComponent, MessageDialogComponent } from './Widgets/index';
import { ToastyModule, ToastyService } from 'ng2-toasty';
import { Toast, DisplaySettings, HttpWrapperService, SessionManager } from './Services/index';
import { ModalModule, TooltipModule } from 'ngx-bootstrap';

@NgModule({
    imports: [CommonModule, ToastyModule.forRoot(), ModalModule.forRoot() , ],
    declarations: [NavbarComponent, FooterComponent, HeaderComponent, SpinnerComponent, MessageDialogComponent],
    exports: [NavbarComponent, FooterComponent, HeaderComponent, SpinnerComponent, MessageDialogComponent],
    providers: [ToastyService, DisplaySettings, Toast, HttpWrapperService, SessionManager]
})
export class SharedModule {
}