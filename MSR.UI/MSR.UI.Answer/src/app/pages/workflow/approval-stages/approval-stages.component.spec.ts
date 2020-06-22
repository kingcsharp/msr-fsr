import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovalStagesComponent } from './approval-stages.component';

describe('ApprovalStagesComponent', () => {
  let component: ApprovalStagesComponent;
  let fixture: ComponentFixture<ApprovalStagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApprovalStagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprovalStagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
