import { Component, OnInit, Input } from '@angular/core';
import{ HttpService } from '../../../services/http/http.service';
import { ParagraphBox } from 'src/app/interfaces/dtos/site_components/paragraph_box';

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
      console.log("Component ID: "+this.component_id+" Site Id: "+this.site_id);
      this._httpService.getParagraphBox(this.component_id).subscribe((data) =>{
        this.pbox_object = data;
      });
    }
  }

}
