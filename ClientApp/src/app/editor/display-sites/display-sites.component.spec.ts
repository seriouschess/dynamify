import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplaySitesComponent } from './display-sites.component';

describe('DisplaySitesComponent', () => {
  let component: DisplaySitesComponent;
  let fixture: ComponentFixture<DisplaySitesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DisplaySitesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplaySitesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
