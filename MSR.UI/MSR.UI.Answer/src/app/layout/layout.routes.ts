import { Routes, RouterModule }  from '@angular/router';
import { Layout } from './layout.component';
// noinspection TypeScriptValidateTypes
const routes: Routes = [
  { path: '', component: Layout, children: [
      { path: '', redirectTo: 'main', pathMatch: 'full' },
      { path: 'main', loadChildren: () => import('../pages/main/main.module').then(m => m.MainModule) },
      {path: 'inbox', loadChildren: () => import('../pages/inbox/inbox.module').then(m => m.InboxModule)},
      {path: 'charts', loadChildren: () => import('../pages/charts/charts.module').then(m => m.ChartsModule)},
      {path: 'profile', loadChildren: () => import('../pages/profile/profile.module').then(m => m.ProfileModule)},
      {path: 'ecommerce', loadChildren: () => import('../pages/ecommerce/ecommerce.module').then(m => m.EcommerceModule)},
      {path: 'core', loadChildren: () => import('../pages/core/core-elements.module').then(m => m.CoreElementsModule)},
      {path: 'forms', loadChildren: () => import('../pages/forms/forms.module').then(m => m.FormModule)},
      {path: 'ui', loadChildren: () => import('../pages/ui-elements/ui-elements.module').then(m => m.UiElementsModule)},
      {path: 'extra', loadChildren: () => import('../pages/extra/extra.module').then(m => m.ExtraModule)},
      {path: 'tables', loadChildren: () => import('../pages/tables/tables.module').then(m => m.TablesModule)},
      {path: 'grid', loadChildren: () => import('../pages/grid/grid.module').then(m => m.GridModule)},
      {path: 'package', loadChildren: () => import('../pages/package/package.module').then(m => m.PackageModule)}
  ]}
];

export const ROUTES = RouterModule.forChild(routes);
