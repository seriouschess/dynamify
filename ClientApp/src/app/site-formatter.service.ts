
import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { ISiteContentDto } from './interfaces/dtos/site_content_dto';
import { ISiteFormatted } from './interfaces/formatted_site_content';
import { ISiteRequestDto } from './interfaces/dtos/site_request_dto';


@Injectable({
  providedIn: 'root'
})
export class SiteFormatterService {

  constructor(private _httpService:HttpService) { }

  getSiteByIdFormatted(request:ISiteRequestDto, callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called:any){ //object which called is an angular component object
    this._httpService.getSite(request).subscribe(data => format(data,callback, object_which_called));
  }

  getActiveSiteFormatted(callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called){
    this._httpService.getActiveSite().subscribe(data => format(data,callback, object_which_called));
  }
}

//Updates an angular component with an ISiteFormatted sorted by priority
function format(data:any, callback: (parameter:ISiteFormatted, object_which_called:any) => void, object_which_called){
  var s:any = data; //just for now I swear!
    var unformatted_site:ISiteContentDto = {
      title: s.title,
      paragraph_boxes: s.paragraph_boxes,
      images: s.images,
      two_column_boxes: s.two_column_boxes,
      portraits: s.portraits
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
    }

    sorted_list_of_site_components.sort((a, b) => (a.priority > b.priority) ? 1 : -1)

    var formatted_site: ISiteFormatted = {
      title: unformatted_site.title,
      site_components: sorted_list_of_site_components
    }
      console.log(formatted_site);

      callback(formatted_site, object_which_called);
}


