import { Injectable, Inject } from '@angular/core';
import{ HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class HttpService {
  // @Inject('BASE_URL') baseUrl: string
  constructor(private _http:HttpClient) { }

  get_weather(){
    var data:any = this._http.get<WeatherForecast[]>('weatherforecast');
    console.log(data);
    return data;
  }

  get_admins(){
    var data:any = this._http.get<Admin[]>('admin');
    return data;
  }

  post_admin(){
    var NewAdmin:Admin = {
      "FirstName":"Fakey",
      "LastName":"McFake",
      "Email":"fake@notreal.com",
      "Password":"12345"
    };
    console.log(NewAdmin);
    console.log(JSON.stringify(NewAdmin));
    var data:any = this._http.post(
      'admin',
       JSON.stringify(NewAdmin),
      );
    console.log(data);
    return NewAdmin;
  }
}

interface WeatherForecast{
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Admin{
  FirstName: string;
  LastName: string;
  Email: string;
  Password: string;
}
