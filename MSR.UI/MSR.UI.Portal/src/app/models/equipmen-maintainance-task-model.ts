import { SelectItem } from 'primeng/api';
import { LocationModel } from '../services/api.client.generated';

export class EquipmentMaintainanceTaskModel {
    barcode: string;
    location: LocationModel;
    troubleState: boolean;
    maintainanceTask: string;
    status: string;
    maintainanceLastCompleted: Date;
    maintainanceFrequency: Date;
    comments: string;
    maintainanceTaskOptions: Array<SelectItem>;
    statusOptions: Array<SelectItem>;
    locationOptions: Array<SelectItem>;
    userOptions: Array<SelectItem>;
    selectedLocation: number;
    selectedUserId: number;
  }
