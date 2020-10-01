import { Component, Input, OnInit } from '@angular/core';
import { DataLabel } from './../../models/data-label-model';
import { WorkOrderModel, WorkOrderTaskMonitorModel } from '../../services/api.client.generated';

@Component({
  selector: 'technical-data-label',
  templateUrl: './technical-data-label.component.html',
  styleUrls: ['./technical-data-label.component.scss']
})

export class TechnicalDataLabelComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  labels: Array<DataLabel> = new Array<DataLabel>();

  constructor() { }

  ngOnInit(): void {

    this.WorkOrder.workOrderTasks.map(s => {

      if (s.workOrderTaskMonitors === null || s.workOrderTaskMonitors === undefined) {
        s.workOrderTaskMonitors = new Array<WorkOrderTaskMonitorModel>();
      }

    });

    this.generateLabels();

  }

  generateLabels() {

    this.WorkOrder.workOrderTasks.forEach(workOrderTask => {

      for (let index = 0; index < workOrderTask.workOrderTaskMonitors.length; index++) {

        let dataLabel = new DataLabel();
        dataLabel.CustomerPurchaseNumber  = this.WorkOrder.purchase.customerPurchaseNumber;
        dataLabel.Date = this.WorkOrder.scheduledEndDate;
        dataLabel.MonitorName = workOrderTask.workOrderTaskMonitors[index].procedureStepMonitor?.description;
        dataLabel.PartName = this.WorkOrder.product?.part?.name;
        dataLabel.PartNumber = this.WorkOrder.product?.part?.partNumber;
        dataLabel.Requestee = workOrderTask.assignedToUser?.fullName;
        dataLabel.TaskDescription = workOrderTask.procedureStep?.stepText;
        this.labels.push(dataLabel);

      }

    });

  }

}
