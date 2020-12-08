import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IUpdatedSiteDto } from 'src/app/interfaces/dtos/formatted_sites/update_site_dto';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-site-title-editor',
  templateUrl: './site-title-editor.component.html',
  styleUrls: ['./site-title-editor.component.css']
})
export class SiteTitleEditorComponent implements OnInit {

  @Input() current_site_id:number;
  @Input() current_admin_id:number;
  @Input() current_admin_token:string;
  @Input() current_site_title:string;
  @Output() output_title:EventEmitter<string> = new EventEmitter<string>();

  backend_site_edit_error:string;
  open_editor:boolean;

  updated_site:IUpdatedSiteDto;
  updated_title:string;

  constructor(private _httpService:HttpService) { }

  ngOnInit(): void {
    this.open_editor = false;
    this.updated_title = "";
    this.updated_site = {
      admin_id: this.current_admin_id,
      site_id: this.current_site_id,
      title: this.updated_title
    }
  }

  editSiteTitle(){
    this.updated_site.title = this.updated_title;
    this.backend_site_edit_error = "";
    this._httpService.editSiteTitle(this.updated_site, this.current_admin_token).subscribe( res =>{
      this.current_site_title = res.title;
      this.output_title.emit(res.title);
      this.backend_site_edit_error = "";
      this.toggleEditor();
    }, err => this.backend_site_edit_error = err.error );
  }

  editBlankTitle(){
    this.updated_title = "";
    this.editSiteTitle();
  }

  toggleEditor(){
    this.open_editor = !this.open_editor;
  }

}
