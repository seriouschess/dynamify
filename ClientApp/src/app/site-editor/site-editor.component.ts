import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { ActivatedRoute } from '@angular/router';
import { observable } from 'rxjs';

@Component({
  selector: 'app-site-editor',
  templateUrl: './site-editor.component.html',
  styleUrls: ['./site-editor.component.css']
})
export class SiteEditorComponent implements OnInit {

  //route parameters
  current_site_id:number;
  current_admin_id:number;

  test:any;
  current_site:any; //seriously though, it's a Site type
  new_paragraph_box: ParagraphBox;
  formatted_site: any;

  constructor( private _httpService:HttpService, private _Activatedroute:ActivatedRoute) { }

  ngOnInit() {
    this.current_site_id = Number(this._Activatedroute.snapshot.paramMap.get("current_site_id"));
    this.current_admin_id = Number(this._Activatedroute.snapshot.paramMap.get("current_admin_id"));

    this.formatted_site = {
      site_id: null,
      title: null,
      admin_id: null,
      SiteComponents: null
    }

    this.current_site = {
      site_id: 0, //set as default parameter
      title : "Default",
      admin_id: 0,
      owner: null,
      paragraph_boxes: null,
      images: null,
      two_column_boxes: null,
      portraits: null
    }

    this.new_paragraph_box = {
      paragraph_box_id: 0,
      title: "",
      type: "p_box",
      priority: 0,
      content: "",
      site_id: this.current_site_id,
    }

    this.getSiteFromService(); //based on this.current_admin_id

    //test!
    this.getSiteFormatted();
  }

  postSiteToService(new_site:Site){
    this._httpService.postSite(new_site).subscribe(result =>{
      console.log(result);
    }, error => console.log(error));
  }

  getSiteFromService(){
    this._httpService.getSite(this.current_site_id).subscribe(result =>{
      this.current_site = result; //better be a site though
    });
  }

  getSiteFormatted(){
    this._httpService.getSite(this.current_site_id).subscribe(data =>{
    var s:any = data; //just for now I swear!
    var unformatted_site = {
      site_id: s.site_id,
      title: s.title,
      admin_id: s.admin_id,
      priority: s.priority,
      paragraph_boxes: s.paragraph_boxes,
      portraits: s.portraits,
      two_column_boxes: s.two_column_boxes,
      images: s.images,
    }
   
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
      console.log(formatted_site);

      this.formatted_site = formatted_site;
    });
  }


  postParagraphBoxToService(){
      this._httpService.postParagraphBox(this.new_paragraph_box).subscribe(results =>{
      console.log(results);
      this.getSiteFromService();
    }, error => console.log(error));
  }

  editSite(site_id:number, admin_id:number){
      this._httpService.getActiveSite().subscribe(results => {
      console.log(results);
      this.current_site = results;
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
  type: string;
  priority: number;
  content: string;
  site_id: number;
}

interface Portrait{
  portrait_id: number;
  title: string;
  type: string;
  priority: number;
  image_src: string;
  content: string;
}

interface Image{
  image_id: number;
  title: string;
  type: string;
  priority: number;
  image_src: string;
}

interface Site{
  site_id: number;
  title: string;
  admin_id: number;
  owner: Admin;
  paragraph_boxes: ParagraphBox[];
  images: Image[]
  two_column_boxes: Portrait[]
  portraits: Portrait[]
}
