import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-verification-email-sent-confirmation',
  templateUrl: './verification-email-sent-confirmation.component.html',
  styleUrls: ['./verification-email-sent-confirmation.component.css']
})
export class VerificationEmailSentConfirmationComponent implements OnInit {

  constructor(private _httpClient:HttpService, private _router:Router) { }

  email_to_send:string;
  email_sent:boolean;
  email_sent_validation_error:string;

  ngOnInit() {
    this.email_sent_validation_error = "";
    this.email_to_send = "";
  }

  sendVerificationEmail(){
    this.email_sent_validation_error = "";
    if(this.email_to_send != ""){
      this._httpClient.sendPasswordResetEmail(this.email_to_send).subscribe(res =>{
        this.email_sent = true;
      },err =>{
        this.email_sent_validation_error = err.error.message;
      });
    }
  }
}
