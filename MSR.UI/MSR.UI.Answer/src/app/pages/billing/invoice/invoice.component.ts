import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  InvoiceService, InvoiceModel, InvoiceItemModel, EnumApprovalTables, AuditActionResultOfPartModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Component({
  selector: 'invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss']
})
export class InvoiceComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  approvalTables = EnumApprovalTables;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  canCreate: boolean = false;
  canActivateStages: boolean = false;
  canEditStages: boolean = false;
  display: boolean = false;
  currPart: any;
  data: any;
  isKitStatus: any[];
  workflowGroups: any[] = [];
  getWorkflowGroupsDone: boolean = false;
  allParts: any[] = [];
  isActive: any[];
  uploadedFinished: boolean = false;
  showApproveButtons: boolean = true;

  constructor(private globals: Globals, private invoiceService: InvoiceService, public cg: CommonGrid,
    private elem: ElementRef, private toastr: ToastrService) {

  }

  ngOnInit(): void {
    // this.currPart = this.getInvoice(undefined);
    this.gridStorageId = 'invoiceGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    // new ColumnsSaved({ id: 'customerName', label: 'Customer Name', visible: true }),
    new ColumnsSaved({ id: 'description', label: 'Description', visible: true }),
    new ColumnsSaved({ id: 'invoiceNumber', label: 'invoiceNumber', visible: true }),
    new ColumnsSaved({ id: 'total', label: 'Amount', visible: true }),
    new ColumnsSaved({ id: 'invoiceDate', label: 'Due Date', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    // new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    // new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];
    this.isKitStatus = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];
    this.isActive = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];

    this.canCreate = this.hasPrivilege(this.privileges.CanCreate);
    this.canActivateStages = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditStages = this.hasPrivilege(this.privileges.CanEdit);
    this.getInvoices();
    this.data = [];
  }

  getInvoices() {
    this.globals.showLoader(true);
    this.invoiceService.invoiceGet(null, null, null, null, null, null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
      }));
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(this.menuItems.Invoices, privName);
  }

}
