import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-live-site',
  templateUrl: './live-site.component.html',
  styleUrls: ['./live-site.component.css']
})
export class LiveSiteComponent implements OnInit {

  formatted_site:any;
  start:boolean;

  constructor( private _httpService:HttpService){ }
  ngOnInit(){

    this.start = false;
    this.formatted_site = {
      site_id: 0,
      title: null,
      admin_id: null,
      SiteComponents: null
    }

    this.requireSite(); //query database for site with active == true
  }

  requireSite(){
    this._httpService.getActiveSite().subscribe(data =>{
    var s:any = data; //just for now I swear!
    var unformatted_site:Site = {
      site_id: s.site_id,
      title: s.title,
      admin_id: s.admin_id,
      owner: s.owner,
      paragraph_boxes: s.paragraph_boxes,
      portraits: s.portraits,
      two_column_boxes: s.two_column_boxes,
      images: s.images,
    }
    
    console.log(unformatted_site);
   
    var sorted_list_of_site_components = [];

    for(var x=0; x<unformatted_site.paragraph_boxes.length; x++){
      sorted_list_of_site_components.push(unformatted_site.paragraph_boxes[x]);
    };
    for(var x=0; x<unformatted_site.portraits.length; x++){
      sorted_list_of_site_components.push(unformatted_site.portraits[x]);
    };
    for(var x=0; x<unformatted_site.two_column_boxes.length; x++){
      sorted_list_of_site_components.push(unformatted_site.two_column_boxes[x]);
    };
    for(var x=0; x<unformatted_site.images.length; x++){
      sorted_list_of_site_components.push(unformatted_site.images[x]);
    }

    sorted_list_of_site_components.sort((a, b) => (a.priority > b.priority) ? 1 : -1)

    var formatted_site: any = {
      site_id: unformatted_site.site_id,
      title: unformatted_site.title,
      admin_id: unformatted_site.admin_id,
      site_components: sorted_list_of_site_components
    }
      

      this.formatted_site = formatted_site;
    });
  }
 }

 interface Admin{
  admin_id: number;
  first_name: string;
  last_name: string;
  email: string;
  password: string;
  token: string;
}

interface ParagraphBox{
  title: string;
  priority: number;
  site_id: number;

  content: string;
}

interface Image{
  title: string;
  priority:number;
  site_id: number;

  image_src: string;
}

interface Portrait{
  title: string;
  priority:number;
  site_id: number;

  image_src: string;
  content: string;
}

interface TwoColumnBox{
  title:string;
  priority:number;
  site_id:number;

  heading_one:string;
  heading_two:string;
  content_one:string;
  content_two:string;
}

interface Site{
  site_id: number;
  title: string;
  admin_id: number;
  owner: Admin;
  paragraph_boxes: ParagraphBox[];
  images: Image[];
  two_column_boxes: TwoColumnBox[];
  portraits: Portrait[];
}