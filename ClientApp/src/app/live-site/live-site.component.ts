import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-live-site',
  templateUrl: './live-site.component.html',
  styleUrls: ['./live-site.component.css']
})
export class LiveSiteComponent implements OnInit {

  current_site:any; 
  start:boolean;

  constructor( private _httpService:HttpService){ }
  ngOnInit(){

    this.start = false;

    this.current_site = {
      site_id: 0, //set as default parameter
      title : "Default",
      admin_id: 0,
      owner: null,
      paragraph_boxes: null
    };

    this.requireSite(); //query database for site with active == true
  }

  requireSite(){
    this._httpService.getActiveSite().subscribe(results => {
      console.log(results);
      this.current_site = results;
      this.start = true;
      console.log("This is being rendered?: "+JSON.stringify(this.current_site));
    }, error => console.log(error));
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