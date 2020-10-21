import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IComponentRequestDto } from 'src/app/interfaces/dtos/formatted_sites/component_request_dto';
import { ParagraphBox } from 'src/app/interfaces/dtos/site_components/ParagraphBox';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-paragraph-box-editor',
  templateUrl: './paragraph-box-editor.component.html',
  styleUrls: ['./paragraph-box-editor.component.css']
})
export class ParagraphBoxEditorComponent implements OnInit {

  constructor(private _httpService:HttpService) { }

  @Input() admin_id:number;
  @Input() admin_token:string;
  @Input() site_id:number;
  @Input() pbox_id:number;
  @Output() deleteEvent = new EventEmitter<boolean>();
  paragraph_box:ParagraphBox;
  paragraph_box_edits:ParagraphBox;
  toggle_edit:boolean;

  ngOnInit(): void {
    this.getParagraphBox();
  }

  getParagraphBox(){
    this.toggle_edit = false;
    this.paragraph_box = null;
    this.paragraph_box_edits = null;
    let thing:IComponentRequestDto = {
      component_id: this.pbox_id,
      site_id: this.site_id
    }

    this._httpService.getParagraphBox(thing).subscribe(res => {
      this.paragraph_box = {
        title:res.title,
        priority:res.priority,
        site_id:res.site_id,
        content:res.content
      };
      this.paragraph_box_edits = res;
    });
  }

  editParagraphBox(){
    this._httpService.editParagraphBox(this.paragraph_box_edits, this.admin_id, this.admin_token, this.site_id).subscribe(res => {
      this.paragraph_box = res;
      this.paragraph_box_edits = res;
    });
  }

  deleteSiteComponentByIdAndType(){
    this._httpService.deleteSiteComponent(this.pbox_id, "p_box", this.admin_id, this.admin_token).subscribe(result =>{
      this.deleteEvent.emit(true);
    });  
  }

  toggleEdit(){
    this.toggle_edit = !this.toggle_edit;
  }
}
