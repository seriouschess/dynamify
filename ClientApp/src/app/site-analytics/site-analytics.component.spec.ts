import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteAnalyticsComponent } from './site-analytics.component';

describe('SiteAnalyticsComponent', () => {
  let component: SiteAnalyticsComponent;
  let fixture: ComponentFixture<SiteAnalyticsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteAnalyticsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteAnalyticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
