
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable, BehaviorSubject, Subject } from 'rxjs';
import { ModalData } from '../../../app/models/lib/ModalData';

@Component({
  selector: 'approval-comment',
  templateUrl: './approval-comment.component.html',
  styleUrls: ['./approval-comment.component.scss']
})
export class ApprovalCommentComponent implements OnInit {
  showDialog: boolean = false;
  comment: string = '';
  @Input() modalData: ModalData = {
    showModal: false,
    comment: null
  };

  constructor() {

  }

  ngOnInit(): void {

  }

  commentSubmit() {
    const comment = this.comment;
    this.comment = '';
    this.modalData.showModal = false;
    this.modalData.comment.next(comment);
    this.modalData.comment.complete();
  }

  clseDialog() {
    this.comment = '';
    this.modalData.showModal = false;
    this.modalData.comment.next(null);
    this.modalData.comment.complete();
  }
}
