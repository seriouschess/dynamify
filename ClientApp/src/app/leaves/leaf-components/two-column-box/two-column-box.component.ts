import { Component, OnInit, Input } from '@angular/core';
import { TwoColumnBox } from '../../../interfaces/dtos/site_dtos';
import{ HttpService } from '../../../services/http/http.service';
import{ IComponentRequestDto } from '../../../interfaces/dtos/component_request_dto';

@Component({
  selector: 'app-two-column-box',
  templateUrl: './two-column-box.component.html',
  styleUrls: ['./two-column-box.component.css']
})
export class TwoColumnBoxComponent implements OnInit {

  tcb_object:TwoColumnBox;
  @Input() component_id:number;
  @Input() site_id:number;

  constructor(private _httpService:HttpService) { }

  ngOnInit() {
    this.tcb_object = null;
    let request:IComponentRequestDto = {
      component_id: this.component_id,
      site_id: this.site_id
    }
    console.log("component ID: "+this.component_id+" Site Id: "+this.site_id);
    this._httpService.getTwoColumnBox(request).subscribe((data) =>{
      this.tcb_object = data;
    });
  }

}
