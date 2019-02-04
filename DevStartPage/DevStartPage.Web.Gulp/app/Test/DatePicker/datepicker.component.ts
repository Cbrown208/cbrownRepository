import { Component } from "@angular/core";
import { INgxMyDpOptions, IMyDateModel } from 'ngx-mydatepicker';
// other imports here...
@Component({
    selector: 'DatePicker',
    templateUrl: 'app/Test/DatePicker/datepicker.component.html'
}
)
export class DatePickerComponent {

    private myOptions: INgxMyDpOptions = {
        // other options...
        dateFormat: 'dd.mm.yyyy'
    };

    // Initialized to specific date (09.10.2018)
    private model: Object = { date: { year: 2018, month: 10, day: 9 } };

    constructor() { }

    // optional date changed callback
    onDateChanged(event: IMyDateModel): void {
        // date selected
    }
}