import { Component, OnInit, Input } from '@angular/core';
import { ParagraphBox } from '../../../interfaces/dtos/site_dtos';
import{ HttpService } from '../../../services/http/http.service';
import{ IComponentRequestDto } from '../../../interfaces/dtos/component_request_dto';

@Component({
  selector: 'app-paragraph-box',
  templateUrl: './paragraph-box.component.html',
  styleUrls: ['./paragraph-box.component.css']
})
export class ParagraphBoxComponent implements OnInit {

  @Input() component_id:number;
  @Input() site_id:number;
  pbox_object: ParagraphBox;

  constructor( private _httpService:HttpService ) { }

  ngOnInit() {
    this.pbox_object = null;
    let request:IComponentRequestDto = {
      component_id: this.component_id,
      site_id: this.site_id
    }
    console.log("component ID: "+this.component_id+" Site Id: "+this.site_id);
    this._httpService.getParagraphBox(request).subscribe((data) =>{
      this.pbox_object = data;
    });
  }

}
