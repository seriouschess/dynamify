import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  constructor(private _httpService:HttpService) { }
  @Output() logEvent = new EventEmitter<Admin>();
  logged_admin: Admin;
  newAdminObject: Admin;
  entered_email: string;
  entered_password: string;
  error_display: string;

   ngOnInit() {
    this.newAdminObject = {
      admin_id: 0,
      first_name: "",
      last_name: "",
      email: "",
      password: "",
      token: ""
     }

    this.logged_admin = {
      admin_id: 0,
      first_name: "",
      last_name: "",
      email: "",
      password: "",
      token: ""
    }

    this.entered_email = "";
    this.entered_password ="";
    this.error_display = "";
    this.logEvent.emit(this.logged_admin); //zero is the default non sql ID value
  }

  postAdminToService(){
    console.log(this.newAdminObject.admin_id);
    //this.newAdminObject.admin_id = 0; //will be changed on the backend
    this._httpService.postAdmin<Admin>(this.newAdminObject).subscribe(
      result => {
        console.log(result);
        let incomingAdmin:any = result; //it's an Admin though.
        this.logged_admin.admin_id = incomingAdmin.admin_id;
        this.logged_admin.first_name = incomingAdmin.first_name;
        this.logged_admin.last_name = incomingAdmin.last_name;
        this.logged_admin.email = incomingAdmin.email;
        this.logged_admin.password = incomingAdmin.password;
        this.logged_admin.token = incomingAdmin.token;
        this.logEvent.emit(this.logged_admin);
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
          this.logged_admin = incomingAdmin;
          this.logEvent.emit(this.logged_admin); //zero is the default non sql ID value
          this.error_display = "";
        }
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
  token: string;
}

interface Login {
  email: string;
  password: string;
}
