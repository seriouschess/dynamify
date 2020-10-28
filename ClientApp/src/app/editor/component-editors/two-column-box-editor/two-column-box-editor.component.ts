import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IComponentRequestDto } from 'src/app/interfaces/dtos/formatted_sites/component_request_dto';
import { TwoColumnBox } from 'src/app/interfaces/dtos/site_components/TwoColumnBox';
import { HttpService } from 'src/app/services/http/http.service';
import { ValidationService } from 'src/app/services/validation/validation.service';

@Component({
  selector: 'app-two-column-box-editor',
  templateUrl: './two-column-box-editor.component.html',
  styleUrls: ['./two-column-box-editor.component.css']
})
export class TwoColumnBoxEditorComponent implements OnInit {

  constructor(private _httpService:HttpService,
    public validator:ValidationService) { }

  @Input() admin_id:number;
  @Input() admin_token:string;
  @Input() site_id:number;
  @Input() two_column_box_id:number;
  @Output() deleteEvent = new EventEmitter<boolean>();
  two_column_box:TwoColumnBox;
  two_column_box_edits:TwoColumnBox;
  toggle_edit:boolean;
  toggle_delete:boolean;

  ngOnInit(): void {
    this.getTwoColumnBox();
  }

  getTwoColumnBox(){
    this.toggle_edit = false;
    this.toggle_delete = false;
    this.two_column_box = null;
    this.two_column_box_edits = null;

    this._httpService.getTwoColumnBox(this.two_column_box_id).subscribe(res => {
      this.two_column_box = {
        title:res.title,
        priority:res.priority,
        site_id:res.site_id,
        heading_one: res.heading_one,
        heading_two: res.heading_two,
        content_one: res.content_one,
        content_two: res.content_two
      };
      this.two_column_box_edits = res;
    });
  }

  editTwoColumnBox(){
    this._httpService.editTwoColumnBox(this.two_column_box_edits, this.admin_id, this.admin_token, this.site_id).subscribe(res => {
      this.two_column_box = res;
      this.two_column_box_edits = res;
      this.toggle_edit = false;
    });
  }

  deleteSiteComponentByIdAndType(){
    this._httpService.deleteSiteComponent(this.two_column_box_id, "2c_box", this.admin_id, this.admin_token, this.site_id).subscribe(result =>{
      this.deleteEvent.emit(true);
    });  
  }

  toggleEdit(){
    this.toggle_edit = !this.toggle_edit;
  }

  toggleDelete(){
    this.toggle_delete = !this.toggle_delete;
  }
}