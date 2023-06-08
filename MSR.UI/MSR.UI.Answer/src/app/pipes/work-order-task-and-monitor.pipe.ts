import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "workOrderTaskAndMonitor",
})
export class WorkOrderTaskAndMonitorPipe implements PipeTransform {
  monitorTypes: Array<any> = new Array<any>();

  transform(
    primaryValue: string,
    modelId: number,
    backupValue: string,
    isPassOrFail: boolean = false,
    isYesOrNo: boolean = false,
    isMonitorType: boolean = false
  ): any {
    if (isPassOrFail) {
      return this.getValueByModelId(
        modelId,
        Number(primaryValue),
        Number(backupValue)
      ) === 0
        ? "Fail"
        : "Pass";
    }

    if (isYesOrNo) {
      return this.getValueByModelId(
        modelId,
        Number(primaryValue),
        Number(backupValue)
      ) === 0
        ? "No"
        : "Yes";
    }

    if (isMonitorType) {
      const monitorTypeId = this.getValueByModelId(
        modelId,
        primaryValue,
        backupValue
      );

      this.monitorTypes = [
        { label: "Equipment", value: 1 },
        { label: "Number", value: 2 },
        { label: "Yes or No", value: 3 },
        { label: "Text", value: 4 },
        { label: "Pass or Fail", value: 5 },
        { label: "Select", value: 6 },
      ];
      return this.monitorTypes.find((t) => t.value === monitorTypeId).label;
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
