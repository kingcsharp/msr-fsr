import { Injectable } from '@angular/core';
import { CreateProcedureTypeRequest } from '../models/createProcedureTypeRequest';
import { UpdateProcedureTypeRequest } from '../models/updateProcedureTypeRequest';
import { ProcedureTypeMock } from '../models/ProcedureTypeMock';
import { ProcedureTemplate } from '../models/procedureTemplate'
import { CreateProcedureTemplateRequest } from '../models/createProcedureTemplateRequest';
import { UpdateProcedureTemplateRequest } from '../models/updateProcedureTemplateRequest';
import { Procedure } from '../models/procedure';
import { Role } from '../models/role';

@Injectable()
export class MockServices {
    constructor() { }

    procedureTypesPost(createProcedureTypeRequest: CreateProcedureTypeRequest) {

        alert("Success create placeholder for Web API");
    }

    procedureTypesPatch(updateProcedureTypeRequest: UpdateProcedureTypeRequest) {

        alert("Success update placeholder for Web API");

    }

    procedureTypesDelete(id: number) {

        alert("Success delete placeholder for Web API with id: " + id);

    }

    procedureTypesGet(id: number | null | undefined) {

        let procedureTypes = new Array<ProcedureTypeMock>()

        if (id !== null && id !== undefined) {

            let procedureType = new ProcedureTypeMock();
            procedureType.id = id;
            procedureType.name = 'Loreum Ipsum';
            procedureType.type = 'Setup';

            procedureTypes.push(procedureType);

        } else {

            for (let index = 1; index < 75; index++) {

                let procedureType = new ProcedureTypeMock();
                procedureType.id = index;
                procedureType.name = 'Name ' + index;
                procedureType.type = 'Operate';
                procedureType.revision = index;
                procedureType.status = 'Approved';
                procedureTypes.push(procedureType);

            }

        }

        return procedureTypes;
    }

    procedureTemplatePost(createProcedureTemplateRequest: CreateProcedureTemplateRequest) {

        alert("Success create placeholder for Web API");

    }

    procedureTemplatePatch(updateProcedureTemplateRequest: UpdateProcedureTemplateRequest) {

        alert("Success update placeholder for Web API");

    }

    procedureTemplateDelete(id: number) {

        alert("Success delete placeholder for Web API with id: " + id);

    }

    procedureTemplateGet(id: number | null | undefined) {

        let procedureTemplates = new Array<ProcedureTemplate>()

        if (id !== null && id !== undefined) {

            let procedureTemplate = new ProcedureTemplate();
            procedureTemplate.isRelatedToAProduct = false;
            procedureTemplate.revision = 1;
            procedureTemplate.status = 'Approved';
            procedureTemplate.text = 'Loreum Ipsum dum';
            procedureTemplate.title = 'Procedure Template ' + id;
            procedureTemplate.procedureStepTypeId = 1;
            procedureTemplate.baseStartOnCounter = false;
            procedureTemplate.comments = 'Loreum Ipsum dum itum lom si nam';
            procedureTemplate.estimatedStepDuration = 4;
            procedureTemplate.numberOfQuestionsToUse = 5;
            procedureTemplate.procedureStepId = undefined;
            procedureTemplate.procedureStepTypeId = 2;
            procedureTemplate.referenceDocuments = undefined;
            procedureTemplate.referenceFiles = new Array();
            procedureTemplate.referenceProcedures = [1,3,5,2];
            procedureTemplate.replacementCost = 22;
            procedureTemplate.roles = this.roleGet().slice(0, 3);
            procedureTemplate.text = 'Some random sample text';
            procedureTemplate.title = 'Sample Procedure Step';
            procedureTemplate.usefulLife = 4;
            procedureTemplate.utilization = 87.99;
            procedureTemplates.push(procedureTemplate);

        } else {

            for (let index = 1; index < 75; index++) {

                let procedureTemplate = new ProcedureTemplate();
                procedureTemplate.id = index;
                procedureTemplate.isRelatedToAProduct = false;
                procedureTemplate.revision = index;
                procedureTemplate.status = 'Approved';
                procedureTemplate.text = 'Loreum Ipsum dum' + index;
                procedureTemplate.title = 'Procedure Template ' + index;
                procedureTemplates.push(procedureTemplate);

            }

        }

        return procedureTemplates;

    }

    procedureGet(id: number | null | undefined) {

        let procedures = new Array<Procedure>()

        if (id !== null && id !== undefined) {

            let procedure = new Procedure();
            procedure.id = id;
            procedure.comment = 'Loreum Ipsum';
            procedure.createdByDepartmentName = 'Department Test Name';
            procedure.creatorCompany = 'Name of Creator Company';
            procedure.duration = 3;
            procedure.durationType = 'SYS_MINUTES';
            procedure.isRelatedToAProduct = true;
            procedure.name = 'Procedure Step A';
            procedure.procedureType = this.procedureTypesGet(2)[0];
            procedure.referenceFiles = new Array<any>();;
            procedure.revision = 1;
            procedure.roles = this.roleGet().slice(0, 5);
            procedures.push(procedure);

        } else {

            for (let index = 1; index < 75; index++) {

                let procedure = new Procedure();
                procedure.id = index;
                procedure.comment = 'Loreum Ipsum' + index;
                procedure.createdByDepartmentName = 'Department Test ' + index +' Name';
                procedure.creatorCompany = 'Name of Creator Company';
                procedure.duration = 3;
                procedure.durationType = 'SYS_MINUTES';
                procedure.isRelatedToAProduct = true;
                procedure.name = 'Procedure Step A'+ index;
                procedure.procedureType = this.procedureTypesGet(2)[0];
                procedure.referenceFiles = new Array<any>();
                procedure.revision = index;
                procedure.roles = this.roleGet().slice(0, 3);

                procedures.push(procedure);

            }

        }

        return procedures;

    }

    roleGet() {

        let roles = new Array<Role>();

        for (let index = 1; index < 75; index++) {

            let role = new Role();
            role.id = index;
            role.isCertificationRole = false;
            role.name = 'Loreum Ipsum Role ' + index;

            roles.push(role);
        }

        return roles;

    }
}