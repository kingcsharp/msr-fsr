import { Component, Input, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment as env } from '../../../environments/environment';
import { ToastrService } from "ngx-toastr";
import { XmlService, XmlTransmissionRequest } from "../../services/api.client.generated";
import { responseHandler } from "../../utils/responseHandler";

@Component({
  selector: "app-xml-retry",
  templateUrl: "./xml-retry.component.html",
  styleUrls: ["./xml-retry.component.scss"],
  providers: [XmlService]
})
export class XmlRetryComponent implements OnInit {
  @Input() isFailure: boolean;
  @Input() value: string;
  @Input() id: number;
  isLoading: boolean = false;

  constructor(private http: HttpClient, private toastr: ToastrService, private xmlService: XmlService) {}

  ngOnInit(): void {}

  retry() {
    this.isLoading = true;
    const request  = {transmissionId: this.id} as XmlTransmissionRequest;
    this.xmlService.transmit(env.apiVersion, request).subscribe(responseHandler((response) => {
      console.log('-------', response)
    }))
    // this.http.get("/retry_xml_submission").subscribe(
    //   (result: string) => {
    //     console.log(result);
    //     this.toastr.success("The file has been sent successfully.");
    //     this.value = result;
    //     this.isFailure = false;
    //     this.isLoading = false;
    //   },
    //   (error) => {
    //     this.toastr.error(
    //       "The file could not be sent. Please contact Support."
    //     );
    //     console.error(error);
    //     this.isFailure = true;
    //     this.isLoading = false;
    //   }
    // );
  }
}
