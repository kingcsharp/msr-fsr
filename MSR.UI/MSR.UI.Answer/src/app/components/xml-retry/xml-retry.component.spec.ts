import { ComponentFixture, TestBed } from '@angular/core/testing';

import { XmlRetryComponent } from './xml-retry.component';

describe('XmlRetryComponent', () => {
  let component: XmlRetryComponent;
  let fixture: ComponentFixture<XmlRetryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ XmlRetryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(XmlRetryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
