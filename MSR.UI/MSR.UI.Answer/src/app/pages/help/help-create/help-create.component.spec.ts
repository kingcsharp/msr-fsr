import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HelpCreateComponent } from './help-create.component';

describe('HelpCreateComponent', () => {
  let component: HelpCreateComponent;
  let fixture: ComponentFixture<HelpCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HelpCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HelpCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
