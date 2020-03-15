//Angular resources
import { Injectable } from '@angular/core';
import{ HttpClient } from '@angular/common/http';

//dto imports
import { Admin } from './dtos/admin_dtos';
import { Login } from './dtos/login_dto';
import { ParagraphBox, Image, Portrait, TwoColumnBox, Site} from './dtos/site_dtos';
import { ComponentReference } from './dtos/component_reference';
import { INewSiteDto } from './dtos/new_site_dto';
import { IActiveSiteRequestDto } from './dtos/active_site_request_dto'

@Injectable({
  providedIn: 'root'
})

export class HttpService {
  // // @Inject('BASE_URL') baseUrl: string
   constructor(private _http:HttpClient) { }

  //admin services

  getAdmins<Admin>(){
    var data:any = this._http.get<Admin[]>('admin');
    return data;
  }

  getAdminByEmail(login_payload:Login){
    return this._http.get(`admin/by_email/${login_payload.email}/${login_payload.password}`);
  }

  postAdmin<Admin>(NewAdmin:Admin){
   console.log(JSON.stringify(NewAdmin));
   return this._http.post('admin', NewAdmin);
  }

  deleteAdmin(admin_id:Admin["admin_id"]){
    console.log(`Admin ID for deletion:${admin_id}`);
    return this._http.delete(`admin/${admin_id}`);
  }

  editAdmin(AdminToEdit:Admin){
    console.log(JSON.stringify(AdminToEdit));
    return this._http.put('admin', AdminToEdit);
  }

  //site services

  getSite(site_id: number){
    return this._http.get(`site/get/${site_id}`);
  }

  getActiveSite<Site>(){
    return this._http.get<Site>('site/get_active');
  }

  getSitesByAdmin(admin_id: number, admin_token: string) {
    return this._http.get(`site/get_by_admin/${admin_id}/${admin_token}`);
  }

  deleteSite(site_id: number){
    return this._http.delete(`site/delete/${site_id}`);
  }

  postSite(input_site: INewSiteDto){
    return this._http.post(`site/create_site`, input_site);
  }

  setActiveSite(request: IActiveSiteRequestDto){
    return this._http.post(`site/set_active`, request);
  }
  
  //site configuration services
  deleteSiteComponent(component_id:number, component_type:string){
    var component_reference: ComponentReference = {
      component_id: component_id,
      component_type:component_type
    }
    return this._http.post(`site/delete/site_component`, component_reference);
  }

  postParagraphBox(paragraph_box: ParagraphBox, admin_id:number, admin_token: string){
    return this._http.post(`site/create/paragraph_box/${admin_id}/${admin_token}`, paragraph_box);
  }

  postImage(image: Image, admin_id: number, admin_token:string){
    return this._http.post(`site/create/image/${admin_id}/${admin_token}`, image);
  }

  postPortrait(portrait: Portrait, admin_id: number, admin_token: string){
    return this._http.post(`site/create/portrait/${admin_id}/${admin_token}`, portrait);
  }

  postTwoColumnBox(two_column_box: TwoColumnBox, admin_id: number, admin_token:string){
    return this._http.post(`site/create/2c_box/${admin_id}/${admin_token}`, two_column_box);
  }
}



