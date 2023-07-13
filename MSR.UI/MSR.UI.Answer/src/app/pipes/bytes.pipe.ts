import { Pipe, PipeTransform } from "@angular/core";
import { formatBytes } from "../../app/models/lib/Utils";

@Pipe({
  name: "byte",
})
export class BytePipe implements PipeTransform {
  constructor() {}

  transform(value: number): string {
    if (value) {
      let convert = formatBytes(value, 2);
      return convert;
    }
  }
}
