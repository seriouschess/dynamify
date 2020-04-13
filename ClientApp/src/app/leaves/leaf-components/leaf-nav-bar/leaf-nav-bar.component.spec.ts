import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeafNavBarComponent } from './leaf-nav-bar.component';

describe('LeafNavBarComponent', () => {
  let component: LeafNavBarComponent;
  let fixture: ComponentFixture<LeafNavBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeafNavBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeafNavBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
