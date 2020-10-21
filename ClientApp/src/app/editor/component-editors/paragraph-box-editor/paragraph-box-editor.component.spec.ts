import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParagraphBoxEditorComponent } from './paragraph-box-editor.component';

describe('ParagraphBoxEditorComponent', () => {
  let component: ParagraphBoxEditorComponent;
  let fixture: ComponentFixture<ParagraphBoxEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParagraphBoxEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParagraphBoxEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
