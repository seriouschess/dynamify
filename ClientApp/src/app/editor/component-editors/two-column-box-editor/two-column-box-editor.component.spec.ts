import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TwoColumnBoxEditorComponent } from './two-column-box-editor.component';

describe('TwoColumnBoxEditorComponent', () => {
  let component: TwoColumnBoxEditorComponent;
  let fixture: ComponentFixture<TwoColumnBoxEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TwoColumnBoxEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TwoColumnBoxEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
