import { Component, OnInit, Input } from '@angular/core';
import{ HttpService } from '../../../services/http/http.service';
import{ IComponentRequestDto } from '../../../interfaces/dtos/formatted_sites/component_request_dto';
import { Portrait } from 'src/app/interfaces/dtos/site_components/Portrait';

@Component({
  selector: 'app-portrait',
  templateUrl: './portrait.component.html',
  styleUrls: ['./portrait.component.css']
})
export class PortraitComponent implements OnInit {

 @Input() portrait_object: Portrait;
 @Input() component_id:number;
 @Input() site_id:number;

  constructor( private _httpService:HttpService ) { }

  ngOnInit() {
    if(this.portrait_object == null){ //attempt to api call for object if not provided
      let request:IComponentRequestDto = {
        component_id: this.component_id,
        site_id: this.site_id
      }
      console.log("component ID: "+this.component_id+" Site Id: "+this.site_id);
      this._httpService.getPortrait(request).subscribe((data) =>{
        this.portrait_object = data;
      });
    }
  }

}
