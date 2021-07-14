import { Pipe, PipeTransform } from '@angular/core';
import { WorkOrderTaskModel } from '../services/api.client.generated';

@Pipe({
  name: 'tasktitle'
})
export class TaskTitlePipe implements PipeTransform {


  transform(workOrderTask: WorkOrderTaskModel): string {
    
    if(workOrderTask.procedureStep !== undefined){

        if(workOrderTask.procedureStep?.title !== undefined){
            return workOrderTask.procedureStep?.title
        } else {
            return workOrderTask.title;
        }

    } else {
        return workOrderTask.title;
    }

  }
}
