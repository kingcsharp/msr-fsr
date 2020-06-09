import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PcalendarWrapperComponent } from './pcalendar-wrapper.component';

describe('PcalendarWrapperComponent', () => {
  let component: PcalendarWrapperComponent;
  let fixture: ComponentFixture<PcalendarWrapperComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PcalendarWrapperComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PcalendarWrapperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
