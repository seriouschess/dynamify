import { Injectable } from '@angular/core';
import { Admin } from 'src/app/interfaces/dtos/admin_related/admin_dto';
import { AdminComponent } from 'src/app/admin-related/admin/admin.component';

@Injectable({
  providedIn: 'root'
})

export class ClientStorageService {

  constructor() { }

  storeAdmin( stored_admin:Admin ){
    localStorage.setItem( "logged_admin", JSON.stringify(stored_admin) ); //localStorage used
  }

  getAdmin<Admin>():Admin{
    return JSON.parse(localStorage.getItem( "logged_admin" )) as Admin;
  }

  logoutAdmin(){
    localStorage.clear();
  }

  checkLogin(){
    let test_admin = this.getAdmin() as Admin; //it's definitely an Admin!

    if(test_admin === undefined || test_admin === null){
      console.log("Admin not in storage");
      return false;
    }else{
      console.log("Admin in storage");
      console.log("-----local storage admin---------");
      console.log( JSON.stringify(test_admin) );
      console.log( "Name: "+test_admin.first_name );
      console.log("-------------------");
      return true;
    } 
  }
}
