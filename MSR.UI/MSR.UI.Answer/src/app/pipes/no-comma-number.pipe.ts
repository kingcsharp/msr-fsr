import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'noCommaNumber'
})
export class NoCommaNumberPipe implements PipeTransform {

  transform(value: number): number {
    if (value !== undefined && value !== null) {
      const numberToMultiply = value.toString().replace(/,/g, "");
      return Number(numberToMultiply) * 100;
    } else {
      return 0;
    }
  }

}
