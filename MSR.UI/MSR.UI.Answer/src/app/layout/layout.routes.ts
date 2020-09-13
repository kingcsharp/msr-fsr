import { Routes, RouterModule } from '@angular/router';
import { Layout } from './layout.component';
// noinspection TypeScriptValidateTypes

const routes: Routes = [
  {
    path: '', component: Layout, children: [
      { path: '', redirectTo: 'people', pathMatch: 'full' },
      { path: 'people', loadChildren: () => import('../pages/main/main.module').then(m => m.MainModule) },
      { path: 'workflow', loadChildren: () => import('../pages/workflow/workflow.module').then(m => m.WorkflowModule) },
      { path: 'parts', loadChildren: () => import('../pages/parts/parts.module').then(m => m.PartsModule) },
      { path: 'billing', loadChildren: () => import('../pages/billing/billing.module').then(m => m.BillingModule) },
      { path: 'inbox', loadChildren: () => import('../pages/inbox/inbox.module').then(m => m.InboxModule) },
      { path: 'ecommerce', loadChildren: () => import('../pages/ecommerce/ecommerce.module').then(m => m.EcommerceModule) },
      { path: 'core', loadChildren: () => import('../pages/core/core-elements.module').then(m => m.CoreElementsModule) },
      { path: 'forms', loadChildren: () => import('../pages/forms/forms.module').then(m => m.FormModule) },
      { path: 'ui', loadChildren: () => import('../pages/ui-elements/ui-elements.module').then(m => m.UiElementsModule) },
      { path: 'extra', loadChildren: () => import('../pages/extra/extra.module').then(m => m.ExtraModule) },
      { path: 'tables', loadChildren: () => import('../pages/tables/tables.module').then(m => m.TablesModule) },
      { path: 'package', loadChildren: () => import('../pages/package/package.module').then(m => m.PackageModule) },
      { path: 'help', loadChildren: () =>  import('../pages/help/help.module').then(m => m.HelpModule) },
      { path: 'locations', loadChildren: () =>  import('../pages/locations/locations.module').then(m => m.LocationsModule) },
      { path: 'procedures', loadChildren: () =>  import('../pages/procedures/procedures.module').then(m => m.ProceduresModule) },
      { path: 'monitors', loadChildren: () =>  import('../pages/monitors/monitors.module').then(m => m.MonitorsModule) },
      { path: 'pricing', loadChildren: () =>  import('../pages/pricing/pricing.module').then(m => m.PricingModule) },
      { path: 'utilities', loadChildren: () =>  import('../pages/utilities/utilities.module').then(m => m.UtilitiesModule) },
    ]
  }
];

export const ROUTES = RouterModule.forChild(routes);
