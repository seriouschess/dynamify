import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-site-editor',
  templateUrl: './site-editor.component.html',
  styleUrls: ['./site-editor.component.css']
})
export class SiteEditorComponent implements OnInit {

  site:any; //better be a site though
  start:boolean;
  test:any;

  constructor( private _httpService:HttpService) { }

  ngOnInit() {
    this.start = false;
    this.getSiteFromService(1);
  }

  postSiteToService(new_site:Site){
    this._httpService.postSite(new_site).subscribe(result =>{
      console.log(result);
    }, error => console.log(error));
  }

  getSiteFromService(site_id:number){
    this._httpService.getSite(site_id).subscribe(result =>{
      this.site = result; //better be a site though
      this.start = true;
    });
  }

  postParagraphBoxToService(p_box:ParagraphBox){
    this._httpService.postParagraphBox(p_box);
  }
}

interface Admin{
  admin_id: number;
  first_name: string;
  last_name: string;
  email: string;
  password: string;
}

interface ParagraphBox{
  paragraph_box_id: number;
  title: string;
  content: string;
  site_id:number;
}

interface Site{
  site_id: number;
  title: string;
  admin_id: number;
  owner: Admin;
  paragraph_boxes: ParagraphBox[];
}
