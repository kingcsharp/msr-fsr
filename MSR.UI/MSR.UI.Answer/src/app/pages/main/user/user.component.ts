import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { UserService } from './user.service';

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
  data: any;
  loading: boolean = true;

  constructor(public userService: UserService) {
  }

  ngOnInit(): void {
    const now = new Date();
    this.month = now.getMonth() + 1;
    this.year = now.getFullYear();
    this.getUsers();
  }

  async getUsers() {
    //data
    this.data = await this.userService.getUsers();
    this.loading = false;
  }
}
