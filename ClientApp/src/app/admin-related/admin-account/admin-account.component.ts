import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Admin } from 'src/app/interfaces/dtos/admin_dto';
import { ClientStorageService } from 'src/app/services/client-storage/client-storage.service';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-admin-account',
  templateUrl: './admin-account.component.html',
  styleUrls: ['./admin-account.component.css']
})
export class AdminAccountComponent implements OnInit {

  current_admin:Admin;
  email_sent:boolean;
  delete_selected:boolean;

  constructor( private _clientStorage:ClientStorageService,
    private _router:Router,
    private _http:HttpService) { }

  ngOnInit() {
    this.email_sent = false;
    this.delete_selected = false;
    this.initiateLogin();
  }

  toggleDeleteSelection(){
    this.delete_selected = !this.delete_selected;
  }

  deleteAccount(){
    this._http.deleteAdmin(this.current_admin.admin_id, this.current_admin.token).subscribe((res) =>{
      this._clientStorage.logoutAdmin();
      this.initiateLogin();
    });
  
  }

  initiateLogin(){
    if( this._clientStorage.checkLogin() ){
      let test_admin = this._clientStorage.getAdmin() as Admin;

      this.current_admin = {
        admin_id: test_admin.admin_id,
        first_name: test_admin.first_name,
        last_name: test_admin.last_name,
        email: test_admin.email,
        password: test_admin.password,
        token: test_admin.token
      }

    }else{
      this._router.navigate(['base/admin']);
    }
  }

}
