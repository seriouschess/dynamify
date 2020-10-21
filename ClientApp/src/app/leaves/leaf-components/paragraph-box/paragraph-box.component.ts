import { Component, OnInit, Input } from '@angular/core';
import{ HttpService } from '../../../services/http/http.service';
import{ IComponentRequestDto } from '../../../interfaces/dtos/formatted_sites/component_request_dto';
import { ParagraphBox } from 'src/app/interfaces/dtos/site_components/ParagraphBox';

@Component({
  selector: 'app-paragraph-box',
  templateUrl: './paragraph-box.component.html',
  styleUrls: ['./paragraph-box.component.css']
})
export class ParagraphBoxComponent implements OnInit {

  @Input() component_id:number;
  @Input() site_id:number;
  @Input() pbox_object: ParagraphBox;

  constructor( private _httpService:HttpService ) { }

  ngOnInit() {
    if(this.pbox_object == null){ //tutorial inputs pbox_object, null means call api for one
      let request:IComponentRequestDto = {
        component_id: this.component_id,
        site_id: this.site_id
      }
      console.log("Component ID: "+this.component_id+" Site Id: "+this.site_id);
      this._httpService.getParagraphBox(request).subscribe((data) =>{
        this.pbox_object = data;
      });
    }
  }

}
