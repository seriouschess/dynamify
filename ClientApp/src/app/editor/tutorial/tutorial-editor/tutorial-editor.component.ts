import { Component, OnInit, Input, Inject, Output, EventEmitter, AfterViewInit } from '@angular/core';

//import { ISiteRequestDto } from '../../interfaces/dtos/site_request_dto';

//import { SiteFormatterService } from '../../services/leaf-formatter/site-formatter.service';
import { DOCUMENT } from '@angular/common';
import { SiteFormatterService } from 'src/app/services/leaf-formatter/site-formatter.service';
import { ISkeletonSiteDto } from 'src/app/interfaces/dtos/formatted_sites/skeleton_site_dto';
import { ISiteFormatted } from 'src/app/interfaces/dtos/tutorial_site_emulator_dtos/formatted_site_content';
import { HttpService } from 'src/app/services/http/http.service';
import { ValidationService } from 'src/app/services/validation/validation.service';
import { BSfourConverterService } from 'src/app/services/b-sfour-converter/b-sfour-converter.service';
import { ParagraphBox } from 'src/app/interfaces/dtos/site_components/ParagraphBox';
import { TwoColumnBox } from 'src/app/interfaces/dtos/site_components/TwoColumnBox';
import { Image } from 'src/app/interfaces/dtos/site_components/Image';
import { LinkBox } from 'src/app/interfaces/dtos/site_components/LinkBox';
import { NavLink } from 'src/app/interfaces/dtos/site_components/NavLink';
import { Portrait } from 'src/app/interfaces/dtos/site_components/Portrait';

@Component({
  selector: 'app-tutorial-editor',
  templateUrl: './tutorial-editor.component.html',
  styleUrls: ['./tutorial-editor.component.css']
})

export class TutorialEditorComponent implements OnInit, AfterViewInit {
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
  new_nav_link: NavLink;
  curlies: string; //have to do this because of the way angular templates handles curlies

  //temp_file:File;
  new_portrait: Portrait;

  //setImageBase64 asynic flag
  image_converter_working: boolean;

  //functionality
  preview_mode: boolean;
  open_next_component: string;
  nav_bar_editor_open: boolean;

  //dtos
  //site_request_object:ISiteRequestDto;
  tutorial_site: ISiteFormatted; //not a Site type technically
  formatted_skeleton_site:ISkeletonSiteDto;

  //tutorial related
  @Input() is_tutorial:boolean;
  tutorial_sequence:number;
  flash:boolean; 

  constructor( 
    private _httpService:HttpService,
    private validator:ValidationService,
    private b64converter:BSfourConverterService,
    private _siteFormatter:SiteFormatterService,
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
    //  this.site_request_object = {
    //    site_id: this.current_site_id,
    //    admin_id: this.current_admin_id,
    //    token: this.current_admin_token
    //  }

    // this.formatted_skeleton_site = {

    // }

    this.tutorial_site = {
      title: null,
      site_id: null,
      //nav_bar: null,
      site_components: null
     }

     this.initializeComponents();
     this.validator.resetValidation();

    //image converter async flag
    this.image_converter_working = false;

    this.open_next_component = ""; //used to select editor
    this.curlies = "{ or }";
    this.nav_bar_editor_open = false;
    this.preview_mode = false;

    //start mode
    if(this.is_tutorial){
      this.flash = false;
      this.tutorial_sequence = 1; //sequence starts from 1
      this.getTutorialSite();
    }else{
      this.tutorial_sequence = 0; //no tutorial
      this.requireSite();
    }
  }

  initializeComponents(){
    // let link_one:NavLink = {url:"http://www.cnn.com", label:"News Site"}

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
    //this._siteFormatter.getSiteByIdFormatted(this.site_request_object, this.recieveSite, this);
  }

  getTutorialSite(){ //retrieves a blank site for demo use
    this._siteFormatter.getBlankSite(this.recieveSite, this);
  }

  //callback sent to site formatter frontend service
  recieveSite(tutorial_site:ISiteFormatted, this_component:TutorialEditorComponent){
    this_component.tutorial_site = tutorial_site;
  }

  //sets priority value for newly posted sites to be at the end of the list
  setPriority(){
    let new_priority;
    let number_of_components = this.tutorial_site.site_components.length-1;
    
    if(number_of_components <= 0){
      new_priority = 0;
    }else{
      new_priority = this.tutorial_site.site_components[number_of_components].priority + 100;
    }
    this.new_paragraph_box.priority = new_priority;
    this.new_2c_box.priority = new_priority;
    this.new_image.priority = new_priority;
    this.new_portrait.priority = new_priority;
    this.new_link_box.priority = new_priority;
  }

