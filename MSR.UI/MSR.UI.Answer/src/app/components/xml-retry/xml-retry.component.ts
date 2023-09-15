import { Component, Input, OnInit } from "@angular/core";
import { environment as env } from '../../../environments/environment';
import { XmlService, XmlTransmissionRequest } from "../../services/api.client.generated";
import { responseHandler } from "../../utils/responseHandler";

@Component({
  selector: "app-xml-retry",
  templateUrl: "./xml-retry.component.html",
  styleUrls: ["./xml-retry.component.scss"],
  providers: [XmlService]
})
export class XmlRetryComponent implements OnInit {
  @Input() id: number;
  isLoading: boolean = false;

  constructor(private xmlService: XmlService) {}

  ngOnInit(): void {}

  retry() {
    this.isLoading = true;
    const request  = {transmissionId: this.id} as XmlTransmissionRequest;
    this.xmlService.transmit(env.apiVersion, request).subscribe(responseHandler(() => {
      this.isLoading = false;
    }, () => {
      // error
      this.isLoading = false;
    }));
  }
}
