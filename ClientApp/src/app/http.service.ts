import { Injectable, Inject } from '@angular/core';
import{ HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class HttpService {
  // @Inject('BASE_URL') baseUrl: string
  constructor(private _http:HttpClient) {
    this.get_data();
  }

  get_data(){
    var data:any = this._http.get<WeatherForecast[]>('weatherforecast');
    console.log(data);
    return data
  }
}

interface WeatherForecast{
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
