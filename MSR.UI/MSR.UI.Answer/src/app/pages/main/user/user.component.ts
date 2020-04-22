import {Component, OnInit, ViewEncapsulation} from '@angular/core';

@Component({
  selector: 'user',
  templateUrl: './user.template.html',
  styleUrls: ['./user.style.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserComponent implements OnInit {
  config: any;
  month: any;
  year: any;

  constructor() {
  }

  ngOnInit(): void {
    const now = new Date();
    this.month = now.getMonth() + 1;
    this.year = now.getFullYear();
  }
}
