import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InformacionIPComponent } from './informacion-ip.component';

describe('InformacionIPComponent', () => {
  let component: InformacionIPComponent;
  let fixture: ComponentFixture<InformacionIPComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InformacionIPComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InformacionIPComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
