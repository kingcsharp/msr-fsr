import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../../utils/responseHandler';
import { environment as env } from '../../../../environments/environment';
import { AdminCostSettingsService, AdminCostSettingsModel, UpdateAdminCostSettingRequest } from '../../../services/api.client.generated';
declare let jQuery: any;

@Component({
  selector: 'app-admin-cost-settings',
  templateUrl: './admin-cost-settings.component.html',
  styleUrls: ['./admin-cost-settings.component.scss'],
  providers: [
    AdminCostSettingsService,
  ]
})
export class AdminCostSettingsComponent implements OnInit {
  adminCostSettings: any;
  getAdminCostSettingsFlag: boolean = false;

  constructor(
    public globals: Globals,
    private adminCostSettingsService: AdminCostSettingsService,
  ) { }

  ngOnInit(): void {
    this.getAdminCostSettings();
  }

  getAdminCostSettings() {
    this.globals.showLoader(true);
    this.adminCostSettingsService.adminCostSettingsGet(env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.adminCostSettings = response.object;
        this.getAdminCostSettingsFlag = true;
      }));
  }

  onSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      const RequestData = new UpdateAdminCostSettingRequest({
        rmAnnualRate: parseFloat(this.adminCostSettings.rmAnnualRate),
        laborRateMinute: parseFloat(this.adminCostSettings.laborRateMinute),
        yearsHours: parseInt(this.adminCostSettings.yearsHours),
        hourMinutes: parseInt(this.adminCostSettings.hourMinutes)
      });
      this.globals.showLoader(true);
      this.adminCostSettingsService.adminCostSettingsPatch(env.apiVersion, RequestData)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.adminCostSettings = response.object;
        this.getAdminCostSettingsFlag = true;
      }));
    }
  }

}
