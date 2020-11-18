import { Component, Input, OnInit } from '@angular/core';
import { HttpService } from '../services/http/http.service';

@Component({
  selector: 'app-site-analytics',
  templateUrl: './site-analytics.component.html',
  styleUrls: ['./site-analytics.component.css']
})
export class SiteAnalyticsComponent implements OnInit {

  @Input() site_id:number;
  view_count:number;

  constructor(private _httpService:HttpService) { }

  ngOnInit(): void {
    this.view_count = 0;
    this.getAnalyticsForSite();
  }

    //analytics
    getAnalyticsForSite(){
      this._httpService.getAnalyticsForSite(this.site_id).subscribe(res =>{
        this.view_count = res.total_view_count;
      });
    }

}
