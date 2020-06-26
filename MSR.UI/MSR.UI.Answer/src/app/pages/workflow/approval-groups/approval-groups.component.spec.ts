import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovalGroupsComponent } from './approval-groups.component';

describe('ApprovalGroupsComponent', () => {
  let component: ApprovalGroupsComponent;
  let fixture: ComponentFixture<ApprovalGroupsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApprovalGroupsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprovalGroupsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
