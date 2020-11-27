import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Admin } from 'src/app/interfaces/dtos/admin_related/admin_dto';
import { ClientStorageService } from 'src/app/services/client-storage/client-storage.service';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-password-reset',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {

  constructor(private _route: ActivatedRoute,
    private _httpClient: HttpService,
    private _router: Router) { }

    entered_password:string;
    confirm_entered_password:string;
    admin_id:string;
    token:string;

    //validation flags
    password_short_error_flag:boolean;
    password_entry_error:boolean;

    //operation
    password_reset_success_flag:boolean;
    admin_username:string;

  ngOnInit() {
    this.password_reset_success_flag;
    this.password_entry_error = false;
    this._route.params.subscribe((params:Params) => {
      this.admin_id = params['admin_id'];
      this.token = params['token'];
    });
  }

  resetPassword(){
    this.password_entry_error = false;
    this.password_short_error_flag = false;
    if(this.validatePassword()){
      if(this.entered_password == this.confirm_entered_password){
        this._httpClient.changeAdminPassword(this.admin_id, this.token, this.entered_password).subscribe((res:Admin) => {
          this.password_reset_success_flag = true;
          this.admin_username = res.username;
        }, err=>{
          console.log(err);
        });
      }else{
        this.password_entry_error = true;
      }
    }else{
      this.password_short_error_flag = true;
    }
  }

  validatePassword(){
    if( this.entered_password.length >= 8 ){
      return true;
    }else{
      return false;
    }
  }

  goToLogin(){
    this._router.navigate(['app/admin']);
  }
}
