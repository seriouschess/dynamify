import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';

//dto imports
import { Admin } from '../interfaces/dtos/admin_dtos';

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

  constructor(private _httpService:HttpService) {}

  ngOnInit() {
    this.current_site_editor_id = 0;

    this.current_admin = {
      admin_id: 0,
      first_name: "",
      last_name: "",
      email: "",
      password: "",
      token: ""
    }
    
    this.open_admin_editor = false;
    this.current_admin.admin_id = 0; //default impossible value
  }

  recieveAdminFromLogin($event){
    this.current_admin.admin_id = $event.admin_id;
    this.current_admin = $event;
  }

  selectAdmin(admin_id:number){
    this.current_admin.admin_id = admin_id;
  }

  //editor related methods
  setSelectedSite($event){
    this.current_site_editor_id = $event;
  }

  resetEditor(){
    this.current_site_editor_id = 0;
  }

  //currently unused methods
  allAdmins(){
    this._httpService.getAdmins().subscribe(result => {
      if(result){
        this.admins = result;
      }
    }, error => console.log(error));
  }

  deleteAdminByID(id:number){
    this._httpService.deleteAdmin(id, this.current_admin.token).subscribe(result =>{
      console.log(result);
      this.allAdmins();
    });
  }
}
