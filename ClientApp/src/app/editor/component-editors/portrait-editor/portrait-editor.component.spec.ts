import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PortraitEditorComponent } from './portrait-editor.component';

describe('PortraitEditorComponent', () => {
  let component: PortraitEditorComponent;
  let fixture: ComponentFixture<PortraitEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PortraitEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PortraitEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
