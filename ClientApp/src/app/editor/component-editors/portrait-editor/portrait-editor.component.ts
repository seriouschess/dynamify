import { Component, EventEmitter, Input, OnInit, Output, ɵConsole } from '@angular/core';
import { IComponentRequestDto } from 'src/app/interfaces/dtos/formatted_sites/component_request_dto';
import { Portrait } from 'src/app/interfaces/dtos/site_components/portrait';
import { BSfourConverterService } from 'src/app/services/b-sfour-converter/b-sfour-converter.service';
import { HttpService } from 'src/app/services/http/http.service';
import { ValidationService } from 'src/app/services/validation/validation.service';


@Component({
  selector: 'app-portrait-editor',
  templateUrl: './portrait-editor.component.html',
  styleUrls: ['./portrait-editor.component.css']
})
export class PortraitEditorComponent implements OnInit {

  constructor(private _httpService:HttpService,
    public validator:ValidationService,
    private b64converter:BSfourConverterService) { }

  @Input() admin_id:number;
  @Input() admin_token:string;
  @Input() site_id:number;
  @Input() portrait_id:number;
  @Output() deleteEvent = new EventEmitter<boolean>();
  portrait:Portrait;
  portrait_edits:Portrait;
  toggle_edit:boolean;
  toggle_delete:boolean;

  ngOnInit(): void {
    this.getportrait();
  }

  getportrait(){
    this.toggle_edit = false;
    this.toggle_delete = false;
    this.portrait = null;
    this.portrait_edits = null;

    this._httpService.getPortrait(this.portrait_id).subscribe(res => {
      this.portrait = {
        title:res.title,
        priority:res.priority,
        site_id:res.site_id,
        image_src:res.image_src,
        content:res.content
      };
      this.portrait_edits = res;
    });
  }

  editPortrait(){
    this._httpService.editPortrait(this.portrait_edits, this.admin_id, this.admin_token, this.site_id).subscribe(res => {
      this.portrait = {
        title:res.title,
        priority:res.priority,
        site_id:res.site_id,
        image_src:res.image_src,
        content:res.content
      };
      this.portrait_edits = res;
      this.toggle_edit = false;
    });
  }

  deleteSiteComponentByIdAndType(){
    this._httpService.deleteSiteComponent(this.portrait_id, "portrait", this.admin_id, this.admin_token, this.site_id).subscribe(result =>{
      this.deleteEvent.emit(true);
    });  
  }

  toggleEdit(){
    this.toggle_edit = !this.toggle_edit;
  }

  toggleDelete(){
    this.toggle_delete = !this.toggle_delete;
  }

   //portrait Conversion Methods
   fileConversionListener($event) : void {
    if($event.target != null){
      this.b64converter.setImageBase64($event.target, this);
    }
  };

  //for use with setportraitBase64() required for async data retrieval
  B64Callback(output_string: string, this_component:PortraitEditorComponent){
    if(output_string === "invalid_file_size"){
      this_component.validator.image_src_invalid_size_flag = true;
    }else if(output_string === "invalid_file_type"){
      this_component.validator.image_src_invalid_flag = true;
    }else{
      this_component.portrait_edits.image_src = output_string;
      this_component.validator.image_src_invalid_flag = false;
      this_component.validator.image_src_invalid_size_flag = false;
    }
  }

}
