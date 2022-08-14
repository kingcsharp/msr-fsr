import { Component, Input, OnInit, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "app-editable-cell",
  templateUrl: "./editable-cell.component.html",
  styleUrls: ["./editable-cell.component.scss"]
})
export class EditableCellComponent implements OnInit {
  constructor() {}
  @Input() isVisible = true;
  @Input() value: string;
  @Input() index: number;
  @Input() displayChangeButton = true;
  @Input() tooltip;
  @Output() change = new EventEmitter<string>();
  @Output() submit = new EventEmitter<string>();
  editable = false;
  originalValue = "";

  ngOnInit(): void {
    this.originalValue = this.value;
  }

  handleSubmit() {
    this.editable = false;
    this.originalValue = this.value;
    this.submit.emit(this.value);
  }

  handleChange() {
    this.editable = true;
    this.change.emit(this.value);
  }

  handleCancel() {
    this.value = this.originalValue;
    this.editable = false;
  }
}
