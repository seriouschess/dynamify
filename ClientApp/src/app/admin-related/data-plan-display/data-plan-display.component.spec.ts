import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DataPlanDisplayComponent } from './data-plan-display.component';

describe('DataPlanDisplayComponent', () => {
  let component: DataPlanDisplayComponent;
  let fixture: ComponentFixture<DataPlanDisplayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DataPlanDisplayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DataPlanDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
