import { Injectable, Inject } from '@angular/core';
import{ HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class HttpService {
  // @Inject('BASE_URL') baseUrl: string
  constructor(private _http:HttpClient) { }

  getAdmins(){
    var data:any = this._http.get<Admin[]>('admin');
    return data;
  }

  postAdmin(NewAdmin:Admin){
   console.log(JSON.stringify(NewAdmin));
   return this._http.post('admin', NewAdmin);
  }

  deleteAdmin(adminId:Admin["adminId"]){
    console.log(`Admin ID for deletion:${adminId}`);
    return this._http.delete(`admin/${adminId}`);
  }

  editAdmin(AdminToEdit:Admin){
    console.log(JSON.stringify(AdminToEdit));
    return this._http.put('admin', AdminToEdit);
  }
}

interface WeatherForecast{
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Admin{
  adminId: number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}
