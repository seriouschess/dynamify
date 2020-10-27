import { Component, OnInit, Input } from '@angular/core';
import{ HttpService } from '../../../services/http/http.service';
import { TwoColumnBox } from 'src/app/interfaces/dtos/site_components/TwoColumnBox';

@Component({
  selector: 'app-two-column-box',
  templateUrl: './two-column-box.component.html',
  styleUrls: ['./two-column-box.component.css']
})
export class TwoColumnBoxComponent implements OnInit {

  @Input() tcb_object:TwoColumnBox;
  @Input() component_id:number;
  @Input() site_id:number;

  constructor(private _httpService:HttpService) { }

  ngOnInit() {
    if(this.tcb_object == null){ //attempt to api call for object if not provided
      this._httpService.getTwoColumnBox(this.component_id).subscribe((data) =>{
        this.tcb_object = data;
      });
    }
  }
}
