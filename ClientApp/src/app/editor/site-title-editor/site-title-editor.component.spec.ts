import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteTitleEditorComponent } from './site-title-editor.component';

describe('SiteTitleEditorComponent', () => {
  let component: SiteTitleEditorComponent;
  let fixture: ComponentFixture<SiteTitleEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteTitleEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteTitleEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
