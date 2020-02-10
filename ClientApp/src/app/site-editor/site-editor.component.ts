import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-site-editor',
  templateUrl: './site-editor.component.html',
  styleUrls: ['./site-editor.component.css']
})
export class SiteEditorComponent implements OnInit {

  //route parameters
  current_site_id: number;
  current_admin_id: number;

  open_next_component: string;
  formatted_site: any; //seriously though, it's a Site type

  //site component types
  new_paragraph_box: ParagraphBox;
  new_2c_box: TwoColumnBox;
  new_image: Image;
  new_portrait: Portrait;

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

    this.new_paragraph_box = {
      title: "",
      priority: 0,
      content: "",
      site_id: this.current_site_id,
    }

    this.new_2c_box = {
      title: "",
      priority: 0,
      site_id: this.current_site_id,
      heading_one: "",
      heading_two: "",
      content_one: "",
      content_two: ""
    }

    this.new_image = {
      title: "",
      priority: 0,
      site_id: this.current_site_id,
      image_src: "",
    }

    this.new_portrait = {
      title: "",
      priority: 0,
      site_id: this.current_site_id,
      image_src: "",
      content: ""
    }

    this.getSiteFromService();
    this.open_next_component = ""; //used to select editor
    
  }

  getSiteFromService(){
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

      //sets new component to the highest priority +100 by default to ensure it is deplayed on the bottom
      if(this.formatted_site.site_components.length < 1){
        this.new_paragraph_box.priority = 0; //first element
      }else{
        let new_priority = this.formatted_site.site_components[formatted_site.site_components.length-1].priority+100; 
        this.new_paragraph_box.priority = new_priority;
        this.new_2c_box.priority = new_priority;
        this.new_image.priority = new_priority;
        this.new_portrait.priority = new_priority;
        console.log("New "+JSON.stringify(this.new_paragraph_box));
      }
    });
  }

  //set editors
  setPboxEdit(){
    this.open_next_component="p_box";
  }

  set2cBoxEdit(){
    this.open_next_component="2c_box";
  }

  setPortraitEdit(){
    this.open_next_component="portrait";
  }

  setImageEdit(){
    this.open_next_component="image";
  }

  resetEditOptions(){
    this.open_next_component='';
  }

  postParagraphBoxToService(){
      this._httpService.postParagraphBox(this.new_paragraph_box).subscribe(results =>{
      console.log(results);
      this.getSiteFromService();
      this.open_next_component=""; //reset editing tool options
    }, error => console.log(error));
  }

  postTwoColumnBoxToService(){
    this._httpService.postTwoColumnBox(this.new_2c_box).subscribe(results =>{
      console.log(results);
      this.getSiteFromService();
      this.open_next_component="";
    }, error => console.log(error));
  }

  postImageToService(){
    console.log(this.new_image);
    this._httpService.postImage(this.new_image).subscribe(results =>{
      console.log(results);
      this.getSiteFromService();
      this.open_next_component="";

    }, error => console.log(error));
  }

  postPortraitToService(){
    this._httpService.postPortrait(this.new_portrait).subscribe(results =>{
      console.log(results);
      this.getSiteFromService();
      this.open_next_component="";

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
  images: Image[]
  two_column_boxes: Portrait[]
  portraits: Portrait[]
}
