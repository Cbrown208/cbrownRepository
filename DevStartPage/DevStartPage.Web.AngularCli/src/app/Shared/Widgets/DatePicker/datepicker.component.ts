//import { Component, Output, EventEmitter, ViewChild } from "@angular/core";
//import { INgxMyDpOptions, IMyDateModel } from 'ngx-mydatepicker';
//// other imports here...
//@Component({
//    selector: 'DatePicker',
//    templateUrl: 'datepicker.component.html'
//}
//)
//export class DatePickerComponent {

//    @Output()
//    public setDate: EventEmitter<string> = new EventEmitter<string>();

//    @Output()
//    public dateValidation: EventEmitter<string> = new EventEmitter<string>();

//    @ViewChild('dp') dp;

//    private myOptions: INgxMyDpOptions = {
//        // other options...
//        dateFormat: 'mm/dd/yyyy',
//    };

//    // Initialized to specific date (09.10.2018)
//    public model: any;

//    constructor() {
//    }

//    setDisableDate(date) {
//        let disableSince: any = this.FormateDate(date);
//        this.myOptions.disableSince = disableSince.date;
//    }

//    setDefaultDate(date) {
//        let defaultDate = new Date(date);
//        if (date) {
//            this.model = this.FormateDate(defaultDate);
//        }
//    }

//    FormateDate(date: Date) {
//        return {
//            date: {
//                year: date.getFullYear(),
//                month: date.getMonth() + 1,
//                day: date.getDate()
//            }
//        };
//    }


//    // optional date changed callback
//    onDateChanged(event: IMyDateModel): void {
//        // date selected
//        this.setDate.emit(event.formatted);
//    }

//    validateDate(testdate) {
//        if (testdate.length === 0) return true;
//        var dateRegex = /^\d{2}\/\d{2}\/\d{4}$/;
//        return dateRegex.test(testdate);
//    };

//    onblur(event) {
//        if (!this.validateDate(event.srcElement.value)) {
//            this.dp.clearDate();
//            this.dateValidation.emit();
//        }
//    };
//}