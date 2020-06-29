import { Component, OnInit, Input } from '@angular/core';
import { LinkBox } from '../../../interfaces/dtos/site_dtos';
import{ HttpService } from '../../../services/http/http.service';
import{ IComponentRequestDto } from '../../../interfaces/dtos/component_request_dto';

@Component({
  selector: 'app-link-box',
  templateUrl: './link-box.component.html',
  styleUrls: ['./link-box.component.css']
})
export class LinkBoxComponent implements OnInit {

  link_box_object: LinkBox;
  @Input() component_id:number;
  @Input() site_id:number;

  constructor(private _httpService:HttpService) { }

  ngOnInit() {
    this.link_box_object = null;
    let request:IComponentRequestDto = {
      component_id: this.component_id,
      site_id: this.site_id
    }
    console.log("component ID: "+this.component_id+" Site Id: "+this.site_id);
    this._httpService.getLinkBox(request).subscribe((data) =>{
      this.link_box_object = data;
      this.formatURL();
    });
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
