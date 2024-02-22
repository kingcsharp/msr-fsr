import { WorkOrderTaskMonitorModel } from "../services/api.client.generated";

export class DataLabel {
  TaskDescription: string;
  MonitorName: string;
  PartNumber: string;
  CustomerPurchaseNumber: string;
  PartName: string;
  Requestee: string;
  Date: Date;
  workOrderTaskMonitors?: WorkOrderTaskMonitorModel[]
}
