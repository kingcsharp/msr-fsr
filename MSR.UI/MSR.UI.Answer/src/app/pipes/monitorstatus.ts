import { Pipe, PipeTransform } from '@angular/core';
import { EnumMonitorInputType } from '../models/enums/EnumMonitorInputType';
import { EnumMonitorPassFailStatus } from '../models/enums/EnumMonitorPassFailStatus';
import { EnumMonitorType } from '../models/enums/EnumMonitorType';
import { WorkOrderTaskMonitorModel } from '../services/api.client.generated';

@Pipe({
    name: 'monitorstatus'
})
export class MonitorStatusPipe implements PipeTransform {

    constructor() {
    }

    transform(workOrderTaskMonitor: WorkOrderTaskMonitorModel): any {
        if (workOrderTaskMonitor) {
            return this.isMonitorPassing(workOrderTaskMonitor);
        }
    }

    isMonitorPassing(workOrderTaskMonitor: WorkOrderTaskMonitorModel) {

        const monitorType = workOrderTaskMonitor?.procedureStepMonitor?.monitorTypeId;

        switch (monitorType) {
            case EnumMonitorType.Equipment:
                return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null && workOrderTaskMonitor.textVal !== '');
            case EnumMonitorType.Number:

                if (workOrderTaskMonitor.procedureStepMonitor?.inputTypeId === EnumMonitorInputType.Sensor) {
                    return (workOrderTaskMonitor.sensorValue !== undefined && workOrderTaskMonitor.sensorValue !== null && workOrderTaskMonitor.sensorValue !== '');
                }

                if (workOrderTaskMonitor.procedureStepMonitor?.inputTypeId === EnumMonitorInputType.Manual) {


                    if (workOrderTaskMonitor.procedureStepMonitor.targetValue === null ||
                        workOrderTaskMonitor.procedureStepMonitor.targetValue === undefined ||
                        workOrderTaskMonitor.procedureStepMonitor.targetValue === '') {
                        return false;
                    }

                    let targetValue = Number(workOrderTaskMonitor.procedureStepMonitor.targetValue);

                    switch (workOrderTaskMonitor.procedureStepMonitor.shouldBe) {
                        case 'EQUAL': {
                            return (targetValue === workOrderTaskMonitor.numVal);
                        }
                        case 'ABOVE': {
                            return (targetValue <= workOrderTaskMonitor.numVal);
                        }
                        case 'BELOW': {
                            return (targetValue >= workOrderTaskMonitor.numVal);
                        }
                        case 'BETWEEN': {

                            return (workOrderTaskMonitor.procedureStepMonitor.lowTarget <= workOrderTaskMonitor.numVal &&
                                workOrderTaskMonitor.procedureStepMonitor.highTarget >= workOrderTaskMonitor.numVal);
                        }
                        default:
                            return false;
                    }


                }

                return workOrderTaskMonitor.numVal;
            case EnumMonitorType.YesOrNo:
                return workOrderTaskMonitor.numVal === EnumMonitorPassFailStatus.PassOrYes;
            case EnumMonitorType.Text:
                return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null && workOrderTaskMonitor.textVal !== '');
            case EnumMonitorType.Select:
                return (workOrderTaskMonitor.multiVal !== undefined && workOrderTaskMonitor.multiVal !== null);
            case EnumMonitorType.PassOrFail:
                return workOrderTaskMonitor.numVal === EnumMonitorPassFailStatus.PassOrYes;
            default:
                break;
        }

        return false;
    }

}
