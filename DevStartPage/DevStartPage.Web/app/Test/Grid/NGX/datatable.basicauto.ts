import { Component } from '@angular/core';

@Component({
    selector: 'basic-auto-demo',
    templateUrl: './app/Test/Grid/NGX/basic-auto-table.html'
})
export class BasicAutoComponent {

    rows = [];
    loadingIndicator: boolean = true;
    reorderable: boolean = true;

    columns = [
        { prop: 'name' },
        { name: 'Gender' },
        { name: 'Company' }
    ];

    constructor() {
        this.rows = [
            { name: 'Austin', gender: 'Male', company: 'Swimlane' },
            { name: 'Dany', gender: 'Male', company: 'KFC' },
            { name: 'Molly', gender: 'Female', company: 'Burger King' },
        ];
    }
}