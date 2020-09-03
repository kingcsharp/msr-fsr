import { Procedure, FileRequest, ProcedureStepTemplateModel } from './api.client.generated';

export class ProcedureStepTemplate extends ProcedureStepTemplateModel {
    referenceFiles?: FileRequest[] | undefined;
}
