import { Injectable } from "@angular/core";
import { EnumSegregationType } from "./api.client.generated";

@Injectable({
  providedIn: "root",
})
export class ProductSegregationService {
  private segregationType: EnumSegregationType = EnumSegregationType.NONCU;
  private wipDetailsBeingDisplayed: boolean = false;
  private partTitle: string;

  constructor() {}

  get SegregationType() {
    return this.segregationType;
  }

  set SegregationType(segregationType: EnumSegregationType) {
    this.segregationType = segregationType;
  }

  get PartTitle() {
    return this.partTitle;
  }

  set PartTitle(partTitle: string) {
    this.partTitle = partTitle;
  }

  get WipDetailsBeingDisplayed() {
    return this.wipDetailsBeingDisplayed;
  }

  set WipDetailsBeingDisplayed(wipDetailsBeingDisplayed: boolean) {
    this.wipDetailsBeingDisplayed = wipDetailsBeingDisplayed;
  }
}
