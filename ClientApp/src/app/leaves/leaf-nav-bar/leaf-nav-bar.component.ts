import { Component, OnInit, Input } from '@angular/core';
import { NavBar } from 'src/app/interfaces/dtos/site_components/NavBar';
import { HttpService } from 'src/app/services/http/http.service';
import { ValidationService } from 'src/app/services/validation/validation.service';

@Component({
  selector: 'app-leaf-nav-bar',
  templateUrl: './leaf-nav-bar.component.html',
  styleUrls: ['./leaf-nav-bar.component.css']
})
export class LeafNavBarComponent implements OnInit {

  nav_bar: NavBar;
  @Input() site_id:number;
 
  isExpanded = false;

  @Input() nav_bar_editor_open:boolean;

  //misc
  curlies: string; //have to do this because of the way angular templates handles curlies

  constructor(
    private _httpService:HttpService,
    public validator:ValidationService) { }

  ngOnInit() {
    this.curlies = "{ or }";
    this.nav_bar = null;
    this.requestNavBar();
  }

  //Nav Bar CRUD

  requestNavBar(){
    this._httpService.getNavBar(this.site_id).subscribe( res => {
      this.nav_bar = res
      if(this.nav_bar != null){
        for(let i=0; i<this.nav_bar.links.length ;i++){
          let temp_url = this.nav_bar.links[i].url;
          this.nav_bar.links[i].url = this.formatURL(temp_url);
        }
      }
    }, err => {
      this.nav_bar = null;
    });
  }

  //misc helpers
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
