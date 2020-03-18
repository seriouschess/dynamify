import { Component, OnInit } from '@angular/core';

import { HttpService } from '../http.service';
import { SiteFormatterService } from '../site-formatter.service';

import{ ISiteContentDto } from '../interfaces/dtos/site_content_dto';
import{ Image, ParagraphBox, TwoColumnBox, Portrait } from '../interfaces/dtos/site_dtos';
import { ISiteFormatted } from '../interfaces/formatted_site_content';


@Component({
  selector: 'app-live-site',
  templateUrl: './live-site.component.html',
  styleUrls: ['./live-site.component.css']
})
export class LiveSiteComponent implements OnInit {

  formatted_site:ISiteFormatted;
  start:boolean;

  constructor( private _httpService:HttpService, private _siteFormatter:SiteFormatterService){ }
  ngOnInit(){

    this.start = false;
    this.formatted_site = {
      title: null,
      site_components: null
    }

    this.requireSite(); //query database for site with active == true
  }

  requireSite(){
    this._siteFormatter.getActiveSiteFormatted(this.recieveSite, this);
  }

  recieveSite(formatted_site:ISiteFormatted, this_component:LiveSiteComponent){
    this_component.formatted_site = formatted_site;
  }
 }