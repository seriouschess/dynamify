//Angular resources
import { Injectable } from '@angular/core';
import{ HttpClient } from '@angular/common/http';

//dto imports
import { Admin } from './interfaces/dtos/admin_dtos';
import { Login } from './interfaces/dtos/login_dto';
import { ParagraphBox, Image, Portrait, TwoColumnBox, LinkBox } from './interfaces/dtos/site_dtos';
import { ComponentReference } from './interfaces/dtos/component_reference';
import { INewSiteDto } from './interfaces/dtos/new_site_dto';
import { ISiteRequestDto } from './interfaces/dtos/site_request_dto';
import { ISiteContentDto } from './interfaces/dtos/site_content_dto';

@Injectable({
  providedIn: 'root'
})

export class HttpService {
  // @Inject('BASE_URL') baseUrl: string
  constructor(private _http:HttpClient) { }

  //admin services

  getAdmins<Admin>(){
    var data:any = this._http.get<Admin[]>('admin');
    return data;
  }

  loginAdmin(login_payload:Login){
    return this._http.post(`api/admin/login`, login_payload);
  }

  postAdmin<AdminRegistrationDto>(NewAdmin:AdminRegistrationDto){
   console.log(JSON.stringify(NewAdmin));
   return this._http.post<Admin>('api/admin', NewAdmin);
  }

  deleteAdmin(admin_id:number, token:string){
    console.log(`Admin ID for deletion:${admin_id}`);
    let payload = {
      admin_id: admin_id,
      token: token
    }
    return this._http.post(`admin/${admin_id}`, payload);
  }

  editAdmin(AdminToEdit:Admin){
    console.log(JSON.stringify(AdminToEdit));
    return this._http.put('api/admin', AdminToEdit);
  }

  //site services

  getSite<ISiteContentDto>(request:ISiteRequestDto){
    return this._http.post<ISiteContentDto>(`api/site/get`, request);
  }

  getActiveSite<ISiteContentDto>(){
    return this._http.get<ISiteContentDto>('api/site/active');
  }

  getSitesByAdmin(admin_id: number, admin_token: string) {
    return this._http.get(`api/site/get_by_admin/${admin_id}/${admin_token}`);
  }

  deleteSite(site_id: number, admin_id:number, admin_token:string){
    return this._http.delete(`api/site/delete/${site_id}/${admin_id}/${admin_token}`);
  }

  postSite(input_site: INewSiteDto){
    return this._http.post(`api/site/create_site`, input_site);
  }

  setActiveSite(request: ISiteRequestDto){
    return this._http.post(`api/site/set_active`, request);
  }
  
  //site configuration services
  deleteSiteComponent(component_id:number, component_type:string, admin_id:number, admin_token:string){
    var component_reference: ComponentReference = {
      component_id: component_id,
      component_type:component_type
    }
    return this._http.post(`api/site/delete/site_component/${admin_id}/${admin_token}`, component_reference);
  }

  postParagraphBox(paragraph_box: ParagraphBox, admin_id:number, admin_token: string){
    return this._http.post(`api/site/create/paragraph_box/${admin_id}/${admin_token}`, paragraph_box);
  }

  postImage(image: Image, admin_id: number, admin_token:string){
    return this._http.post(`api/site/create/image/${admin_id}/${admin_token}`, image);
  }

  postPortrait(portrait: Portrait, admin_id: number, admin_token: string){
    return this._http.post(`api/site/create/portrait/${admin_id}/${admin_token}`, portrait);
  }

  postTwoColumnBox(two_column_box: TwoColumnBox, admin_id: number, admin_token:string){
    return this._http.post(`api/site/create/2c_box/${admin_id}/${admin_token}`, two_column_box);
  }

  postLinkBox(link_box: LinkBox, admin_id: number, admin_token:string){
    console.log(link_box);
    return this._http.post(`api/site/create/link_box/${admin_id}/${admin_token}`, link_box);
  }
}



