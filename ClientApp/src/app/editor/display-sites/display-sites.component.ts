import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpService } from '../../services/http/http.service';
import { INewSiteDto } from '../../interfaces/dtos/database_changers/new_site_dto';
import { JsonResponseDto } from '../../interfaces/dtos/json_response_dto';

@Component({
  selector: 'app-display-sites',
  templateUrl: './display-sites.component.html',
  styleUrls: ['./display-sites.component.css']
})

export class DisplaySitesComponent implements OnInit {


  //get Admin information
  @Input() current_admin_id: number;
  @Input() current_admin_token: string;

  //select site to be sent to admin component
  @Output() siteIdSelectEvent = new EventEmitter<number>();

  //tutorial related
  @Input() is_tutorial: boolean;
  @Output() done = new EventEmitter<boolean>();
  tutorial_sequence: number;
  
  all_sites: any;
  newSiteObject: INewSiteDto;
  current_site_id: number;
  open_new_site_form:boolean;

  //validation messages
  reserved_url_error_flag: boolean;
  invalid_url_error_flag: boolean;
  blank_url_error_flag: boolean;
  url_exists_error_flag: boolean;

  //backend validation
  backend_validation_error:string;

  constructor(private _httpService:HttpService) { }

  ngOnInit() {
    this.current_site_id = 0; //non SQL id default value
    this.all_sites = [];

    if(!this.is_tutorial){
      this.getSitesByAdminFromService();
    }

    this.initialiseNewSite();

    //tutorial related
    if(this.is_tutorial){
      this.tutorial_sequence = 1;
    }else{
      this.tutorial_sequence = 0;
    }

    //validation error flags
    this.resetValidators();
  }

  initialiseNewSite(){
    this.open_new_site_form = false;
    
    this.newSiteObject = {
      title : "",
      url: "",
      admin_id: this.current_admin_id,
      token: this.current_admin_token
    };
  }

  
  validateURL(){
    let re = /^[a-zA-ZäöüßÄÖÜ]+$/;
    return re.test(String(this.newSiteObject.url));
  }

  resetValidators(){
    this.backend_validation_error = "";
    
    this.blank_url_error_flag = false;
    this.reserved_url_error_flag = false;
    this.invalid_url_error_flag = false;
    this.url_exists_error_flag = false;
  }

  validateSite(){
    this.resetValidators();
    let errors = 0;
    if(this.newSiteObject.url == ""){ //title may be blank, url may not
      errors += 1;
      this.blank_url_error_flag = true;
    }else if(!this.validateURL() ){ //check invalid if not blank
      errors += 1;
      this.invalid_url_error_flag = true;
    }

    if(this.newSiteObject.url.toLowerCase() == "app"){
      errors += 1;
      this.reserved_url_error_flag = true;
    }

    if(this.newSiteObject.url.toLowerCase() == "default"){
      errors += 1;
      this.reserved_url_error_flag = true;
    }

    if(this.newSiteObject.url.toLowerCase() == "api"){
      errors += 1;
      this.reserved_url_error_flag = true;
    }

    if(this.newSiteObject.url.toLowerCase() == "swagger"){
      errors += 1;
      this.reserved_url_error_flag = true;
    }

    if(errors > 0 ){
      return false;
    }else{
      return true;
    }
  }

  //creates a new site without any content
  postSiteToService(){
    if(this.validateSite()){
      if(this.is_tutorial && this.tutorial_sequence == 3){
        this.all_sites = [];
        this.all_sites.push(this.newSiteObject);
        this.iterateTutorial();
      }else if(!this.is_tutorial){

        this.newSiteObject.url = this.newSiteObject.url.toLowerCase();
        this._httpService.postSite(this.newSiteObject).subscribe(
        result => {
          let _server_response:any = result; //wow
          let server_response:JsonResponseDto = _server_response;

          //check backend for duplicate URL
          if(server_response.response.includes("success")){ 
            this.resetValidators();
            this.initialiseNewSite();
          }else{
            this.url_exists_error_flag = true;
          }
          this.getSitesByAdminFromService();
        }, error => this.backend_validation_error = error.error);
      }else{
        //innert tutorial button
      }
    }else{
      //invalid, see errors
    }
  }

  //requests all sites owned by a specific admin 

  getSitesByAdminFromService(){
    this._httpService.getSitesByAdmin(this.current_admin_id, this.current_admin_token).subscribe(results => 
    {
      this.all_sites = results;
    }, error => this.backend_validation_error = error.error);
  }

  deleteSiteById(site_id:number,){
    if(!this.is_tutorial){
      this._httpService.deleteSite(site_id, this.current_admin_id, this.current_admin_token).subscribe(result =>{
        this.getSitesByAdminFromService();
      }, error => this.backend_validation_error = error.error);
    }
  }

  //operational
  toggleNewSiteForm(){
    this.open_new_site_form = ! this.open_new_site_form;
    if(this.is_tutorial && this.tutorial_sequence == 2){
      this.iterateTutorial();
    }
  }

  //tutorial related
  iterateTutorial(){
    if(this.is_tutorial){
      this.tutorial_sequence += 1;
    }
  }

  //unused method
  editSite(current_site_id:number){
    if(this.is_tutorial){
      this.done.emit(true);
    }else{
      this.current_site_id = current_site_id;
      this.siteIdSelectEvent.emit(current_site_id);
    }
  }
}


