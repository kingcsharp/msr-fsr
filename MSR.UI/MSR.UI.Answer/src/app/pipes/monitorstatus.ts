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

        const monitorType =  workOrderTaskMonitor?.procedureMonitorId !== undefined && workOrderTaskMonitor?.procedureMonitorId !== null 
        ? workOrderTaskMonitor?.procedureStepMonitor?.monitorTypeId : workOrderTaskMonitor?.monitorTypeId;

        switch (monitorType) {
            case EnumMonitorType.Equipment:
                return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null && workOrderTaskMonitor.textVal !== '');
            case EnumMonitorType.Number:

                if (workOrderTaskMonitor.procedureStepMonitor?.inputTypeId === EnumMonitorInputType.Sensor || workOrderTaskMonitor?.inputTypeId === EnumMonitorInputType.Sensor) {
                    return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null
                        && workOrderTaskMonitor.textVal !== '' && workOrderTaskMonitor.textVal !== 'No Sensor Value Available');
                }

                if (workOrderTaskMonitor.procedureStepMonitor?.inputTypeId === EnumMonitorInputType.Manual) {

                    if (workOrderTaskMonitor.numVal === null ||
                        workOrderTaskMonitor.numVal === undefined) {
                        return false;
                    }

                    let targetValue;
                    if(workOrderTaskMonitor?.procedureMonitorId !== undefined && workOrderTaskMonitor?.procedureMonitorId !== null){
                        targetValue = Number(workOrderTaskMonitor.procedureStepMonitor?.targetValue);
                    }else{
                        targetValue = Number(workOrderTaskMonitor.targetValue);
                    }
                    

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

                            if(workOrderTaskMonitor?.procedureMonitorId !== undefined && workOrderTaskMonitor?.procedureMonitorId !== null){
                                return (workOrderTaskMonitor.procedureStepMonitor.lowTarget <= workOrderTaskMonitor.numVal &&
                                    workOrderTaskMonitor.procedureStepMonitor.highTarget >= workOrderTaskMonitor.numVal);
                            }else{
                                return (workOrderTaskMonitor.lowTarget <= workOrderTaskMonitor.numVal &&
                                    workOrderTaskMonitor.highTarget >= workOrderTaskMonitor.numVal);
                                
                            }
                            
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
