import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Globals } from '../../models/lib/globals';

@Component({
    selector: 'app-landingPage',
    templateUrl: './landing.component.html',
    styleUrls: ['./landing.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class LandingPageComponent implements OnInit {
    userName: string;

    constructor(public globals: Globals) { }

    ngOnInit(): void {
        this.globals.showLoader(true);
        this.userName = this.globals.getCurrentUser().fullName;
        this.globals.showLoader(false);
    }
}
