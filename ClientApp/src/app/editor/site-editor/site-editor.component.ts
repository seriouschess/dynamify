import { Component, OnInit, Input, Inject, Output, EventEmitter, AfterViewInit } from '@angular/core';
import { HttpService } from '../../services/http/http.service';
import { ValidationService } from '../../services/validation/validation.service';
import { BSfourConverterService } from '../../services/b-sfour-converter/b-sfour-converter.service';
import { DOCUMENT } from '@angular/common';
import { ISkeletonSiteDto } from 'src/app/interfaces/dtos/formatted_sites/skeleton_site_dto';
import { ParagraphBox } from 'src/app/interfaces/dtos/site_components/ParagraphBox';
import { TwoColumnBox } from 'src/app/interfaces/dtos/site_components/TwoColumnBox';
import { LinkBox } from 'src/app/interfaces/dtos/site_components/LinkBox';
import { Portrait } from 'src/app/interfaces/dtos/site_components/Portrait';
import { Image } from 'src/app/interfaces/dtos/site_components/Image';
import { ComponentReference } from 'src/app/interfaces/dtos/site_components/ComponentReference';

// import { ConsoleReporter } from 'jasmine';

@Component({
  selector: 'app-site-editor',
  templateUrl: './site-editor.component.html',
  styleUrls: ['./site-editor.component.css']
})

export class SiteEditorComponent implements OnInit, AfterViewInit {
  @Output() exitEvent = new EventEmitter<boolean>();
  //route parameters
  @Input() current_site_id: number;
  @Input() current_admin_id: number;
  @Input() current_admin_token: string;

  //site component types
  new_paragraph_box: ParagraphBox;
  new_2c_box: TwoColumnBox;
  new_image: Image;
  new_link_box: LinkBox;

  //temp_file:File;
  new_portrait: Portrait;

  //setImageBase64 asynic flag
  image_converter_working: boolean;

  //functionality
  preview_mode: boolean;
  open_next_component: string;
  nav_bar_editor_open: boolean;

  //dtos
  formatted_skeleton_site:ISkeletonSiteDto;


  tutorial_sequence:number;
  flash:boolean; 

  constructor( 
    private _httpService:HttpService,
    public validator:ValidationService,
    private b64converter:BSfourConverterService,
    private _apiClient:HttpService,
    @Inject(DOCUMENT) private document: Document,
    @Inject('Window') private window: Window //for scrolling
  ) 
  {
    this.window = this.document.defaultView;
  }

  ngAfterViewInit(): void {
    console.log( "Document Height: "+this.document.body.clientHeight );
  }

   ngOnInit() {
    this.formatted_skeleton_site = {
      title: null,
      site_id: null,
      site_components: null
     }

     this.initializeComponents();
     this.validator.resetValidation();

    //image converter async flag
    this.image_converter_working = false;

    this.open_next_component = ""; //used to select editor
    this.nav_bar_editor_open = false;
    this.preview_mode = false;

    this.requireSite();
  }

  initializeComponents(){

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

    this.new_link_box = {
      title: "",
      priority: 0,
      site_id: this.current_site_id,
      content: "",
      url: "",
      link_display: ""
    }
  }


  //used to get site content from the backend
  requireSite(){
    this._apiClient.getSkeletonSiteById(this.current_site_id).subscribe((res) =>{
      this.formatted_skeleton_site = res;
    },
     (err) => {
      console.log(err);
     });
  }

  //sets priority value for newly posted sites to be at the end of the list
  setPriority(){
    let new_priority:number;
    let component_count = this.formatted_skeleton_site.site_components.length;
    
    if(component_count <= 0){
      new_priority = 0;
    }else{
      new_priority = this.formatted_skeleton_site.site_components[component_count-1].priority + 1;
    }
    this.new_paragraph_box.priority = new_priority;
    this.new_2c_box.priority = new_priority;
    this.new_image.priority = new_priority;
    this.new_portrait.priority = new_priority;
    this.new_link_box.priority = new_priority;
  }

  swapSiteComponents(component_one_id:number, type_one:string, component_two_id:number, type_two:string){
    let component_one:ComponentReference = {
      component_id:component_one_id,
      component_type:type_one
    }
    let component_two:ComponentReference = {
      component_id: component_two_id,
      component_type:type_two
    }
    this._httpService.SwapComponentPriority(component_one, component_two, this.current_admin_id, this.current_admin_token, this.current_site_id ).subscribe(res =>{
      this.requireSite();
    });
  }

  //set editors
  setPboxEdit(){
    this.initializeComponents();
    this.validator.resetValidation();
    this.open_next_component="p_box";
    this.viewEditorBottom();
  }

