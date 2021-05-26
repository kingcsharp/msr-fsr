import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel } from '../../services/api.client.generated';

@Component({
  selector: 'detailed-packing-list',
  templateUrl: './detailed-packing-list.component.html',
  styleUrls: ['./detailed-packing-list.component.scss']
})
export class DetailedPackingListComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;

  constructor() { }

  ngOnInit(): void {
  }

}
