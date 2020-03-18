import { Component, OnInit, Input } from '@angular/core';
import { HttpService } from '../http.service';
import {ParagraphBox, Image, TwoColumnBox, Portrait } from '../interfaces/dtos/site_dtos';
import { ISiteRequestDto } from '../interfaces/dtos/site_request_dto';
import { ValidationService } from '../validation.service';
import { BSfourConverterService } from '../b-sfour-converter.service';
import { ISiteContentDto } from '../interfaces/dtos/site_content_dto';
import { ISiteFormatted } from '../interfaces/formatted_site_content';
import { SiteFormatterService } from '../site-formatter.service';
// import { ConsoleReporter } from 'jasmine';

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
  site_request_object:ISiteRequestDto;

  formatted_site: ISiteFormatted; //not a Site type technically

  //site component types
  new_paragraph_box: ParagraphBox;
  new_2c_box: TwoColumnBox;
  new_image: Image;
  //temp_file:File;
  new_portrait: Portrait;

  //setImageBase64 asynic flag
  image_converter_working: boolean;


  //functionality
  open_next_component: string;

   constructor( private _httpService:HttpService, private validator:ValidationService, private b64converter:BSfourConverterService, private _siteFormatter:SiteFormatterService) { }

   ngOnInit() {
     this.site_request_object = {
       site_id: this.current_site_id,
       admin_id: this.current_admin_id,
       token: this.current_admin_token
     }

    this.formatted_site = {
      title: null,
      site_components: null
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

    ///this.temp_file = null;

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

    //image converter async flag
    this.image_converter_working = false;

    this.requireSite();
    this.open_next_component = ""; //used to select editor
  }

  requireSite(){
    this._siteFormatter.getSiteByIdFormatted(this.site_request_object, this.recieveSite, this);
  }

  recieveSite(formatted_site:ISiteFormatted, this_component:SiteEditorComponent){
    this_component.formatted_site = formatted_site;
  }

  deleteSiteComponentByIdAndType(component_id:number, type:string){
    this._httpService.deleteSiteComponent(component_id, type).subscribe(result =>{
      this.requireSite();
      console.log(result);
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
    this.new_image.image_src = ""; //reset potentially uploaded files
    this.new_portrait.image_src ="";
    this.validator.image_src_invalid_flag = false;
  }

  postParagraphBoxToService(){
    if(this.validator.validatePbox(this.new_paragraph_box)){
      this._httpService.postParagraphBox(this.new_paragraph_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        console.log(results);
        this.requireSite();
        this.open_next_component=""; //reset editing tool options
      }, error => console.log(error));
    } 
  }

  postTwoColumnBoxToService(){
      if(this.validator.validateTwoColumnBox(this.new_2c_box)){
      this._httpService.postTwoColumnBox(this.new_2c_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        console.log(results);
        this.requireSite();
        this.open_next_component="";
      }, error => console.log(error));
    }
  }

  //Image Conversion Methods
  fileConversionListener($event) : void {
    console.log("hi");
    console.log(this);
    this.b64converter.setImageBase64($event.target, this);
  };

  //for use with setImageBase64() required for async data retrieval
  B64Callback(output_string: string, this_component:SiteEditorComponent){
    this_component.image_converter_working = false;
    if(output_string === "invalid_file_type"){
      this_component.validator.image_src_invalid_flag = true;
    }else{
      this_component.new_image.image_src = output_string;
      this_component.new_portrait.image_src = output_string;
      this_component.validator.image_src_invalid_flag = false;
    }
  }

  postImageToService(){
    if(this.validator.validateImage(this.new_image, this.new_image.image_src)){
      //this.new_image.image_src = this.temp_file.data;
      this._httpService.postImage(this.new_image, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        console.log(results);
        this.requireSite();
        this.open_next_component="";
      }, error => console.log(error));
    }
  }

  postPortraitToService(){
    if(this.validator.validatePortrait(this.new_portrait, this.new_portrait.image_src)){
      this._httpService.postPortrait(this.new_portrait, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        console.log(results);
        this.requireSite();
        this.open_next_component="";
  
      }, error => console.log(error));
    }
  }
}
