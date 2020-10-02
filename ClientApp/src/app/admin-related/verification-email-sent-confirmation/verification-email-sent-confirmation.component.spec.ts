import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerificationEmailSentConfirmationComponent } from './verification-email-sent-confirmation.component';

describe('VerificationEmailSentConfirmationComponent', () => {
  let component: VerificationEmailSentConfirmationComponent;
  let fixture: ComponentFixture<VerificationEmailSentConfirmationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerificationEmailSentConfirmationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerificationEmailSentConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
