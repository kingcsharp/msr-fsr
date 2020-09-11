import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WipstatusComponent } from './wipstatus.component';

describe('WipstatusComponent', () => {
  let component: WipstatusComponent;
  let fixture: ComponentFixture<WipstatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WipstatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WipstatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
