import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IComponentRequestDto } from 'src/app/interfaces/dtos/formatted_sites/component_request_dto';
import { Image } from 'src/app/interfaces/dtos/site_components/Image';
import { BSfourConverterService } from 'src/app/services/b-sfour-converter/b-sfour-converter.service';
import { HttpService } from 'src/app/services/http/http.service';
import { ValidationService } from 'src/app/services/validation/validation.service';

@Component({
  selector: 'app-image-editor',
  templateUrl: './image-editor.component.html',
  styleUrls: ['./image-editor.component.css']
})
export class ImageEditorComponent implements OnInit {

  constructor(private _httpService:HttpService,
    public validator:ValidationService,
    private b64converter:BSfourConverterService) { }

  @Input() admin_id:number;
  @Input() admin_token:string;
  @Input() site_id:number;
  @Input() image_id:number;
  @Output() deleteEvent = new EventEmitter<boolean>();
  image:Image;
  image_edits:Image;
  toggle_edit:boolean;
  toggle_delete:boolean;

  ngOnInit(): void {
    this.getImage();
  }

  getImage(){
    this.toggle_edit = false;
    this.toggle_delete = false;
    this.image = null;
    this.image_edits = null;

    this._httpService.getImage(this.image_id).subscribe(res => {
      this.image = {
        title:res.title,
        priority:res.priority,
        site_id:res.site_id,
        image_src:res.image_src
      };
      this.image_edits = res;
    });
  }

  editImage(){
    this._httpService.editImage(this.image_edits, this.admin_id, this.admin_token, this.site_id).subscribe(res => {
      this.image = {
        title:res.title,
        priority:res.priority,
        site_id:res.site_id,
        image_src:res.image_src
      };
      this.image_edits = res;
      this.toggle_edit = false;
    });
  }

  deleteSiteComponentByIdAndType(){
    this._httpService.deleteSiteComponent(this.image_id, "image", this.admin_id, this.admin_token, this.site_id).subscribe(result =>{
      this.deleteEvent.emit(true);
    });  
  }

  toggleEdit(){
    this.toggle_edit = !this.toggle_edit;
  }

  toggleDelete(){
    this.toggle_delete = !this.toggle_delete;
  }

   //Image Conversion Methods
   fileConversionListener($event) : void {
    if($event.target != null){ 
      this.b64converter.setImageBase64($event.target, this);
    }
  };

  //for use with setImageBase64() required for async data retrieval
  B64Callback(output_string: string, this_component:ImageEditorComponent){
    if(output_string === "invalid_file_size"){
      console.log("invalid file size");
      this_component.validator.image_src_invalid_size_flag = true;
    }else if(output_string === "invalid_file_type"){
      this_component.validator.image_src_invalid_flag = true;
    }else{
      this_component.image_edits.image_src = output_string;
      this_component.validator.image_src_invalid_flag = false;
      this_component.validator.image_src_invalid_size_flag = false;
    }
  }

}
