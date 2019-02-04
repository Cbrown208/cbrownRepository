import { Component, OnInit } from '@angular/core';
import { links } from './navbar.links.collection';
@Component({
    selector: 'navbar',
    templateUrl: 'app/Shared/Widgets/navbar/navbar.component.html'
})

export class NavbarComponent implements OnInit {
    links: any[] = [];
    constructor() {
        this.links = links;
    }

    ngOnInit() { }
}