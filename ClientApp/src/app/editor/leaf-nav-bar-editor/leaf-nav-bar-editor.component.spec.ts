import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeafNavBarEditorComponent } from './leaf-nav-bar-editor.component';

describe('LeafNavBarEditorComponent', () => {
  let component: LeafNavBarEditorComponent;
  let fixture: ComponentFixture<LeafNavBarEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeafNavBarEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeafNavBarEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
