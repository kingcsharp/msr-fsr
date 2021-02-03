import { Pipe, PipeTransform } from '@angular/core';
import { EnumSegregationType } from '../services/api.client.generated';

@Pipe({
  name: 'segregationType'
})
export class SegregationTypePipe implements PipeTransform {

  transform(segregationType: EnumSegregationType): string {
    console.log(segregationType);
    switch(Number(segregationType)) { 
      case EnumSegregationType.CU: { 
         return 'CU';
      } 
      case EnumSegregationType.NONCU: { 
         return 'Non-Cu';
      } 
      case EnumSegregationType.DESEG: { 
        return 'Deseg';
     } 
      default: { 
         return 'N/A';
      } 
   } 
  }

}
