import { Injectable } from '@angular/core';
import { EnumSegregationType } from './api.client.generated';

@Injectable({
  providedIn: 'root'
})
export class ProductSegregationService {

  private segregationType: EnumSegregationType = EnumSegregationType.NONCU;
  private partTitle: string;

  constructor() { }

  get SegregationType(){
    return this.segregationType;
  }

  set SegregationType(segregationType: EnumSegregationType){
    this.segregationType = segregationType;
  }

  get PartTitle(){
    return this.partTitle;
  }

  set PartTitle(partTitle: string){
    this.partTitle = partTitle;
  }
}
