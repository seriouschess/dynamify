import { Component, OnInit, Input } from '@angular/core';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-site-editor',
  templateUrl: './site-editor.component.html',
  styleUrls: ['./site-editor.component.css']
})
export class SiteEditorComponent implements OnInit {

  //route parameters
  @Input() current_site_id: number;
  @Input() current_admin_id: number;
  @Input() current_admin_token: string;

  formatted_site: any; //not a Site type technically

  //site component types
  new_paragraph_box: ParagraphBox;
  new_2c_box: TwoColumnBox;
  new_image: Image;
  new_portrait: Portrait;

  //validation flags
  pbox_title_invalid_flag:boolean;
  pbox_content_invalid_flag:boolean;

  image_title_invalid_flag:boolean;
  image_src_invalid_flag:boolean;

  portrait_title_invalid_flag:boolean;
  portrait_content_invalid_flag:boolean;
  portrait_image_src_invalid_flag:boolean;

  tcb_title_invalid_flag:boolean;
  tcb_head_one_invalid_flag:boolean;
  tcb_head_two_invalid_flag:boolean;
  tcb_content_one_invalid_flag:boolean;
  tcb_content_two_invalid_flag:boolean;


  //functionality
  open_next_component: string;

   constructor( private _httpService:HttpService) { }

   ngOnInit() {
     //this.current_site_id = 1;
     //this.current_admin_id = 1;

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

    //validation flags
    this.pbox_content_invalid_flag = false;
    this.pbox_title_invalid_flag = false;

    this.image_title_invalid_flag = false;
    this.image_src_invalid_flag = false;

    this.portrait_title_invalid_flag = false;
    this.portrait_content_invalid_flag = false;
    this.portrait_image_src_invalid_flag = false;

    this.tcb_title_invalid_flag = false;
    this.tcb_head_one_invalid_flag = false;
    this.tcb_head_two_invalid_flag = false;
    this.tcb_content_one_invalid_flag = false;
    this.tcb_content_two_invalid_flag = false;

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

  deleteSiteComponentByIdAndType(component_id:number, type:string){
    this._httpService.deleteSiteComponent(component_id, type).subscribe(result =>{
      this.getSiteFromService()
      console.log(result)
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

  validatePbox(test_box:ParagraphBox){
    let error_count = 0;

    if(test_box.title == ""){
      this.pbox_title_invalid_flag = true;
      error_count += 1;
    }else{
      this.pbox_title_invalid_flag = false;
    }

    if(test_box.content == ""){
      this.pbox_content_invalid_flag = true;
      error_count += 1;
    }else{
      this.pbox_content_invalid_flag = false;
    }
    
    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }

  validateImage(test_box:Image){
    let error_count = 0;

    if(test_box.title == ""){
      this.image_title_invalid_flag = true;
      error_count += 1;
    }else{
      this.image_title_invalid_flag = false;
    }

    if(test_box.image_src == ""){
      this.image_src_invalid_flag = true;
      error_count += 1;
    }else{
      this.image_src_invalid_flag = false;
    }
    
    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }

  validateTwoColumnBox(two_c_box:TwoColumnBox){
    let error_count = 0;

    if(two_c_box.title == ""){
      this.tcb_title_invalid_flag = true;
      error_count += 1;
    }else{
      this.tcb_title_invalid_flag = false;
     }

    if(two_c_box.heading_one == ""){
      this.tcb_head_one_invalid_flag = true;
      error_count += 1;
    }else{
     this.tcb_head_one_invalid_flag = false;
    }

    if(two_c_box.heading_two == ""){
      this.tcb_head_two_invalid_flag = true;
      error_count += 1;
    }else{
      this.tcb_head_two_invalid_flag = false;
    }

    if(two_c_box.content_one == ""){
      this.tcb_content_one_invalid_flag = true;
      error_count += 1;
    }else{
      this.tcb_content_one_invalid_flag = false;
     }

    if(two_c_box.content_two == ""){
      this.tcb_content_two_invalid_flag = true;
      error_count += 1;
    }else{
      this.tcb_content_two_invalid_flag = false;
     }
    
    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }


  validatePortrait(test_portrait:Portrait){
    let error_count = 0;

    if(test_portrait.title == ""){
      this.portrait_title_invalid_flag = true;
      error_count += 1;
    }else{
      this.portrait_title_invalid_flag = true;
    }

    if(test_portrait.content == ""){
      this.portrait_content_invalid_flag = true;

    }else{
      this.portrait_content_invalid_flag = false;
    }

    if(test_portrait.image_src == ""){
      this.portrait_image_src_invalid_flag = true;
    }else{
      this.portrait_image_src_invalid_flag = false;
    }

    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }

  postParagraphBoxToService(){
    if(this.validatePbox(this.new_paragraph_box)){
      this._httpService.postParagraphBox(this.new_paragraph_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        console.log(results);
        this.getSiteFromService();
        this.open_next_component=""; //reset editing tool options
      }, error => console.log(error));
    } 
  }

  postTwoColumnBoxToService(){
      if(this.validateTwoColumnBox(this.new_2c_box)){
      this._httpService.postTwoColumnBox(this.new_2c_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        console.log(results);
        this.getSiteFromService();
        this.open_next_component="";
      }, error => console.log(error));
    }
  }

  postImageToService(){
    if(this.validateImage(this.new_image)){
      this._httpService.postImage(this.new_image, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        console.log(results);
        this.getSiteFromService();
        this.open_next_component="";
  
      }, error => console.log(error));
    }
  }

  postPortraitToService(){
    if(this.validatePortrait(this.new_portrait)){
      this._httpService.postPortrait(this.new_portrait, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        console.log(results);
        this.getSiteFromService();
        this.open_next_component="";
  
      }, error => console.log(error));
    }
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

interface Site {
  site_id: number;
  title: string;
  admin_id: number;
  owner: Admin;
  paragraph_boxes: ParagraphBox[];
  images: Image[];
  two_column_boxes: TwoColumnBox[];
  portraits: Portrait[];
}
