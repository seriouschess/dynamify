import { Injectable } from '@angular/core';

import { ParagraphBox } from 'src/app/interfaces/dtos/site_components/paragraph_box';
import { Portrait } from 'src/app/interfaces/dtos/site_components/portrait';
import { TwoColumnBox } from 'src/app/interfaces/dtos/site_components/two_column_box';
import { LinkBox } from 'src/app/interfaces/dtos/site_components/link_box';
import { NewNavLinkDto } from 'src/app/interfaces/dtos/site_components/new_nav_link_dto';

@Injectable({
  providedIn: 'root'
})

export class ValidationService {

    //validation flags
    pbox_content_invalid_flag:boolean;
  
    image_src_invalid_flag:boolean; //used in portraits too
    image_src_invalid_size_flag:boolean;
  
    portrait_content_invalid_flag:boolean;
  
    tcb_content_one_invalid_flag:boolean;
    tcb_content_two_invalid_flag:boolean;

    link_box_url_invalid_flag:boolean;
    link_box_display_invalid_flag:boolean;

    nav_bar_invalid_url_flag:boolean;
    nav_bar_invalid_label_flag:boolean;

  constructor() {
    this.resetValidation();
   }

   resetValidation(){
    this.pbox_content_invalid_flag = false;

    this.image_src_invalid_flag = false;

    this.portrait_content_invalid_flag = false;

    this.tcb_content_one_invalid_flag = false;
    this.tcb_content_two_invalid_flag = false;

    this.link_box_url_invalid_flag = false;
    this.link_box_display_invalid_flag = false;

    this.nav_bar_invalid_url_flag = false;
    this.nav_bar_invalid_label_flag = false;
   }

   validatePbox(test_box:ParagraphBox){
    let error_count = 0;

    if(test_box.content == ""){
      this.pbox_content_invalid_flag = true;
      error_count += 1;
    }else{
      this.pbox_content_invalid_flag = false;
    }
    
    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }

  validateImage(image_src:string){
    let error_count = 0;

    if(image_src === ""){ 
      this.image_src_invalid_flag = true;
      error_count += 1;
    }
    
    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }

  validateTwoColumnBox(two_c_box:TwoColumnBox){
    let error_count = 0;

    if(two_c_box.content_one == ""){
      this.tcb_content_one_invalid_flag = true;
      error_count += 1;
    }else{
      this.tcb_content_one_invalid_flag = false;
     }

    if(two_c_box.content_two == ""){
      this.tcb_content_two_invalid_flag = true;
      error_count += 1;
    }else{
      this.tcb_content_two_invalid_flag = false;
     }
    
    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }

  validatePortrait(test_portrait:Portrait, image_src:string){
    let error_count = 0;

    if(test_portrait.content == ""){
      this.portrait_content_invalid_flag = true;
      error_count += 1;
    }else{
      this.portrait_content_invalid_flag = false;
    }

    if(image_src === ""){
      this.image_src_invalid_flag = true;
      error_count += 1;
    }

    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }

  validateLinkBox(test_link_box:LinkBox){
    let error_count = 0;

    if(test_link_box.link_display === ""){
      error_count += 1;
      this.link_box_display_invalid_flag = true;
    }

    if(test_link_box.url === ""){
      this.link_box_url_invalid_flag = true;
    }

    if( test_link_box.url.indexOf(' ') !== -1){ //contains a space
      error_count += 1;
      this.link_box_url_invalid_flag = true;
    }

    if( test_link_box.url.indexOf('.') === -1){ //does not contain a period
      error_count += 1;
      this.link_box_url_invalid_flag = true;
    }

    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }

  validateNavBarLink(test_nav_link: NewNavLinkDto){
    let error_count = 0;

    if( test_nav_link.url == "" ){
      error_count += 1;
      this.nav_bar_invalid_url_flag = true;
    }

    if( test_nav_link.label == ""){
      error_count += 1;
      this.nav_bar_invalid_label_flag = true;
    }
    
    if( test_nav_link.url.indexOf(' ') !== -1){ //contains a space
      error_count += 1;
      this.nav_bar_invalid_url_flag = true;
    }

    if( test_nav_link.url.indexOf('.') === -1){ //does not contain a period
      error_count += 1;
      this.nav_bar_invalid_url_flag = true;
    }

    if(error_count > 0){
      return false;
    }else{
      return true;
    }
  }
}