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
  @Input() defaultId: number;
  @Input() limit: number;
  @Output() modelChange = new EventEmitter<any>();
  currentOptions: any = [];
  selectedObjs: any;
  basicOptions: any;
  constructor() {
  }

  ngOnInit(): void {
    const ctrl = this;
    this.selectedObjs = { items: [] };
    this.basicOptions = {
      name: this.defaultLabelProperty || 'name',
      id: this.defaultId || 'id',
      limit: this.limit || 4
    };

    this.options.map((x) => {
      this.currentOptions.push({ label: x[ctrl.basicOptions.name], value: { id: x[ctrl.basicOptions.id], name: x[ctrl.basicOptions.name] } });
    });
    this.setSelectedObjects();
  }

  removeItem(elem) {
    const index = this.model.findIndex(x => x[this.basicOptions.id] === elem.id);
    const selectedObjIndex = this.selectedObjs.items.findIndex(x => x.id === elem.id);
    this.selectedObjs.items.splice(selectedObjIndex, 1);
    this.model.splice(index, 1);
  }

  getElementValue(elem) {
    let length = this.currentOptions.length;
    while (length--) {
      if (this.currentOptions[length].value.id === elem[this.basicOptions.id]) {
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
    this.emptyArr(this.model);
    const ctrl = this;

    this.selectedObjs.items.map((x) => {
      const elem = ctrl.options.find(u => u[this.basicOptions.id] === x.id);
      this.model.push(elem);
    });
    this.modelChange.emit(this.model);
  }

  emptyArr(arr) {
    if(arr===undefined){
      return;
    }
    let length = arr.length;
    while (length--) {
      arr.pop();
    }
  }
}
