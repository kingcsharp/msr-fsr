import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { callFunctionWithFilters } from '../../../models/lib/Utils';
import {
  EnumMenuItem, PurchaseService, PurchaseModel,
} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../../utils/responseHandler';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.scss'],
  providers: [PurchaseService]
})
export class PurchasesComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  gridVersion: string;
  data: PurchaseModel[] = [];
  currentPurchase: PurchaseModel;
  purchasePrivileges: AllowedActions;
  purchaseOrderPrivileges: AllowedActions;
  display: boolean = false;
  purchaseOrderStatus: any[];
  totalRecords: number = 0;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private purchaseService: PurchaseService,
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'purchaseGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Purchase Number', visible: true }),
      new ColumnsSaved({ id: 'mttn', label: 'Cust Ref Number', visible: true }),
      new ColumnsSaved({ id: 'purchaseOrderProduct.name', label: 'Order Description', visible: true }),
      new ColumnsSaved({ id: 'statusId', label: 'Purchase Status', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created Date', visible: true })
    ];
    this.purchasePrivileges = this.globals.getEnumPrivileges(this.menuItems.Purchases);
    this.purchaseOrderPrivileges = this.globals.getEnumPrivileges(this.menuItems.PurchaseOrders);
    this.currentPurchase = null;
    this.purchaseOrderStatus = this.globals.getTopLevelStatus();
  }

  getPurchases(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      callFunctionWithFilters(this.purchaseService, this.purchaseService.purchaseGet, event, this.globals.functionDic)
        .pipe(take(1))
        .subscribe(responseHandler(response => {
          this.data = response.object;
          this.totalRecords = response.totalNumberOfRecords;
        }));
    }, 10);
  }

  onClickViewPurchase(purchase: PurchaseModel) {
    this.currentPurchase = purchase;
    this.display = true;
  }

  onClickCloseDialog() {
    this.display = false;
    this.currentPurchase = null;
  }
}
