import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { QuotesComponent } from './quotes/quotes.component';
import { WidgetModule } from '../../layout/widget/widget.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TrendModule } from 'ngx-trend';
import { UtilsModule } from '../../layout/utils/utils.module';
import { RickshawChartModule } from '../../components/rickshaw/rickshaw.module';
import { LiveTileModule } from '../../components/tile/tile.module';
import { FlotChartModule } from '../../components/flot/flot.module';
import { JqSparklineModule } from '../../components/sparkline/sparkline.module';
import { MapaelLayersMapModule } from '../../components/mapael/mapael.module';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { FormsModule } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { InputSwitchModule } from 'primeng/inputswitch';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
import { QuoteCreateComponent } from './quote-create/quote-create.component';
import { ProductDefinitionComponent } from './product-definition/product-definition.component';

export const routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', component:QuotesComponent, pathMatch: 'full' },
  { path: 'quote-create', component: QuoteCreateComponent, pathMatch: 'full'},
  { path: 'product-edit', component: ProductDefinitionComponent, pathMatch: 'full'},
  { path: 'product-view', component: ProductDefinitionComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [QuotesComponent, QuoteCreateComponent, ProductDefinitionComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    WidgetModule,
    ProgressbarModule.forRoot(),
    TrendModule,
    CheckboxModule,
    MultiSelectModule,
    BsDropdownModule.forRoot(),
    DropdownModule,
    TooltipModule.forRoot(),
    FormsModule,
    InputSwitchModule,
    TextMaskModule,
    DialogModule,
    TableModule,
    CalendarModule,
    UtilsModule,
    RickshawChartModule,
    LiveTileModule,
    WidgetModule,
    FlotChartModule,
    RickshawChartModule,
    JqSparklineModule,
    MapaelLayersMapModule,
    NewWidgetModule
  ],
  providers: []
})
export class PricingModule {
  static routes = routes;
}
