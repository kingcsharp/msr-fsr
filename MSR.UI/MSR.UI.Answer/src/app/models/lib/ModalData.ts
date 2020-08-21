import { Subject } from 'rxjs';

export class ModalData {
    showModal: boolean;
    comment: Subject<string>;
}