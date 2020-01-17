import { Component, Inject } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public admins: Admin[];

  constructor(private _httpService:HttpService) {
    this.allAdmins();
  }

  newAdmin(){
    this._httpService.post_admin();
  }

  allAdmins(){
    this._httpService.get_admins().subscribe(result => {
      if(result){
        this.admins = result;
      }
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Admin {
  FirstName: string;
  LastName: string;
  Email: string;
  Password: string;
}


