import { Injectable } from '@angular/core';
import { Admin } from 'src/app/interfaces/dtos/admin_dto';

@Injectable({
  providedIn: 'root'
})
export class ClientStorageService {

  constructor() { }

  storeAdmin( stored_admin:Admin ){
    localStorage.setItem( "logged_admin", JSON.stringify(stored_admin) ); //localStorage used
    let test_admin = JSON.parse(localStorage.getItem( "logged_admin" )) as Admin;
    console.log("-----local storage admin---------");
    console.log( JSON.stringify(test_admin) );
    console.log( "Name: "+test_admin.first_name );
    console.log("-------------------");
    localStorage.clear(); 
    test_admin = JSON.parse(localStorage.getItem( "logged_admin" )) as Admin;
  }

  logoutAdmin(){
    localStorage.clear();
  }

  checkLogin(test_admin){
    if(test_admin === undefined || test_admin === null){
      console.log("Admin not in storage");
      return false;
    }else{
      console.log("Admin in storage");
      console.log("Email: "+test_admin.email);
      return true;
    } 
  }
}
