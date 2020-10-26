import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IComponentRequestDto } from 'src/app/interfaces/dtos/formatted_sites/component_request_dto';
import { LinkBox } from 'src/app/interfaces/dtos/site_components/LinkBox';
import { HttpService } from 'src/app/services/http/http.service';
import { ValidationService } from 'src/app/services/validation/validation.service';

@Component({
  selector: 'app-link-box-editor',
  templateUrl: './link-box-editor.component.html',
  styleUrls: ['./link-box-editor.component.css']
})
export class LinkBoxEditorComponent implements OnInit {

  constructor(private _httpService:HttpService,
    public validator:ValidationService) { }

  @Input() admin_id:number;
  @Input() admin_token:string;
  @Input() site_id:number;
  @Input() lbox_id:number;
  @Output() deleteEvent = new EventEmitter<boolean>();
  link_box:LinkBox;
  link_box_edits:LinkBox;
  toggle_edit:boolean;
  toggle_delete:boolean;
  link_display:boolean;

  ngOnInit(): void {
    this.getLinkBox();
  }

  getLinkBox(){
    this.link_display = true;
    this.toggle_edit = false;
    this.toggle_delete = false;
    this.link_box = null;
    this.link_box_edits = null;
    let thing:IComponentRequestDto = {
      component_id: this.lbox_id,
      site_id: this.site_id
    }

    this._httpService.getLinkBox(thing).subscribe(res => {
      this.link_box = {
        title:res.title,
        priority:res.priority,
        site_id:res.site_id,
        content:res.content,
        url:res.url,
        link_display:res.link_display
      };
      this.link_box_edits = res;
    });
  }

  editLinkBox(){
    this.validator.resetValidation();
    if(this.validator.validatePbox(this.link_box_edits)){
      this.link_display = false;
      this._httpService.editLinkBox(this.link_box_edits, this.admin_id, this.admin_token, this.site_id).subscribe(res => {
        this.link_box = res;
        this.link_box_edits = res;
        this.toggle_edit = false;
        this.link_display = true;
      }, err =>{
        console.log(err);
        this.link_display = true;
      });
    }
  }

  deleteSiteComponentByIdAndType(){
    this._httpService.deleteSiteComponent(this.lbox_id, "link_box", this.admin_id, this.admin_token).subscribe(result =>{
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