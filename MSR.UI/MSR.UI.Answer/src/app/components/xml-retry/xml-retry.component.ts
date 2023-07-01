import { Component, Input } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-xml-retry",
  templateUrl: "./xml-retry.component.html",
  styleUrls: ["./xml-retry.component.scss"],
})
export class XmlRetryComponent {
  @Input() isFailure: boolean;
  @Input() value: string;
  isLoading: boolean = false;

  constructor(private http: HttpClient, private toastr: ToastrService) {}

  retry() {
    this.isLoading = true;
    this.http.get("/retry_xml_submission").subscribe(
      (result: string) => {
        console.log(result);
        this.toastr.success("The file has been sent successfully.");
        this.value = result;
        this.isFailure = false;
        this.isLoading = false;
      },
      (error) => {
        this.toastr.error(
          "The file could not be sent. Please contact Support."
        );
        console.error(error);
        this.isFailure = true;
        this.isLoading = false;
      }
    );
  }
}