  deleteSiteComponentByIdAndType(component_id:number, type:string){
    this._httpService.deleteSiteComponent(component_id, type, this.current_admin_id, this.current_admin_token).subscribe(result =>{
      this.requireSite();
    });  
  }

  //set editors
  setPboxEdit(){
    this.initializeComponents();
    this.validator.resetValidation();
    if( this.is_tutorial ){
      if(this.tutorial_sequence == 4){
        this.iterateTutorial();
      }
    }
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
    if(!this.is_tutorial || this.tutorial_sequence > 5){
      this.open_next_component='';
      this.new_image.image_src = ""; //reset potentially uploaded files
      this.new_portrait.image_src ="";
      this.validator.image_src_invalid_flag = false;
    }
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
      if(this.is_tutorial == true){
        let type = "p_box";
        this._siteFormatter.sortSite(this.tutorial_site, this.new_paragraph_box, type, this.recieveSite, this);
        this.iterateTutorial();
        this.initializeComponents();
        this.open_next_component=""; //reset editing tool options
      }else{
        this.setPriority();
        this._httpService.postParagraphBox(this.new_paragraph_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
          this.requireSite();
          this.open_next_component=""; //reset editing tool options
        }, error => console.log(error));
      } 
    }
  }

  postTwoColumnBoxToService(){
    this.validator.resetValidation();
    if(this.validator.validateTwoColumnBox(this.new_2c_box)){
      if(this.is_tutorial == true){
        let type = "2c_box";
        this._siteFormatter.sortSite(this.tutorial_site, this.new_2c_box, type, this.recieveSite, this);
        this.initializeComponents();
        this.open_next_component="";
      }else{
        this.setPriority();
        this._httpService.postTwoColumnBox(this.new_2c_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
          this.requireSite();
          this.open_next_component="";
        }, error => console.log(error));
      }
    }
  }

  postImageToService(){
     //this.validator.resetValidation();
    if(this.validator.validateImage(this.new_image.image_src)){ //validate this.new_image?
      if(this.is_tutorial == true){
        let type = "image";
        this._siteFormatter.sortSite(this.tutorial_site, this.new_image, type, this.recieveSite, this);
        this.initializeComponents();
        this.open_next_component="";
      }else{
          this.setPriority();
          //this.new_image.image_src = this.temp_file.data;
          this._httpService.postImage(this.new_image, this.current_admin_id, this.current_admin_token).subscribe(results =>{
            this.requireSite();
            this.open_next_component="";
          }, error => console.log(error));
      }
    }
  }

  postPortraitToService(){
    this.validator.resetValidation();
    if(this.validator.validatePortrait(this.new_portrait, this.new_portrait.image_src)){
      if(this.is_tutorial == true){
        let type = "portrait";
        this._siteFormatter.sortSite(this.tutorial_site, this.new_portrait, type, this.recieveSite, this);
        this.initializeComponents();
        this.open_next_component="";
      }else{
        this.setPriority();
          this._httpService.postPortrait(this.new_portrait, this.current_admin_id, this.current_admin_token).subscribe(results =>{
            this.requireSite();
            this.open_next_component="";
          }, error => console.log(error));
      }
    }
  }

  postLinkBoxToService(){
    this.validator.resetValidation();
    if(this.is_tutorial == true){
      let type = "link_box";
      this._siteFormatter.sortSite(this.tutorial_site, this.new_portrait, type, this.recieveSite, this);
      this.initializeComponents();
      this.open_next_component="";
    }else{
      this.setPriority();
      if(this.validator.validateLinkBox(this.new_link_box)){
        this._httpService.postLinkBox(this.new_link_box, this.current_admin_id, this.current_admin_token).subscribe(results =>{
          this.requireSite();
          this.open_next_component="";
        }, error => console.log(error));
      }
    }
  }

  //Image Conversion Methods
  fileConversionListener($event) : void {
    this.b64converter.setImageBase64($event.target, this);
  };

  //for use with setImageBase64() required for async data retrieval
  B64Callback(output_string: string, this_component:TutorialEditorComponent){
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

  //tutorial related
  iterateTutorial(){
    if(this.is_tutorial){
      this.tutorial_sequence += 1;
      if(this.tutorial_sequence == 6 || this.tutorial_sequence == 1){
        //this.window = this.document.defaultView;
        this.window.scrollTo(0,0);
      }
    }
  }

  cycleCssHighlight(){ //do we need this?
    if(this.flash){
      this.flash = false;
    }else{
      this.flash = true;
    }
  }
}
