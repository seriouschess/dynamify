import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveSiteComponent } from './live-site.component';

describe('LiveSiteComponent', () => {
  let component: LiveSiteComponent;
  let fixture: ComponentFixture<LiveSiteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LiveSiteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LiveSiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
