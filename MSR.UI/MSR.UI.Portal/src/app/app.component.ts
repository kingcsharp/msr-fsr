
import { Component, OnInit } from '@angular/core';
import { Globals } from './models/lib/globals';

@Component({
  selector: 'app-root',
  template: `<router-outlet></router-outlet>`
})
export class AppComponent {
  constructor(private globalService: Globals) {
  }

  ngOnInit() {
  }
}
