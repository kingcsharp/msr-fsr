import { Pipe, PipeTransform } from '@angular/core';
import { EnumMonitorInputType } from '../models/enums/EnumMonitorInputType';
import { EnumMonitorType } from '../models/enums/EnumMonitorType';
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
        const monitorType =  workOrderTaskMonitor?.procedureMonitorId !== undefined && workOrderTaskMonitor?.procedureMonitorId !== null 
        ? workOrderTaskMonitor?.procedureStepMonitor?.monitorTypeId : workOrderTaskMonitor?.monitorTypeId;
        switch (monitorType) {
            case EnumMonitorType.Equipment:
                return workOrderTaskMonitor.textVal;
            case EnumMonitorType.Number: {

                if (workOrderTaskMonitor.procedureStepMonitor?.inputTypeId === EnumMonitorInputType.Sensor || workOrderTaskMonitor?.inputTypeId === EnumMonitorInputType.Sensor) {
                    return workOrderTaskMonitor.textVal;
                } else {
                    return workOrderTaskMonitor.numVal;
                }

            }
            case EnumMonitorType.YesOrNo: {

                if (workOrderTaskMonitor.numVal === null || workOrderTaskMonitor.numVal === undefined) {
                    return null;
                } else {
                    return workOrderTaskMonitor.numVal === 1 ? 'Yes' : 'No';
                }

            }
            case EnumMonitorType.Text:
                return workOrderTaskMonitor.textVal;
            case EnumMonitorType.PassOrFail: {

                if (workOrderTaskMonitor.numVal === null || workOrderTaskMonitor.numVal === undefined) {
                    return null;
                } else {
                    return workOrderTaskMonitor.numVal === 1 ? 'Pass' : 'Fail';
                }

            }
            case EnumMonitorType.Select:
                return workOrderTaskMonitor.textVal;
            default:
                break;
        }
    }
}
