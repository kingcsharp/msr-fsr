import { Procedure, FileRequest, ProcedureStepTemplateModel } from './api.client.generated';

export class ProcedureModel extends Procedure {
    referenceFiles?: FileRequest[] | undefined;
}

export class ProcedureStepTemplate extends ProcedureStepTemplateModel {
    referenceFiles?: FileRequest[] | undefined;
}
