import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WiphistoryComponent } from './wiphistory.component';

describe('WiphistoryComponent', () => {
  let component: WiphistoryComponent;
  let fixture: ComponentFixture<WiphistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WiphistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WiphistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
