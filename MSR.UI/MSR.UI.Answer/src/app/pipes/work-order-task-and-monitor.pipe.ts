import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'workOrderTaskAndMonitor'
})
export class WorkOrderTaskAndMonitorPipe implements PipeTransform {

  transform(primaryValue: string, modelId: number, backupValue: string, isPassOrFail: boolean = false, isYesOrNo: boolean = false): any {

    if (isPassOrFail) {

      return this.getValueByModelId(modelId, Number(primaryValue), Number(backupValue)) === 0 ? 'Fail' : 'Pass';

    }

    if (isYesOrNo) {

      return this.getValueByModelId(modelId, Number(primaryValue), Number(backupValue)) === 0 ? 'No' : 'Yes';

    }


    return this.getValueByModelId(modelId, primaryValue, backupValue);
  }

  getValueByModelId(modelId, primaryValue, backupValue) {
    if (modelId !== null && modelId !== undefined) {
      return primaryValue;
    } else {
      return backupValue;
    }
  }

}
