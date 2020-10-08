import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { WidgetModule } from '../../layout/widget/widget.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { TrendModule } from 'ngx-trend';
import { UtilsModule } from '../../layout/utils/utils.module';
import { LiveTileModule } from '../../components/tile/tile.module';
import { MapaelLayersMapModule } from '../../components/mapael/mapael.module';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { FormsModule } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { InputSwitchModule } from 'primeng/inputswitch';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { InputNumberModule } from 'primeng/inputnumber';
import { MultiSelectModule } from 'primeng/multiselect';
import { QuoteCreateComponent } from './quote-create/quote-create.component';
import { ProductDefinitionComponent } from './product-definition/product-definition.component';
import { PurchaseOrdersComponent } from './purchase-orders/purchase-orders.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { SelectButtonModule } from 'primeng/selectbutton';
import { QuotesProductsComponent } from './quotes-products/quotes-products.component';
import {PanelModule} from 'primeng/panel';

export const routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', component: QuotesProductsComponent, pathMatch: 'full' },
  { path: 'quote/create', component: QuoteCreateComponent, pathMatch: 'full' },
  { path: 'product/:mode/:id', component: ProductDefinitionComponent, pathMatch: 'full' },
  { path: 'purchaseorder', component: PurchaseOrdersComponent, pathMatch: 'full' },
];

@NgModule({
  declarations: [
    QuoteCreateComponent,
    ProductDefinitionComponent,
    PurchaseOrdersComponent,
    QuotesProductsComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    WidgetModule,
    ProgressbarModule.forRoot(),
    TrendModule,
    MultiSelectModule,
    BsDropdownModule.forRoot(),
    DropdownModule,
    FormsModule,
    InputSwitchModule,
    TextMaskModule,
    DialogModule,
    TableModule,
    CalendarModule,
    InputNumberModule,
    UtilsModule,
    LiveTileModule,
    WidgetModule,
    MapaelLayersMapModule,
    NewWidgetModule,
    ConfirmDialogModule,
    SelectButtonModule,
    PanelModule,
  ],
  providers: []
})
export class PricingModule {
  static routes = routes;
}
