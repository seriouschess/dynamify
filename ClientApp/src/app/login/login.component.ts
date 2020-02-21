import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  constructor(private _httpService:HttpService) { }
  @Output() logEvent = new EventEmitter<number>();
  logged_admin_id:number;
  newAdminObject: Admin; //but seriously it's an Admin
  entered_email:string;
  entered_password:string;
  error_display:string;

  ngOnInit() {
    this.newAdminObject = {
      admin_id: 0,
      first_name: "",
      last_name: "",
      email: "",
      password: ""
    }

    this.entered_email = "";
    this.entered_password ="";
    this.error_display = "";
    this.logged_admin_id = 0;
    this.logEvent.emit(this.logged_admin_id); 
  }

  postAdminToService(){
    console.log(this.newAdminObject);
    //this.newAdminObject.admin_id = 0; //will be changed on the backend
    this._httpService.postAdmin<Admin>(this.newAdminObject).subscribe(
      result => {
        console.log(result);
        let incomingAdmin:any = result; //it's an Admin though.
        this.logged_admin_id = incomingAdmin.admin_id;
        this.logEvent.emit(this.logged_admin_id);
      });
  }

  loginAdmin(){
    let login_package = {
      email: this.entered_email,
      password: this.entered_password
    }

    this._httpService.getAdminByEmail(login_package).subscribe(
      result => {
        console.log(result);
        let incomingAdmin:any = result;
        if(incomingAdmin.first_name === "<ACCESS DENIED, Password Invalid>"){
          this.error_display = "Invalid password";
        }

        if(incomingAdmin.first_name === "<NOT FOUND, Email invalid>"){
          this.error_display = "Email not registered";
        }

        if(incomingAdmin.email === this.entered_email && incomingAdmin.password == this.entered_password){
          this.logged_admin_id = incomingAdmin.admin_id;
          this.logEvent.emit(this.logged_admin_id);
          this.error_display = "";
        }
        
        this.logged_admin_id = incomingAdmin.admin_id;
      }
    )
  }
}



interface Admin {
  admin_id: number;
  first_name: string;
  last_name: string;
  email: string;
  password: string;
}

interface Login {
  email: string;
  password: string;
}
