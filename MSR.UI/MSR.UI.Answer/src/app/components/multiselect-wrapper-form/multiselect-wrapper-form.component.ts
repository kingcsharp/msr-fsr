import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';

@Component({
  selector: 'multiselect-wrapper-form',
  templateUrl: './multiselect-wrapper-form.component.html'
})
export class MultiselectWrapperFormComponent implements OnInit {
  multiselectName: string = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);

  @Input() options: any;
  @Input() model: any;
  @Input() defaultLabel: string;
  @Input() defaultLabelProperty: string;
  @Output() modelChange = new EventEmitter<any>();

  currentOptions: any = [];
  selectedObjs: any;
  constructor() {
  }

  ngOnInit(): void {
    this.selectedObjs = { items: [] };
    this.options.map((x) => {
      this.currentOptions.push({ label: x.name, value: { id: x.id, name: x.name } });
    });
    this.setSelectedObjects();
  }

  getElementValue(id) {
    let length = this.currentOptions.length;
    while (length--) {
      if (this.currentOptions[length].value.id === id) {
        return this.currentOptions[length].value;
      }
    }
    return null;
  }

  setSelectedObjects() {
    if (this.model !== undefined) {
      this.model.map((x) => {
        let elem = this.getElementValue(x);
        if (elem !== null) {
          this.selectedObjs.items.push(elem);
        }
      });
    }
  }

  updateModelVal() {
    if (this.model === undefined) {
      this.model = [];
    } else {
      this.model.length = 0;
    }
    this.selectedObjs.items.map((x) => {
      this.model.push(x.id);
    });
    this.modelChange.emit(this.model);
  }
}
