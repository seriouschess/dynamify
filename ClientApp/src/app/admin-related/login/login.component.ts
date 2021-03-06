import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpService } from '../../services/http/http.service';
import { AdminRegistrationDto } from '../../interfaces/dtos/admin_related/admin_registration_dto';
import { Login } from '../../interfaces/dtos/admin_related/login_dto';
import { Admin } from '../../interfaces/dtos/admin_related/admin_dto';
import { ClientStorageService } from '../../services/client-storage/client-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  constructor(private _httpService:HttpService,
              private _clientStorage: ClientStorageService ) { }
  @Output() logEvent = new EventEmitter<Admin>();
  logged_admin: Admin;
  newAdminObject: AdminRegistrationDto;
  entered_email: string;
  entered_password: string;

  //asthetics
  open_registration:boolean;
  display_use_terms:boolean;
  registered:boolean;

  login_backend_error_message:any;//:string;
  
  //validation fields
  error_display: string;
  registration_password_error_flag: boolean;
  password_confirm: string;
  password_confirm_error_flag:boolean;
  username_required_error:boolean;
  email_validation_error_flag: boolean;
  duplicate_email_error: boolean;

  //login validation
  login_password_validation_error_flag: boolean;
  login_email_validation_error_flag: boolean;
  

  general_invalid_registration_error_flag: boolean; //backend denial

   ngOnInit() {

    this.newAdminObject = {
      username: "",
      email: "",
      password: "",
     }

    this.logged_admin = {
      admin_id: 0,
      username: "",
      email: "",
      password: "",
      token: ""
    }

    this.entered_email = "";
    this.entered_password ="";
    this.error_display = "";
    this.password_confirm = "";

    //functionality
    this.registered = false;
    this.open_registration = false;
    this.display_use_terms = false;
    //this.login_backend_error_message = "";

    //validation flags
    this.general_invalid_registration_error_flag = false; //backend denial
    this.duplicate_email_error = false; //backend found duplicate email in registration

    this.registration_password_error_flag = false;
    this.password_confirm_error_flag = false;
    this.email_validation_error_flag = false;
    this.login_email_validation_error_flag = false;
    this.login_password_validation_error_flag = false;
    this.username_required_error = false;
    this.logEvent.emit(this.logged_admin); //zero is the default non sql ID value
  }

  validateEmail(email:string):boolean {
    let re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }
  
  validateRegistration():boolean{ //returns true of registration information is valid
    let error_count = 0;

    if(this.newAdminObject.username == ""){
      this.username_required_error = true;
      error_count += 1;
    }else{
      this.username_required_error = false;
    }

    if( !this.validateEmail(this.newAdminObject.email) ){ //validate email
      this.email_validation_error_flag = true;
      error_count += 1;
    }else{
      this.email_validation_error_flag = false;
    }

    if(this.newAdminObject.password.length < 8){
      error_count += 1;
      this.registration_password_error_flag = true;
    }else{
      this.registration_password_error_flag = false;
    }

    if(this.newAdminObject.password != this.password_confirm){ //password confirmation match check
      this.password_confirm_error_flag = true;
      error_count += 1;
    }else{
      this.password_confirm_error_flag = false;
    }

    if(error_count > 0){
     return false;
    }else{
      return true;
    }
  }

  postAdminToService(){
    //initialize error flags
    this.general_invalid_registration_error_flag = false;
    this.duplicate_email_error = false;
    this.login_backend_error_message = "";

    if (this.validateRegistration()){
      
      this._httpService.postAdmin<AdminRegistrationDto>(this.newAdminObject).subscribe( //send new admin to backend
        result => {
          let _incomingAdmin:any = result; //it's an Admin though.
          let incomingAdmin:Admin = _incomingAdmin;

          //initialize newly created admin
          this.logged_admin.admin_id = incomingAdmin.admin_id;
          this.logged_admin.username = incomingAdmin.username;
          this.logged_admin.email = incomingAdmin.email;
          this.logged_admin.password = incomingAdmin.password;
          this.logged_admin.token = incomingAdmin.token;
          this.registered = true;

        }, err =>{   //400s or worse
          this.general_invalid_registration_error_flag = true;
          this.login_backend_error_message = err.error;
        });
    }
  }

  validateLogin(){
    let error_count:number = 0;
    if(this.validateEmail(this.entered_email)){
      this.login_email_validation_error_flag = false;
    }else{
      error_count += 1;
      this.login_email_validation_error_flag = true;
    }

    if(this.entered_password == ""){
      error_count += 1;
      this.login_password_validation_error_flag = true;
    }else{
      this.login_password_validation_error_flag = false;
    }

    if(error_count > 0){
      return false;
    } else {
      return true;
    }
  }

  loginAdmin(){
    if(this.validateLogin()){
      let login_package:Login = {
        email: this.entered_email,
        password: this.entered_password
      }
  
        this._httpService.loginAdmin(login_package).subscribe(
          result => {
          let incomingAdmin:any = result;
        
            this.logged_admin = incomingAdmin;

            this._clientStorage.storeAdmin( this.logged_admin );

            this.logEvent.emit(this.logged_admin); //zero is the default non sql ID value
            this.error_display = "";
          }, err =>{
            this.error_display = err.error;
          });
      }
    }

    openRegistration(){
      this.open_registration = true;
    }

    closeRegistration(){
      this.open_registration = false;
      this.display_use_terms = false;
    }

    toggleTerms(){
      this.display_use_terms = !this.display_use_terms;
    }

 
}
