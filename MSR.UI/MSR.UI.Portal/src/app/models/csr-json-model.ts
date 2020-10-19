export class CSRJsonModel {
  Id: number;
  customerId: number;
	RequirementName: string;
	Location: string;
	Company: string;
	DivisionFab: string;
	StreetAddress: string;
	CityStateZIP: string;
	CommercialName: string;
	CommercialTitle: string;
	CommercialPhone: string;
	CommercialEmail: string;
	TechnicalName: string;
	TechnicalTitle: string;
	TechnicalPhone: string;
	TechnicalEmail: string;
	PartKitNo: string;
	CustomerSpecifications: string;
	InProcessAnalyticalRequirements: string;
	DetailedDescription: string;
	CriticalToFunction: string;
	SpecificCustomerQualification: string;
	ForecastedVolumes: string;
	SpecialPackagingShipping: string;
	ExpectedQuoteDate: Date;
	ExpectedCycleTime: number;
	PickupNotification: string;
	ShippingMethod: string;
	AdditionalInformation: string;
	SubmittedDate: Date;
	Status: string;
	SubmittedBy: string;
	ShippingMehtodList: string;
	Process: ProcessModel[];
	Parts: PartModel[];
  CustomerRequirementView: string;

  constructor() {
    this.Id = null;
    this.customerId = null;
    this.RequirementName = null;
    this.Location = null;
    this.Company = null;
    this.DivisionFab = null;
    this.StreetAddress = null;
    this.CityStateZIP = null;
    this.CommercialName = null;
    this.CommercialTitle = null;
    this.CommercialPhone = null;
    this.CommercialEmail = null;
    this.TechnicalName = null;
    this.TechnicalTitle = null;
    this.TechnicalPhone = null;
    this.TechnicalEmail = null;
    this.PartKitNo = null;
    this.CustomerSpecifications = null;
    this.InProcessAnalyticalRequirements = null;
    this.DetailedDescription = null;
    this.CriticalToFunction = null;
    this.SpecificCustomerQualification = null;
    this.ForecastedVolumes = null;
    this.SpecialPackagingShipping = null;
    this.ExpectedQuoteDate = null;
    this.ExpectedCycleTime = null;
    this.PickupNotification = null;
    this.ShippingMethod = null;
    this.AdditionalInformation = null;
    this.SubmittedDate = null;
    this.Status = null;
    this.SubmittedBy = null;
    this.ShippingMehtodList = null;
    this.Process = null;
    this.Parts = null;
    this.CustomerRequirementView = null;
  }
}

export class ProcessModel {
  Id: number;
  ObjectId: number;
  Contaminents: string;
  NonCU: boolean;
  Material: string;
  ApproxDimensions: string;
  ExistingProcess: string;

  constructor() {
    this.Id = null;
    this.ObjectId = null;
    this.Contaminents = null;
    this.NonCU = null;
    this.Material = null;
    this.ApproxDimensions = null;
    this.ExistingProcess = null;
  }
}

export class PartModel {
  Id: number;
  ObjectId: number;
  NonCU: boolean;
  PartDescription: string;
  Substrate: string;
  CoatingSurface: string;
  Dimensions: string;
  CustPartNo: string;
  MfgPartNo: string;
  PartsPerKit: string;

  constructor() {
    this.Id = null;
    this.ObjectId = null;
    this.NonCU = null;
    this.PartDescription = null;
    this.Substrate = null;
    this.CoatingSurface = null;
    this.Dimensions = null;
    this.CustPartNo = null;
    this.MfgPartNo = null;
    this.PartsPerKit = null;
  }
}
