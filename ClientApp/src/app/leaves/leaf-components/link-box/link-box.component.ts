import { Component, OnInit, Input } from '@angular/core';
import{ HttpService } from '../../../services/http/http.service';
import{ IComponentRequestDto } from '../../../interfaces/dtos/formatted_sites/component_request_dto';
import { LinkBox } from 'src/app/interfaces/dtos/site_components/link_box';

@Component({
  selector: 'app-link-box',
  templateUrl: './link-box.component.html',
  styleUrls: ['./link-box.component.css']
})
export class LinkBoxComponent implements OnInit {

  @Input() link_box_object: LinkBox;
  @Input() component_id:number;
  @Input() site_id:number;

  constructor(private _httpService:HttpService) { }

  ngOnInit() {
    if(this.link_box_object == null){
      this.getLinkBox();
    }else{
      this.formatURL();
    }
  }

  getLinkBox(){
    this._httpService.getLinkBox(this.component_id).subscribe((data) =>{
      this.link_box_object = data;
      this.formatURL();
    });
  }

  formatURL(){
    let is_http = this.link_box_object.url.indexOf('http://') !== -1 //true
    let is_https = this.link_box_object.url.indexOf('https://') !== -1 //true
    if(!is_http && !is_https){
      this.link_box_object.url = "http://"+this.link_box_object.url;
    }
  }

}
