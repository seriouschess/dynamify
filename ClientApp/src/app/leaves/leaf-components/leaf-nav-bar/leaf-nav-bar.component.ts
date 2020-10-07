import { Component, OnInit, Input } from '@angular/core';
import { NavBar } from 'src/app/interfaces/dtos/site_components/NavBar';

@Component({
  selector: 'app-leaf-nav-bar',
  templateUrl: './leaf-nav-bar.component.html',
  styleUrls: ['./leaf-nav-bar.component.css']
})
export class LeafNavBarComponent implements OnInit {

  @Input() nav_bar_object: NavBar;
  @Input() is_edit_mode: boolean;
  isExpanded = false;

  constructor() { }

  ngOnInit() {
    if(this.nav_bar_object === null){
      this.nav_bar_object = { links:[], site_id:0 }
    }
    
    for(var x = 0; x<this.nav_bar_object.links.length ;x++){
      let url = this.nav_bar_object.links[x].url;
      this.nav_bar_object.links[x].url = this.formatURL(url);
    }

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
