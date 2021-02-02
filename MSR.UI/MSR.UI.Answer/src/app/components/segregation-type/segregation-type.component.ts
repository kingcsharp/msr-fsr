import { ChangeDetectorRef } from '@angular/core';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { EnumSegregationType } from '../../services/api.client.generated';

@Component({
  selector: 'segregation-type',
  templateUrl: './segregation-type.component.html',
  styleUrls: ['./segregation-type.component.scss']
})
export class SegregationTypeComponent implements OnInit {

  @Input() segregationType: EnumSegregationType;
  @Output() segregationTypeChange: EventEmitter<EnumSegregationType> = new EventEmitter<EnumSegregationType>();
  EnumSegregationType = EnumSegregationType;

  constructor(private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
  }

  segregationTypeSelected(selectedSegregationType:EnumSegregationType){
    this.segregationTypeChange.emit(selectedSegregationType);
  }

}