  set2cBoxEdit(){
    this.validator.resetValidation();
    this.initializeComponents();
    this.open_next_component="2c_box";
    this.viewEditorBottom();
  }

  setPortraitEdit(){
    this.validator.resetValidation();
    this.initializeComponents();
    this.open_next_component="portrait";
    this.viewEditorBottom();
  }

  setImageEdit(){
    this.validator.resetValidation();
    this.initializeComponents();
    this.open_next_component="image";
    this.viewEditorBottom();
  }

  setLinkBoxEdit(){
      this.validator.resetValidation();
      this.initializeComponents();
      this.open_next_component= "link_box";
      this.viewEditorBottom();
  }

  toggleNavBarEditor(){
    if(this.nav_bar_editor_open == false){
      this.nav_bar_editor_open = true;
      this.viewEditorTop();
    }else{
      this.nav_bar_editor_open = false;
    }
  }

  resetEditOptions(){
    this.open_next_component='';
    this.new_image.image_src = ""; //reset potentially uploaded files
    this.new_portrait.image_src ="";
    this.validator.image_src_invalid_flag = false;
    this.initializeComponents();
  }

  leaveEditor(){ //leaves editor and goes to display sites component
    this.exitEvent.emit(true);
  }

  //sets preview mode on and off
  togglePreview(){
    if(this.preview_mode){
      this.preview_mode = false;
    }else{
      this.preview_mode = true;
    }
  }

  postParagraphBoxToService(){
    this.validator.resetValidation();
    if(this.validator.validatePbox(this.new_paragraph_box)){
      this.setPriority();
        this._httpService.postParagraphBox(this.new_paragraph_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
          this.requireSite();
          this.open_next_component=""; //reset editing tool options
        }, error => console.log(error));
    }
  }

  postTwoColumnBoxToService(){
    this.validator.resetValidation();
    if(this.validator.validateTwoColumnBox(this.new_2c_box)){
      this.setPriority();
        this._httpService.postTwoColumnBox(this.new_2c_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
          this.requireSite();
          this.open_next_component="";
        }, error => console.log(error));
    }
  }

  postImageToService(){
     //this.validator.resetValidation();
    if(this.validator.validateImage(this.new_image.image_src)){ //validate this.new_image?
      this.setPriority();
          //this.new_image.image_src = this.temp_file.data;
          this._httpService.postImage(this.new_image, this.current_admin_id, this.current_admin_token).subscribe(results =>{
            this.requireSite();
            this.open_next_component="";
          }, error => console.log(error));
    }
  }

  postPortraitToService(){
    this.validator.resetValidation();
    if(this.validator.validatePortrait(this.new_portrait, this.new_portrait.image_src)){
      this.setPriority();
      this._httpService.postPortrait(this.new_portrait, this.current_admin_id, this.current_admin_token).subscribe(results =>{
        this.requireSite();
        this.open_next_component="";
      }, error => console.log(error));
    }
  }

  postLinkBoxToService(){
    this.validator.resetValidation();
    if(this.validator.validateLinkBox(this.new_link_box)){
      this.setPriority();
      if(this.validator.validateLinkBox(this.new_link_box)){
        this._httpService.postLinkBox(this.new_link_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
          this.requireSite();
          this.open_next_component="";
        }, error => console.log(error));
      }
    }
  }

  //refresh editor data
  refreshRequest($event){
    this.requireSite();
  }

  //Image Conversion Methods
  fileConversionListener($event) : void {
    this.b64converter.setImageBase64($event.target, this);
  };

  //for use with setImageBase64() required for async data retrieval
  B64Callback(output_string: string, this_component:SiteEditorComponent){
    this_component.image_converter_working = false;
    if(output_string === "invalid_file_size"){
      this_component.validator.image_src_invalid_size_flag = true;
    }else if(output_string === "invalid_file_type"){
      this_component.validator.image_src_invalid_flag = true;
    }else{
      this_component.new_image.image_src = output_string;
      this_component.new_portrait.image_src = output_string;
      this_component.validator.image_src_invalid_flag = false;
      this_component.validator.image_src_invalid_size_flag = false;
    }
  }

  //QOL scrolling methods
  viewEditorBottom(){
    setTimeout(() => {
      if(!this.preview_mode){
        window.scrollTo(0,document.body.scrollHeight);
      }
    },50); //you are bad
  }

  viewEditorTop(){
    setTimeout(() => {
      if(!this.preview_mode){
        window.scrollTo(0,0);
      }
    },50);
  }
}
