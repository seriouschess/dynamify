import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParagraphBoxComponent } from './paragraph-box.component';

describe('ParagraphBoxComponent', () => {
  let component: ParagraphBoxComponent;
  let fixture: ComponentFixture<ParagraphBoxComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParagraphBoxComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParagraphBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
