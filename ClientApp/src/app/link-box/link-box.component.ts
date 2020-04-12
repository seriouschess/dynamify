import { Component, OnInit, Input } from '@angular/core';
import { LinkBox } from '../interfaces/dtos/site_dtos';

@Component({
  selector: 'app-link-box',
  templateUrl: './link-box.component.html',
  styleUrls: ['./link-box.component.css']
})
export class LinkBoxComponent implements OnInit {

  @Input() link_box_object: LinkBox;

  constructor() { }

  ngOnInit() {
    this.formatURL();
  }

  formatURL(){
    let is_http = this.link_box_object.url.indexOf('http://') !== -1 //true
    let is_https = this.link_box_object.url.indexOf('https://') !== -1 //true
    if(is_http || is_https){
      console.log(this.link_box_object.url);
    }else{
      this.link_box_object.url = "http://"+this.link_box_object.url;
      console.log(this.link_box_object.url);
    }
  }

}
