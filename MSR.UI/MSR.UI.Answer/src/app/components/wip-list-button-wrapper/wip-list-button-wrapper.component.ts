import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'wiplistbutton-wrapper',
  templateUrl: './wip-list-button-wrapper.component.html',
  styleUrls: ['./wip-list-button-wrapper.component.scss']
})
export class WipListButtonWrapperComponent implements OnInit {

  showWipListDialog: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  showWipList() {
    this.showWipListDialog = !this.showWipListDialog;
  }

}
