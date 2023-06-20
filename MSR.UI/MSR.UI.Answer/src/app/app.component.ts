import { Component, OnInit } from "@angular/core";
import { SignalRService } from "../app/services/signalr.service";
import { Globals } from "./models/lib/globals";
import { Router, NavigationEnd } from "@angular/router";

@Component({
  selector: "app-root",
  template: `<router-outlet></router-outlet>`,
})
export class AppComponent {
  constructor(
    private signalrService: SignalRService,
    private globalService: Globals,
    private router: Router
  ) {}

  ngOnInit() {
    this.signalrService.startConnection();

    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        if (event.url.includes("procedure-view-print")) {
          document.body.classList.add("print");
        } else {
          document.body.classList.remove("print");
        }
      }
    });
  }
}
