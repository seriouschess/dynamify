import { Component, OnInit } from '@angular/core';
import { ISiteFormatted } from '../interfaces/formatted_site_content';
import { SiteFormatterService } from '../site-formatter.service';
import { ISiteRequestDto } from '../interfaces/dtos/site_request_dto';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  constructor( private _siteFormatter:SiteFormatterService ) { }

  test_token:string;
  test_admin_id:number;
  test_site_id:number;

  formatted_site:ISiteFormatted;
  start:boolean;

  request:ISiteRequestDto;

  ngOnInit() {
    this.test_token = "3CLL6W0mAlU14Eo";
    this.test_admin_id = 1;
    this.test_site_id = 1;

    this.start = false;
    this.formatted_site = {
      title: null,
      site_components: null
    }

    this.request = {
      site_id: this.test_site_id,
      admin_id: this.test_admin_id,
      token: this.test_token,
    }

    //this.requireSite(); //query database for site with active == true
  }

  requireSite(){
    this._siteFormatter.getSiteByIdFormatted( this.request, this.recieveSite, this);
  }

  recieveSite(formatted_site:ISiteFormatted, this_component:HomepageComponent){
    this_component.formatted_site = formatted_site;
  }

}
