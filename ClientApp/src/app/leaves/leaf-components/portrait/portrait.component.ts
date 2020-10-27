import { Component, OnInit, Input } from '@angular/core';
import{ HttpService } from '../../../services/http/http.service';
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
      this._httpService.getPortrait(this.component_id).subscribe((data) =>{
        this.portrait_object = data;
      });
    }
  }

}
