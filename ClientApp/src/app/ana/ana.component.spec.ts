import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnaComponent } from './ana.component';

describe('AnaComponent', () => {
  let component: AnaComponent;
  let fixture: ComponentFixture<AnaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
