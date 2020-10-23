import { Routes, RouterModule } from '@angular/router';
import { Layout } from './layout.component';
// noinspection TypeScriptValidateTypes

const routes: Routes = [
  {
    path: '', component: Layout, children: [
      { path: '', redirectTo: 'wip', pathMatch: 'full' },
      { path: 'ui', loadChildren: () => import('../pages/ui-elements/ui-elements.module').then(m => m.UiElementsModule) },
      { path: 'package', loadChildren: () => import('../pages/package/package.module').then(m => m.PackageModule) },
      { path: 'reporting', loadChildren: () => import('../pages/reports/report.module').then(m => m.ReportModule) },
      { path: 'wip', loadChildren: () =>  import('../pages/wip/wip.module').then(m => m.WipModule) },
      { path: 'people', loadChildren: () => import('../pages/main/main.module').then(m => m.MainModule) },
    ]
  }
];

export const ROUTES = RouterModule.forChild(routes);
