import { Component, OnInit } from '@angular/core';
import { HttpService } from '../../services/http/http.service';

//dto imports
import { Admin } from '../../interfaces/dtos/admin_related/admin_dto';
import { ClientStorageService } from '../../services/client-storage/client-storage.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})


//Manages admin registration. Also manages sites and houses the site editing tool for registered admins.

export class AdminComponent implements OnInit{
  public admins: Admin[];
  public newAdminObject: Admin;
  public open_admin_editor: boolean;

  public current_site_editor_id: number;
  public current_admin: Admin;
  public logged_in:boolean;

  constructor(private _httpService:HttpService,
              private _clientStorage:ClientStorageService) {}

  ngOnInit() {
    this.initiateLogin();
    this.current_site_editor_id = 0;
    this.open_admin_editor = false;
  }

  recieveAdminFromLogin($event){
    this.current_admin.admin_id = $event.admin_id;
    this.current_admin = $event;
    this.initiateLogin();
  }

  selectAdmin(admin_id:number){
    this.current_admin.admin_id = admin_id;
  }

  //editor related methods
  setSelectedSite($event){
    this.current_site_editor_id = $event;
  }

  resetEditor($event){
    this.current_site_editor_id = 0; //exits editor and displays all sites
  }

  resetLogin(){ //used by nav bar tab to log admins out
    this._clientStorage.logoutAdmin();
    this.initiateLogin();
  }

  initiateLogin(){
    if( this._clientStorage.checkLogin() ){
      let test_admin = this._clientStorage.getAdmin() as Admin;
      this.logged_in = true;

      this.current_admin = {
        admin_id: test_admin.admin_id,
        first_name: test_admin.first_name,
        last_name: test_admin.last_name,
        email: test_admin.email,
        password: test_admin.password,
        token: test_admin.token
      }

    }else{
      this.logged_in = false;

      this.current_admin = {
        admin_id: 0,
        first_name: "",
        last_name: "",
        email: "",
        password: "",
        token: ""
      }
    }
  }
}
