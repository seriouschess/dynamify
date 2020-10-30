import { Component, OnInit } from '@angular/core';
import { ContactForm } from '../interfaces/dtos/reports_related/ContactForm';
import { HttpService } from '../services/http/http.service';



@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit {

  open_contact:boolean;
  feedback_sent:boolean;
  contact_form:ContactForm;

  //error handling
  invalid_feedback_flag:boolean;

  constructor(private _httpService:HttpService) { }

  ngOnInit(): void {
    this.invalid_feedback_flag = false;
    this.contact_form = {
      email:"",
      feedback:""
    }
  }

  toggleContact(){
    this.open_contact = !this.open_contact;
  }

  sendFeedback(){
    if(this.contact_form.feedback != ""){
      this._httpService.SendFeedbackReport(this.contact_form).subscribe(res =>{
        this.feedback_sent = true;
      });
    }else{
      this.invalid_feedback_flag = true;
    }
  }

}
