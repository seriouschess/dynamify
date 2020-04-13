//base64 conversion module

import { Injectable } from '@angular/core';
import { SiteEditorComponent } from '../../editor/site-editor/site-editor.component';

@Injectable({
  providedIn: 'root'
})
export class BSfourConverterService {
  image_converter_working: boolean;

  constructor() { 
    this.image_converter_working = false;
  }

  
    setImageBase64(inputValue: any, residing_component:SiteEditorComponent) : void {
      this.image_converter_working == true;
      var file:File = inputValue.files[0]; 
      var reader:FileReader = new FileReader();
  
      reader.readAsDataURL(file);
      let valid = false;

      console.log(residing_component);
  
      //cheating
      //let this_component_object:SiteEditorComponent = residing_component; //used for callback function
      console.log(residing_component);
      let callback = residing_component.B64Callback;
      let current_component = residing_component;
      console.log(callback);
  
      reader.onload = function() {
        let file_base_64:string = reader.result+"";
  
        //validate file type
        for(var x=0; x<100 ;x++){
  
          //validate file type
          if(file_base_64[x] == "j"){ //check jpg
            if(file_base_64[x+1] == "p" && file_base_64[x+2] == "e" && file_base_64[x+3] == "g" ){
              valid = true;
            }
          }

          if( file_base_64[x] == "p" ){ //check png
            if(file_base_64[x+1] == "n" && file_base_64[x+2] == "g"){
              valid = true;
            }
          }
        }
        if(valid == true){
          console.log("Base64 String: "+file_base_64);
          callback(file_base_64, residing_component);
        }else{ //invalid file type
          console.log("invalid_file_type");
          callback("invalid_file_type", residing_component);
        }
      }
      reader.onerror = function (error){
        console.log('File read error: ', error);
      }
      
      };
}
