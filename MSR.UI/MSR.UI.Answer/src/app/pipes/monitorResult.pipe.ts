import { Pipe, PipeTransform } from '@angular/core';
import { WorkOrderTaskMonitorModel } from '../services/api.client.generated';

@Pipe({
    name: 'monitorResult'
})
export class MonitorResultPipe implements PipeTransform {
    currentUserGmtTimezone: string;
    constructor() {
    }

    transform(value: WorkOrderTaskMonitorModel): any {
        if (value) {
            let convert = this.getResultFromMonitor(value);
            return convert;
        }
    }

    getResultFromMonitor(workOrderTaskMonitor: WorkOrderTaskMonitorModel) {
        const monitorType = workOrderTaskMonitor?.procedureStepMonitor?.monitorTypeId;
        switch (monitorType) {
            case 1:
            case 2:
                return workOrderTaskMonitor.numVal;
            case 3:
                return workOrderTaskMonitor.numVal === 1 ? 'Yes' : 'No';
            case 4:
                // value is textVal
            case 6:
                // value is saved in multival but in workorderGet we are setting the text value in the text field out of what multival has.
                return workOrderTaskMonitor.textVal;
            case 5:
                return workOrderTaskMonitor.numVal === 1 ? 'Pass' : 'Fail';
            default:
                break;
        }
    }
}
