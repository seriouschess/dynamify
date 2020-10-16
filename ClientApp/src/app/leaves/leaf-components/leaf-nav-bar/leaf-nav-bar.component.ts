
import { Component, OnInit, Input } from '@angular/core';
import { NavBar } from 'src/app/interfaces/dtos/site_components/NavBar';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-leaf-nav-bar',
  templateUrl: './leaf-nav-bar.component.html',
  styleUrls: ['./leaf-nav-bar.component.css']
})
export class LeafNavBarComponent implements OnInit {

  nav_bar: NavBar;
  @Input() site_id:number;
  @Input() is_edit_mode:boolean;
  isExpanded = false;

  constructor( private _httpClient:HttpService) { }

  ngOnInit() {
    this.nav_bar = null;
    this.requestNavBar();
  }

  requestNavBar(){
    this._httpClient.getNavBar(this.site_id).subscribe( res => {
      this.nav_bar = res
      for(let i=0; i<this.nav_bar.links.length ;i++){
        let temp_url = this.nav_bar.links[i].url;
        temp_url = this.formatURL(temp_url);
      }
    }, err => {
      console.log(err);
    });
  }

  formatURL(input_url){
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
