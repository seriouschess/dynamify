import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PopInComponent } from './pop-in.component';

describe('PopInComponent', () => {
  let component: PopInComponent;
  let fixture: ComponentFixture<PopInComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PopInComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PopInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
