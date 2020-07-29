import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CkeditorWrapperComponent } from './ckeditor-wrapper.component';

describe('CkeditorWrapperComponent', () => {
  let component: CkeditorWrapperComponent;
  let fixture: ComponentFixture<CkeditorWrapperComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CkeditorWrapperComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CkeditorWrapperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
