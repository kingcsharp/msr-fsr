import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LandingPageComponent } from "./landing.component";

export const routes: Routes = [
  { path: "", redirectTo: "landingPage", pathMatch: "full" },
  { path: "landingPage", component: LandingPageComponent, pathMatch: "full" },
];

@NgModule({
  declarations: [LandingPageComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class LandingPageModule {
  static routes = routes;
}
