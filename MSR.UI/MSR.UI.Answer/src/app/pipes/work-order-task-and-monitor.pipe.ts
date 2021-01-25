import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'workOrderTaskAndMonitor'
})
export class WorkOrderTaskAndMonitorPipe implements PipeTransform {

  transform(primaryValue: string, modelId: number, backupValue: string, isPassOrFail: boolean = false, isYesOrNo: boolean = false): any {

    if (isPassOrFail) {
      if (modelId !== null) {
        return Number(primaryValue) === 1 ? 'Fail' : 'Pass';
      } else {
        return Number(backupValue) === 1 ? 'Fail' : 'Pass';
      }
    }

    if (isYesOrNo) {

      if (modelId !== null) {
        return Number(primaryValue) === 1 ? 'No' : 'Yes';
      } else {
        return Number(backupValue) === 1 ? 'No' : 'Yes';
      }

    }


    if (modelId !== null) {
      return primaryValue;
    } else {
      return backupValue;
    }

  }

}
