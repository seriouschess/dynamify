
import { Component, OnInit, Input } from '@angular/core';
import { NavBar } from 'src/app/interfaces/dtos/site_components/NavBar';
import { NewNavLinkDto } from 'src/app/interfaces/dtos/site_components/NewNavLinKDto';
import { HttpService } from 'src/app/services/http/http.service';
import { ValidationService } from 'src/app/services/validation/validation.service';

@Component({
  selector: 'app-leaf-nav-bar-editor',
  templateUrl: './leaf-nav-bar-editor.component.html',
  styleUrls: ['./leaf-nav-bar-editor.component.css']
})
export class LeafNavBarEditorComponent implements OnInit {

  nav_bar: NavBar;
  @Input() site_id:number;
 
  isExpanded = false;

  //editor related
  new_nav_bar: NavBar;
  new_nav_link:NewNavLinkDto;

  @Input() nav_bar_editor_open:boolean;

  @Input() current_admin_id:number;
  @Input() current_admin_token:string;
  @Input() current_site_id:number;

  //misc
  curlies: string; //have to do this because of the way angular templates handles curlies

  constructor(
    private _httpService:HttpService,
    public validator:ValidationService) { }

  ngOnInit() {
    this.curlies = "{ or }";
    this.nav_bar = null;
    this.resetNewNavLink();
    this.requestNavBar();
  }

  //Nav Bar CRUD

  requestNavBar(){
    this._httpService.getNavBar(this.site_id).subscribe( res => {
      this.nav_bar = res
      if(this.nav_bar != null){
        for(let i=0; i<this.nav_bar.links.length ;i++){
          let temp_url = this.nav_bar.links[i].url;
          temp_url = this.formatURL(temp_url);
        }
      }
    }, err => {
      this.nav_bar = null;
    });
  }

  addLinkToBar(){
    if(this.validator.validateNavBarLink(this.new_nav_link)){
      this._httpService.postNavLink(this.new_nav_link, this.current_admin_id, this.current_admin_token, this.current_site_id).subscribe( res =>{
        this.requestNavBar();
        this.resetNewNavLink();
      });
    }
  }

  deleteNavBar(){
    this._httpService.deleteNavBar( this.current_admin_id, this.current_admin_token, this.current_site_id ).subscribe( res =>{
      this.requestNavBar();
    }, err =>{
      console.log(err);
      this.requestNavBar();
    });
  }

  deleteNavBarLink(link_id: number){
    this._httpService.deleteNavLink( this.current_admin_id, this.current_admin_token, this.current_site_id, link_id ).subscribe( res =>{
      this.requestNavBar();
    });
  }

  createNavBar(){
    if(true){ //additional validators required?
      this._httpService.postNavBar(this.current_admin_id, this.current_admin_token, this.site_id).subscribe(results =>{
        this.requestNavBar();
        this.nav_bar_editor_open;
      }, error => console.log(error)); 
    }
  }

  //misc helpers

  resetNewNavLink(){
    this.new_nav_link ={
      url: "",
      label: ""
    }
  }

  formatURL(input_url:string){
    let is_http = input_url.indexOf('http://') !== -1 //true
    let is_https = input_url.indexOf('https://') !== -1 //true
    if(is_http || is_https){
      return input_url;
    }else{
      return "http://"+input_url;
    }
  }
  
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

}
