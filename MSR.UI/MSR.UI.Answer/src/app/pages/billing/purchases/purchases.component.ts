import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import {
  EnumMenuItem,
  PurchaseService,
  PurchaseModel,
} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../../utils/responseHandler';

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
  display: boolean =  false;
  purchaseOrderStatus: any[];

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private purchaseService: PurchaseService,
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'quotesGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'purchaseNumber', label: 'Purchase Number', visible: true }),
      new ColumnsSaved({ id: 'custRefNumber', label: 'Cust Ref Number', visible: true }),
      new ColumnsSaved({ id: 'orderDescription', label: 'Order Description', visible: true }),
      new ColumnsSaved({ id: 'purchaseStatus', label: 'Purchase Status', visible: true }),
      new ColumnsSaved({ id: 'createdDate', label: 'Created Date', visible: true })
    ];
    this.purchasePrivileges = this.globals.getEnumPrivileges(this.menuItems.Purchases);
    this.purchaseOrderPrivileges = this.globals.getEnumPrivileges(this.menuItems.PurchaseOrders);
    this.getPurchases();
    this.currentPurchase = null;
  }

  getPurchases() {
    this.globals.showLoader(true);
    this.purchaseService.purchaseGet(null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;

        this.purchaseOrderStatus = this.data.filter(
          (thing, i, arr) => arr.findIndex(t => t.statusId === thing.statusId) === i
        ).map(x => ({ label: x.status.name, value: x.status.id }));
      }));
  }

  onClickViewPurchase(purchase: PurchaseModel) {
    //TODO: view purchase detail
    this.currentPurchase = purchase;
    this.display = true;
  }

  onClickCloseDialog() {
    this.display = false;
    this.currentPurchase = null;
  }
}
