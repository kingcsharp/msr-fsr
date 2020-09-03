import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';

@Component({
  selector: 'bootstrap-switch',
  templateUrl: './bootstrap-switch.component.html',
  styleUrls: ['./bootstrap-switch.component.scss']
})
export class BootstrapSwitchComponent implements OnInit {

  @Input() isOn: boolean = false;
  @Output() onChange = new EventEmitter<boolean>();

  toggle: boolean;
  marginLeft: number;

  constructor() { }

  ngOnInit(): void {
    this.marginLeft =  this.isOn ?  0 : -53;
  }

  onClickToggle() {
    console.log(this.isOn);
    this.onChange.emit(!this.isOn);
  }

}
