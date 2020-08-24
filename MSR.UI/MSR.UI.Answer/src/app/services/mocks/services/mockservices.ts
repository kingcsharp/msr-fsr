import { Injectable, ModuleWithComponentFactories } from '@angular/core';
import { CreateProcedureTypeRequest } from '../models/createProcedureTypeRequest';
import { UpdateProcedureTypeRequest } from '../models/updateProcedureTypeRequest';
import { ProcedureTypeMock } from '../models/ProcedureTypeMock';
import { ProcedureTemplate } from '../models/procedureTemplate'
import { CreateProcedureTemplateRequest } from '../models/createProcedureTemplateRequest';
import { UpdateProcedureTemplateRequest } from '../models/updateProcedureTemplateRequest';
import { Procedure } from '../models/procedure';
import { Role } from '../models/role';
import { Monitor } from '../models/monitor';
import { CreateProcedureRequest } from '../models/createProcedureRequest';
import { UpdateProcedureRequest } from '../models/updateProcedureRequest';
import { ProcedureStep } from '../models/procedureStep';
import { CreateProcedureStepMonitorRequest } from '../models/createProcedureStepMonitorRequest';
import { UpdateProcedureStepMonitorRequest } from '../models/updateProcedureStepMonitorRequest';
import { ProcedureStepMonitor } from '../models/procedureStepMonitor';

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
            procedureTemplate.baseStartOnCounter = false;
            procedureTemplate.comments = 'Loreum Ipsum dum itum lom si nam';
            procedureTemplate.estimatedStepDuration = 4;
            procedureTemplate.numberOfQuestionsToUse = 5;
            procedureTemplate.procedureStepId = undefined;
            procedureTemplate.procedureStepTypeId = 2;
            procedureTemplate.referenceDocuments = undefined;
            procedureTemplate.referenceFiles = [{
                "fileId": 197,
                "entityId": 8965,
                "name": "aaa.docx",
                "base64String": null,
                "fileContents": null,
                "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "fileURL": ""
            },
            {
                "fileId": 198,
                "entityId": 8965,
                "name": "lavarropasS.pdf",
                "base64String": null,
                "fileContents": null,
                "contentType": "application/pdf",
                "fileURL": ""
            },
            {
                "fileId": 199,
                "entityId": 8965,
                "name": "xlsAlecTest.xlsx",
                "base64String": null,
                "fileContents": null,
                "contentType": "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "fileURL": ""
            },
            {
                "fileId": 200,
                "entityId": 8965,
                "name": "alec.jpg",
                "base64String": null,
                "fileContents": null,
                "contentType": "image/jpeg",
                "fileURL": ""
            },
            {
                "fileId": 201,
                "entityId": 8965,
                "name": "cmh.PNG",
                "base64String": null,
                "fileContents": null,
                "contentType": "image/png",
                "fileURL": ""
            },
            {
                "fileId": 202,
                "entityId": 8965,
                "name": "casos practico.pptx",
                "base64String": null,
                "fileContents": null,
                "contentType": "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                "fileURL": ""
            },
            {
                "fileId": 203,
                "entityId": 8965,
                "name": "aa.docx",
                "base64String": null,
                "fileContents": null,
                "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "fileURL": ""
            },
            {
                "fileId": 204,
                "entityId": 8965,
                "name": "TimeZoneShit.PNG",
                "base64String": null,
                "fileContents": null,
                "contentType": "image/png",
                "fileURL": ""
            }];
            procedureTemplate.referenceProcedures = [1, 3, 5, 2];
            procedureTemplate.replacementCost = 22;
            procedureTemplate.roles = this.roleGet().slice(0, 3);
            procedureTemplate.usefulLife = 4;
            procedureTemplate.utilization = 87.99;
            procedureTemplates.push(procedureTemplate);

        } else {

            for (let index = 1; index < 75; index++) {

                let procedureTemplate = new ProcedureTemplate();
                procedureTemplate.id = index;
                procedureTemplate.isRelatedToAProduct = false;
                procedureTemplate.revision = 1;
                procedureTemplate.status = 'Approved';
                procedureTemplate.text = 'Loreum Ipsum dum' + index;
                procedureTemplate.title = 'Procedure Template ' + index;
                procedureTemplate.procedureStepTypeId = 1;
                procedureTemplate.baseStartOnCounter = false;
                procedureTemplate.comments = 'Loreum Ipsum dum itum lom si nam';
                procedureTemplate.estimatedStepDuration = 4;
                procedureTemplate.numberOfQuestionsToUse = 5;
                procedureTemplate.procedureStepId = undefined;
                procedureTemplate.procedureStepTypeId = 2;
                procedureTemplate.referenceDocuments = undefined;
                procedureTemplate.referenceFiles = [{
                    "fileId": 197,
                    "entityId": 8965,
                    "name": "aaa.docx",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "fileURL": ""
                },
                {
                    "fileId": 198,
                    "entityId": 8965,
                    "name": "lavarropasS.pdf",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/pdf",
                    "fileURL": ""
                },
                {
                    "fileId": 199,
                    "entityId": 8965,
                    "name": "xlsAlecTest.xlsx",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "fileURL": ""
                },
                {
                    "fileId": 200,
                    "entityId": 8965,
                    "name": "alec.jpg",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "image/jpeg",
                    "fileURL": ""
                },
                {
                    "fileId": 201,
                    "entityId": 8965,
                    "name": "cmh.PNG",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "image/png",
                    "fileURL": ""
                },
                {
                    "fileId": 202,
                    "entityId": 8965,
                    "name": "casos practico.pptx",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                    "fileURL": ""
                },
                {
                    "fileId": 203,
                    "entityId": 8965,
                    "name": "aa.docx",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "fileURL": ""
                },
                {
                    "fileId": 204,
                    "entityId": 8965,
                    "name": "TimeZoneShit.PNG",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "image/png",
                    "fileURL": ""
                }];
                procedureTemplate.referenceProcedures = [1, 3, 5, 2];
                procedureTemplate.replacementCost = 22;
                procedureTemplate.roles = this.roleGet().slice(0, 3);
                procedureTemplate.usefulLife = 4;
                procedureTemplate.utilization = 87.99;
                procedureTemplates.push(procedureTemplate);

            }

        }

        return procedureTemplates;

    }

    procedurePost(createProcedureRequest: CreateProcedureRequest) {

        alert("Success create placeholder for Web API");

    }

    procedurePatch(updateProcedureRequest: UpdateProcedureRequest) {

        alert("Success update placeholder for Web API");

    }

    procedureDelete(id: number) {

        alert("Success delete placeholder for Web API with id: " + id);

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
            procedure.referenceFiles = [{
                "fileId": 197,
                "entityId": 8965,
                "name": "aaa.docx",
                "base64String": null,
                "fileContents": null,
                "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "fileURL": ""
            },
            {
                "fileId": 198,
                "entityId": 8965,
                "name": "lavarropasS.pdf",
                "base64String": null,
                "fileContents": null,
                "contentType": "application/pdf",
                "fileURL": ""
            },
            {
                "fileId": 199,
                "entityId": 8965,
                "name": "xlsAlecTest.xlsx",
                "base64String": null,
                "fileContents": null,
                "contentType": "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "fileURL": ""
            },
            {
                "fileId": 200,
                "entityId": 8965,
                "name": "alec.jpg",
                "base64String": null,
                "fileContents": null,
                "contentType": "image/jpeg",
                "fileURL": ""
            },
            {
                "fileId": 201,
                "entityId": 8965,
                "name": "cmh.PNG",
                "base64String": null,
                "fileContents": null,
                "contentType": "image/png",
                "fileURL": ""
            }];
            procedure.revision = 1;
            procedure.roles = this.roleGet().slice(0, 5);
            procedures.push(procedure);

        } else {

            for (let index = 1; index < 75; index++) {

                let procedure = new Procedure();
                procedure.id = index;
                procedure.comment = 'Loreum Ipsum' + index;
                procedure.createdByDepartmentName = 'Department Test ' + index + ' Name';
                procedure.creatorCompany = 'Name of Creator Company';
                procedure.duration = 3;
                procedure.durationType = 'SYS_MINUTES';
                procedure.isRelatedToAProduct = true;
                procedure.name = 'Procedure Step A' + index;
                procedure.procedureType = this.procedureTypesGet(2)[0];
                procedure.referenceFiles = [{
                    "fileId": 197,
                    "entityId": 8965,
                    "name": "aaa.docx",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "fileURL": ""
                },
                {
                    "fileId": 198,
                    "entityId": 8965,
                    "name": "lavarropasS.pdf",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/pdf",
                    "fileURL": ""
                },
                {
                    "fileId": 199,
                    "entityId": 8965,
                    "name": "xlsAlecTest.xlsx",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "fileURL": ""
                },
                {
                    "fileId": 200,
                    "entityId": 8965,
                    "name": "alec.jpg",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "image/jpeg",
                    "fileURL": ""
                },
                {
                    "fileId": 201,
                    "entityId": 8965,
                    "name": "cmh.PNG",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "image/png",
                    "fileURL": ""
                },
                {
                    "fileId": 202,
                    "entityId": 8965,
                    "name": "casos practico.pptx",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                    "fileURL": ""
                },
                {
                    "fileId": 203,
                    "entityId": 8965,
                    "name": "aa.docx",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "fileURL": ""
                },
                {
                    "fileId": 204,
                    "entityId": 8965,
                    "name": "TimeZoneShit.PNG",
                    "base64String": null,
                    "fileContents": null,
                    "contentType": "image/png",
                    "fileURL": ""
                }];
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

    monitorsPost(createProcedureStepMonitorRequest: CreateProcedureStepMonitorRequest){

        alert("Success create placeholder for Web API");

    }

    monitorsPatch(updateProcedureStepMonitorRequest: UpdateProcedureStepMonitorRequest){

        alert("Success update placeholder for Web API");
        
    }
    
    monitorsDelete(id: number | null | undefined){

        alert("Success delete placeholder for Web API with id: " + id);

    }

    monitorsGet(id: number | null | undefined) {

        let monitors = new Array<Monitor>()

        if (id !== null && id !== undefined) {

            let monitor = new Monitor();
            monitor.id = id;
            monitor.description = 'Loreum Ipsum desca' + id;
            monitor.monitorType = 'Monitor Type' + id;
            monitor.passing = true;
            monitor.result = 'Sample Result text';
            monitor.serialNumber = id + '132' + id;
            monitor.taskCompleted = new Date();
            monitor.workerName = 'John Doe';
            monitor.inputType = 'Text';
            monitor.shouldBe = 'Yes';
            monitor.targetValue = 'Yes';
            monitor.faultHandling = 'Loreum';
            monitor.sendEmailNotification = true;
            monitors.push(monitor);

        } else {

            for (let index = 1; index < 75; index++) {

                let monitor = new Monitor();
                monitor.id = index;
                monitor.description = 'Loreum Ipsum desca' + index;
                monitor.monitorType = 'Monitor Type' + index;
                monitor.passing = true;
                monitor.result = 'Sample Result text';
                monitor.serialNumber = index + '132' + index;
                monitor.taskCompleted = new Date();
                monitor.workerName = 'John Doe';
                monitor.inputType = 'Text';
                monitor.shouldBe = 'Yes';
                monitor.targetValue = 'Yes';
                monitor.faultHandling = 'Loreum';
                monitor.sendEmailNotification = true;

                monitors.push(monitor);
    
    
            }

        }


        return monitors;
    }

    procedureStepGet(id: number | null | undefined) {

        let procedureSteps = new Array<ProcedureStep>()

        if (id !== null && id !== undefined) {

            let procedureStep = new ProcedureStep();
            procedureStep.id = id;
            procedureStep.laborTime = 32;
            procedureStep.predecessorStepId = undefined;
            procedureStep.printOrder = 1;
            procedureStep.procedureId = 1; // TODO: This might change
            procedureStep.referenceFiles = this.getReferennceFiles().slice(0, 3);
            procedureStep.replacementCost = 5.00;
            procedureStep.roles = this.roleGet().slice(0, 3);
            procedureStep.text = 'Loreum Ipsum';
            procedureStep.title = 'Sample Title for Procedure';
            procedureStep.usefulLife = 10;
            procedureStep.utilizationTime = 8;
            procedureStep.duration = 10;
            procedureStep.durationType = 'SYS_MINUTES';
            procedureStep.equipmentTime = 3;
            procedureStep.procedureStepTypeId = 1;
            procedureSteps.push(procedureStep);

        } else {

            for (let index = 1; index < 75; index++) {

                let procedureStep = new ProcedureStep();
                procedureStep.id = index;
                procedureStep.laborTime = 32 + index;
                procedureStep.predecessorStepId = undefined;
                procedureStep.printOrder = 1;
                procedureStep.procedureId = 1; // TODO: This might change
                procedureStep.referenceFiles = this.getReferennceFiles().slice(0, 3);
                procedureStep.replacementCost = 5.00;
                procedureStep.roles = this.roleGet().slice(0, 3);
                procedureStep.text = 'Loreum Ipsum' + index;
                procedureStep.title = 'Sample Title for Procedure' + index;
                procedureStep.usefulLife = 10 + index;
                procedureStep.utilizationTime = 8 + index;
                procedureStep.duration = index;
                procedureStep.durationType = 'SYS_MINUTES';
                procedureStep.equipmentTime = index;
                procedureStep.procedureStepTypeId = 2;
                procedureSteps.push(procedureStep);

            }

        }

        return procedureSteps;
    }

    getReferennceFiles() {
        return [{
            "fileId": 197,
            "entityId": 8965,
            "name": "aaa.docx",
            "base64String": null,
            "fileContents": null,
            "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "fileURL": ""
        },
        {
            "fileId": 198,
            "entityId": 8965,
            "name": "lavarropasS.pdf",
            "base64String": null,
            "fileContents": null,
            "contentType": "application/pdf",
            "fileURL": ""
        },
        {
            "fileId": 199,
            "entityId": 8965,
            "name": "xlsAlecTest.xlsx",
            "base64String": null,
            "fileContents": null,
            "contentType": "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "fileURL": ""
        },
        {
            "fileId": 200,
            "entityId": 8965,
            "name": "alec.jpg",
            "base64String": null,
            "fileContents": null,
            "contentType": "image/jpeg",
            "fileURL": ""
        },
        {
            "fileId": 201,
            "entityId": 8965,
            "name": "cmh.PNG",
            "base64String": null,
            "fileContents": null,
            "contentType": "image/png",
            "fileURL": ""
        },
        {
            "fileId": 202,
            "entityId": 8965,
            "name": "casos practico.pptx",
            "base64String": null,
            "fileContents": null,
            "contentType": "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "fileURL": ""
        },
        {
            "fileId": 203,
            "entityId": 8965,
            "name": "aa.docx",
            "base64String": null,
            "fileContents": null,
            "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "fileURL": ""
        },
        {
            "fileId": 204,
            "entityId": 8965,
            "name": "TimeZoneShit.PNG",
            "base64String": null,
            "fileContents": null,
            "contentType": "image/png",
            "fileURL": ""
        }];
    }

    procedureStepMonitorsPost(createProcedureStepMonitorRequest: CreateProcedureStepMonitorRequest){
        alert("Success create placeholder for Web API");
    }

    procedureStepMonitorsPatch(updateProcedureStepMonitorRequest: UpdateProcedureStepMonitorRequest){
        alert("Success create placeholder for Web API");
    }

    procedureStepMonitorsDelete(id: number | null | undefined){

        alert("Success delete placeholder for Web API with id: " + id);

    }

    procedureStepMonitorsGet(id: number | null | undefined) {

        let procedureStepMonitor = new Array<ProcedureStepMonitor>()

        if (id !== null && id !== undefined) {

            let monitor = new ProcedureStepMonitor();
            monitor.id = id;
            monitor.inputType = 'Manual';
            monitor.type = 'Text';
            monitor.shouldBe = 'EQUAL';
            monitor.targetValue = 'Yes';
            monitor.faultHandling = 'RECORD AND CONTINUE';
            monitor.sendEmailNotification = true;
            monitor.listSource = "NCR Category";
            procedureStepMonitor.push(monitor);

        } else {

            for (let index = 1; index < 75; index++) {

                let monitor = new ProcedureStepMonitor();
                monitor.id = index;
                monitor.description = 'Loreum Ipsum desca' + index;
                monitor.inputType = 'Manual';
                monitor.type = 'Text';
                monitor.shouldBe = 'EQUAL';
                monitor.targetValue = 'Yes';
                monitor.faultHandling = 'RECORD AND CONTINUE';
                monitor.sendEmailNotification = true;
                monitor.listSource = "NCR Category";
                procedureStepMonitor.push(monitor);
    
    
            }

        }


        return procedureStepMonitor;
    }
}