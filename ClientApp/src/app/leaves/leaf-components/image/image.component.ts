import { Component, OnInit, Input } from '@angular/core';
import { Image } from '../../../interfaces/dtos/site_dtos';
import{ HttpService } from '../../../services/http/http.service';
import{ IComponentRequestDto } from '../../../interfaces/dtos/component_request_dto';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css']
})
export class ImageComponent implements OnInit {

  image_object:Image;
  @Input() component_id:number;
  @Input() site_id:number;

  constructor(private _httpService:HttpService) { }

  ngOnInit() {
    this.image_object = null;
    let request:IComponentRequestDto = {
      component_id: this.component_id,
      site_id: this.site_id
    }
    this._httpService.getImage(request).subscribe((data) =>{
      this.image_object = data;
    });
  }

}
