import { Pipe, PipeTransform } from '@angular/core';
import { EnumMonitorInputType } from '../models/enums/EnumMonitorInputType';
import { EnumMonitorPassFailStatus } from '../models/enums/EnumMonitorPassFailStatus';
import { EnumMonitorShouldBe } from '../models/enums/EnumMonitorShouldBe';
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
                    return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null
                        && workOrderTaskMonitor.textVal !== '' && workOrderTaskMonitor.textVal !== 'No Sensor Value Available');
                }

                if (workOrderTaskMonitor.procedureStepMonitor?.inputTypeId === EnumMonitorInputType.Manual) {

                    if (workOrderTaskMonitor.numVal === null ||
                        workOrderTaskMonitor.numVal === undefined) {
                        return false;
                    }

                    let targetValue = Number(workOrderTaskMonitor.procedureStepMonitor.targetValue);

                    switch (workOrderTaskMonitor.procedureStepMonitor.shouldBe) {
                        case EnumMonitorShouldBe.EQUAL: {
                            return (targetValue === workOrderTaskMonitor.numVal);
                        }
                        case EnumMonitorShouldBe.ABOVE: {
                            return (targetValue <= workOrderTaskMonitor.numVal);
                        }
                        case EnumMonitorShouldBe.BELOW: {
                            return (workOrderTaskMonitor.numVal <= targetValue);
                        }
                        case EnumMonitorShouldBe.BETWEEN: {

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
