import { Injectable } from '@angular/core';
import { CreateProcedureTypeRequest } from '../models/createProcedureTypeRequest';
import { UpdateProcedureTypeRequest } from '../models/updateProcedureTypeRequest';
import { ProcedureTypeMock } from '../models/ProcedureTypeMock';
import { AuditActionResult } from '../../api.client.generated';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { HttpResponse } from '@angular/common/http';

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
}