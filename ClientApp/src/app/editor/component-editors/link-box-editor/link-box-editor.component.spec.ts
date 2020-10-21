import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkBoxEditorComponent } from './link-box-editor.component';

describe('LinkBoxEditorComponent', () => {
  let component: LinkBoxEditorComponent;
  let fixture: ComponentFixture<LinkBoxEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LinkBoxEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkBoxEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
