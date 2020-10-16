import { Routes, RouterModule } from '@angular/router';
import { Layout } from './layout.component';
// noinspection TypeScriptValidateTypes

const routes: Routes = [
  {
    path: '', component: Layout, children: [
      { path: '', redirectTo: 'wip', pathMatch: 'full' },
      { path: 'billing', loadChildren: () => import('../pages/billing/billing.module').then(m => m.BillingModule) },
      { path: 'ui', loadChildren: () => import('../pages/ui-elements/ui-elements.module').then(m => m.UiElementsModule) },
      { path: 'package', loadChildren: () => import('../pages/package/package.module').then(m => m.PackageModule) },
      { path: 'procedures', loadChildren: () =>  import('../pages/procedures/procedures.module').then(m => m.ProceduresModule) },
      { path: 'monitors', loadChildren: () =>  import('../pages/monitors/monitors.module').then(m => m.MonitorsModule) },
      { path: 'pricing', loadChildren: () =>  import('../pages/pricing/pricing.module').then(m => m.PricingModule) },
      { path: 'reporting', loadChildren: () => import('../pages/reports/report.module').then(m => m.ReportModule) },
      { path: 'dashboards', loadChildren: () => import('../pages/dashboards/dashboard.module').then(m => m.DashboardModule) },
      { path: 'wip', loadChildren: () =>  import('../pages/wip/wip.module').then(m => m.WipModule) },
      { path: 'people', loadChildren: () => import('../pages/main/main.module').then(m => m.MainModule) },
    ]
  }
]; 

export const ROUTES = RouterModule.forChild(routes);
