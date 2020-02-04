import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent implements OnInit{
  public admins: Admin[];
  public newAdminObject: Admin;
  public open_editor: boolean;
  public editAdminObject: Admin;
  public current_admin_id: number; //for testing

  constructor(private _httpService:HttpService) {}

  ngOnInit() {
    this.allAdmins();

    this.editAdminObject = {
      admin_id: 0,
      first_name: "",
      last_name: "",
      email: "",
      password: ""
    }
    
    this.open_editor = false;
    this.current_admin_id = 0; //default impossible value
  }

  recieveAdminIdFromLogin($event){
    this.current_admin_id = $event;
  }

  deleteAdminByID(id:Admin["admin_id"]){
    this._httpService.deleteAdmin(id).subscribe(result =>{
      console.log(result);
      this.allAdmins();
    });
  }

  selectAdmin(admin_id:number){
    this.current_admin_id = admin_id; 
  }

  openEditor(admin:Admin){
    this.open_editor = true;
    this.editAdminObject = admin;
  }

  allAdmins(){
    this._httpService.getAdmins().subscribe(result => {
      if(result){
        this.admins = result;
      }
    }, error => console.log(error));
  }
}

interface Admin {
  admin_id: number
  first_name: string;
  last_name: string;
  email: string;
  password: string;
}
