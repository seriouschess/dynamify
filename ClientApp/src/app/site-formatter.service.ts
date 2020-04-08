
import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { ISiteContentDto } from './interfaces/dtos/site_content_dto';
import { ISiteFormatted } from './interfaces/formatted_site_content';
import { ISiteRequestDto } from './interfaces/dtos/site_request_dto';
import { IGenericSiteComponent } from './interfaces/generic_site_component';

@Injectable({
  providedIn: 'root'
})
export class SiteFormatterService {

  component_to_add:IGenericSiteComponent;

  constructor(private _httpService:HttpService) { }

  
  getSiteByIdFormatted(request:ISiteRequestDto, callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called:any){ //object which called is an angular component object
    this._httpService.getSite(request).subscribe(data => format(data,callback, object_which_called));
  }

  getActiveSiteFormatted(callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called){
    this._httpService.getActiveSite().subscribe(data => format(data,callback, object_which_called));
  }

  //methods used by tuorial
  getBlankSite(callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called){
    var formatted_site:ISiteFormatted = {
      title: "Demo Site",
      site_components: []
    }

    callback(formatted_site, object_which_called);
  }

  sortSite(unsorted_content:ISiteFormatted, new_box:any, type:string, callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called){

    //add new component
    var generic_component:IGenericSiteComponent;
    generic_component = {
      priority: null,
      title: null,
      type: null,
      site_id: null,
      content: null,
      image_src: null,
  
      heading_one: null,
      heading_two: null,
      content_one: null,
      content_two: null,

      url: null,
      link_display: null
    }

    if(type == "p_box"){
      generic_component.type = "p_box";
      generic_component.title = new_box.title;
      generic_component.priority = unsorted_content.site_components.length*100 + 100; //higher priority means last read
      generic_component.content = new_box.content;
    }

    if(type == "2c_box"){
      generic_component.type = "2c_box";
      generic_component.title = new_box.title;
      generic_component.priority = unsorted_content.site_components.length*100 + 100;
      generic_component.heading_one = new_box.heading_one,
      generic_component.heading_two = new_box.heading_two,
      generic_component.content_one = new_box.content_one,
      generic_component.content_two = new_box.content_two
    }

    if(type == "image"){
      generic_component.type = "image";
      generic_component.title = new_box.title;
      generic_component.priority = unsorted_content.site_components.length*100 + 100;
      generic_component.image_src = new_box.image_src;
    }

    if(type == "portrait"){
      generic_component.type = "portrait";
      generic_component.title = new_box.title;
      generic_component.priority = unsorted_content.site_components.length*100 + 100;
      generic_component.image_src = new_box.image_src;
      generic_component.content = new_box.content;
    }

    if(type == "link_box"){
      generic_component.type = "link_box";
      generic_component.title = new_box.title;

      generic_component.content = new_box.content;
      generic_component.url = new_box.url;
      generic_component.link_display = new_box.link_display;
    }

    unsorted_content.site_components.push(generic_component);

    //sort components by priority
    return sortFormatted( unsorted_content, callback, object_which_called );
  }
}

//Updates an angular component with an ISiteFormatted sorted by priority
function format(data:any, callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called){
    var s:any = data; //just for now I swear!
    console.log(s);
    var unformatted_site:ISiteContentDto = {
      title: s.title,
      paragraph_boxes: s.paragraph_boxes,
      images: s.images,
      two_column_boxes: s.two_column_boxes,
      portraits: s.portraits,
      link_boxes: s.link_boxes
    }
   
    var sorted_list_of_site_components = [];

    for(var x=0; x<unformatted_site.paragraph_boxes.length; x++){
      sorted_list_of_site_components.push(unformatted_site.paragraph_boxes[x]);
    };
    for(var x=0; x<unformatted_site.portraits.length; x++){
      sorted_list_of_site_components.push(unformatted_site.portraits[x]);
    };
    for(var x=0; x<unformatted_site.two_column_boxes.length; x++){
      sorted_list_of_site_components.push(unformatted_site.two_column_boxes[x]);
    };
    for(var x=0; x<unformatted_site.images.length; x++){
      sorted_list_of_site_components.push(unformatted_site.images[x]);
    };
    for(var x=0; x<unformatted_site.link_boxes.length; x++){
      sorted_list_of_site_components.push(unformatted_site.link_boxes[x]);
    };

    sorted_list_of_site_components.sort((a, b) => (a.priority > b.priority) ? 1 : -1);

    var formatted_site: ISiteFormatted = {
      title: unformatted_site.title,
      site_components: sorted_list_of_site_components
    }
      console.log(formatted_site);

      //callback is used for the componentto recieve the data from the formatter.
      callback(formatted_site, object_which_called); 
}

//for use with site editor tutorial mode
function sortFormatted(unsorted_content:ISiteFormatted, callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called){
  let sorted_site = unsorted_content;
  sorted_site.site_components.sort((a, b) => (a.priority > b.priority) ? 1 : -1);
  callback(sorted_site, object_which_called);
}


