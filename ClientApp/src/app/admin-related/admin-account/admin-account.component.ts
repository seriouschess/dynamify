import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Admin } from 'src/app/interfaces/dtos/admin_related/admin_dto';
import { ClientStorageService } from 'src/app/services/client-storage/client-storage.service';
import { HttpService } from 'src/app/services/http/http.service';
import { AdminComponent } from '../admin/admin.component';

@Component({
  selector: 'app-admin-account',
  templateUrl: './admin-account.component.html',
  styleUrls: ['./admin-account.component.css']
})
export class AdminAccountComponent implements OnInit {

  current_admin:Admin;
  updated_admin:Admin;
  email_sent:boolean;
  delete_selected:boolean;
  edit_selected:boolean;

  constructor( private _clientStorage:ClientStorageService,
    private _router:Router,
    private _http:HttpService) { }

  ngOnInit() {
    this.email_sent = false;
    this.delete_selected = false;
    this.edit_selected = false;
    this.initiateLogin();
  }

  toggleDeleteSelection(){
    this.delete_selected = !this.delete_selected;
  }

  toggleEditSelection(){
    this.edit_selected = !this.edit_selected;
  }

  deleteAccount(){
    this._http.deleteAdmin(this.current_admin.admin_id, this.current_admin.token).subscribe((res) =>{
      this._clientStorage.logoutAdmin();
      this.initiateLogin();
    });
  }

  updateAdminAccount(){
    this._http.editAdmin( this.updated_admin ).subscribe((res) => {
      console.log(res);
      this.initAdmin(res);
      this._clientStorage.storeAdmin(this.current_admin);
    });
  }

  updatePassword(){
    this._http.editAdmin( this.updated_admin ).subscribe((res) => {
      this.current_admin = res;
      this.updated_admin = this.current_admin;
    });
  }


  toMySites(){
    this._router.navigate(['base/admin']);
  }

  initiateLogin(){
    if( this._clientStorage.checkLogin() ){
      let account_admin = this._clientStorage.getAdmin() as Admin;
      this.initAdmin(account_admin);
    }else{
      this._router.navigate(['base/admin']);
    }
  }

  initAdmin(changed_admin:Admin){
    this.current_admin = {
      admin_id: changed_admin.admin_id,
      first_name: changed_admin.first_name,
      last_name: changed_admin.last_name,
      email: changed_admin.email,
      password: changed_admin.password,
      token: changed_admin.token
    }

    this.updated_admin = {
      admin_id: changed_admin.admin_id,
      first_name: changed_admin.first_name,
      last_name: changed_admin.last_name,
      email: changed_admin.email,
      password: changed_admin.password,
      token: changed_admin.token
    };
  }

}
