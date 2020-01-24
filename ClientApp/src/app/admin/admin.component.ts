import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})

export class AdminComponent {
  public admins: Admin[];
  public newAdminObject: Admin;
  public open_editor;
  public editAdminObject: Admin;

  constructor(private _httpService:HttpService) {
    this.allAdmins();
    this.newAdminObject = {
      adminId: 0,
      firstName: "",
      lastName: "",
      email: "",
      password: ""
    }

    this.editAdminObject = {
      adminId: 0,
      firstName: "",
      lastName: "",
      email: "",
      password: ""
    }
    
    this.open_editor = false;
  }

  postAdminToService(){
    console.log(this.newAdminObject);
    //this.newAdminObject.adminId = 0; //will be changed on the backend
    this._httpService.postAdmin(this.newAdminObject).subscribe(
      result => {
        console.log(result);
        this.allAdmins();
      });
  }

  deleteAdminByID(id:Admin["adminId"]){
    this._httpService.deleteAdmin(id).subscribe(result =>{
      console.log(result);
      this.allAdmins();
    });
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
  adminId: number
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}
