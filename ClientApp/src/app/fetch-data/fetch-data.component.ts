import { Component, Inject } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(private _httpService:HttpService) {
    this._httpService.get_data().subscribe(result => {
      if(result){
        this.forecasts = result;
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
