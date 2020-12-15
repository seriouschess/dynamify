import { Component, OnInit, Input } from '@angular/core';
import { Image } from 'src/app/interfaces/dtos/site_components/image';
import{ HttpService } from '../../../services/http/http.service';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css']
})
export class ImageComponent implements OnInit {

  @Input() image_object:Image;
  @Input() component_id:number;
  @Input() site_id:number;

  constructor(private _httpService:HttpService) { }

  ngOnInit() {
    if(this.image_object == null){ //makes api call if image is not provided
      this._httpService.getImage(this.component_id).subscribe((data) =>{
        this.image_object = data;
      });
    }
  }

}
