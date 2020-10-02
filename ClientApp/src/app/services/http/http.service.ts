//Angular resources
import { Injectable } from '@angular/core';
import{ HttpClient, HttpErrorResponse } from '@angular/common/http';

//dto imports
import { Admin } from '../../interfaces/dtos/admin_dto';
import { Login } from '../../interfaces/dtos/login_dto';
import { ParagraphBox, Image, Portrait, TwoColumnBox, LinkBox, NavBar } from '../../interfaces/dtos/site_dtos';
import { ComponentReference } from '../../interfaces/dtos/component_reference';
import { INewSiteDto } from '../../interfaces/dtos/new_site_dto';
import { ISiteRequestDto } from '../../interfaces/dtos/site_request_dto';
import { ISiteContentDto } from '../../interfaces/dtos/site_content_dto';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { session } from 'src/app/interfaces/dtos/analytics_session_dto';
import { IComponentRequestDto } from 'src/app/interfaces/dtos/component_request_dto';
import { admin_request_dto } from 'src/app/interfaces/dtos/admin_request_dto';

@Injectable({
  providedIn: 'root'
})

export class HttpService {
  // @Inject('BASE_URL') baseUrl: string
  constructor(private _http:HttpClient) { }

  //admin services

  loginAdmin(login_payload:Login){
    return this._http.post(`api/admin/login`, login_payload);
  }

  postAdmin<AdminRegistrationDto>(NewAdmin:AdminRegistrationDto){
   console.log(JSON.stringify(NewAdmin));
   return this._http.post<Admin>('api/admin/new', NewAdmin);
  }

  deleteAdmin(admin_id:number, token:string){
    console.log(`Admin ID for deletion:${admin_id}`);
    let payload:admin_request_dto = {
      admin_id: admin_id,
      token: token
    }

    //http.request('delete', url, { body: { ... } });
    //delete requests don't allow payloads
    return this._http.request('delete',`api/admin/delete`, { body: payload });
  }

  editAdmin( AdminToEdit:Admin ): Observable<Admin>{
    console.log(JSON.stringify(AdminToEdit));
    return this._http.put<Admin>('api/admin', AdminToEdit);
  }

  activateAccount(admin_email:string, admin_token:string){
    return this._http.request<Admin>('put',`api/admin/activate/${admin_email}/${admin_token}`);
  }

  sendPasswordResetEmail(email:string){
    return this._http.get<string>(`api/admin/email/password_reset/send/${email}`);
  }

  changeAdminPassword(admin_email:string, admin_token:string, new_password:string){
    return this._http.request<Admin>(`put`, `api/admin/password/reset/${admin_email}/${admin_token}/${new_password}`);
  }

  //site services

  getSite<ISiteContentDto>(request:ISiteRequestDto){
    return this._http.post<ISiteContentDto>(`api/site/get`, request);
  }

  getLeafByURL(leaf_url:string){
    return this._http.get<ISiteContentDto>(`api/site/get_by_url/full/${leaf_url}`, {observe: 'response'})
    .pipe(
      catchError(() =>{
        return throwError(new Error('Leaf Not Found'));
      })
    );
  }

  getLeafSkeletonByUrl(leaf_url:string){
    return this._http.get<ISiteContentDto>(`api/site/get_by_url/skeleton/${leaf_url}`, {observe: 'response'})
    .pipe(
      catchError(() =>{
        return throwError(new Error('Leaf Not Found'));
      })
    );
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

  //component retrieval services

  getParagraphBox( request:IComponentRequestDto ){
    let api_url = `api/site/get_component/paragraph_box/${request.component_id}/${request.site_id}`;
    return this._http.get<ParagraphBox>( api_url );
  }

  getPortrait( request:IComponentRequestDto ){
    let api_url = `api/site/get_component/portrait/${request.component_id}/${request.site_id}`;
    return this._http.get<Portrait>( api_url );
  }

  getTwoColumnBox( request:IComponentRequestDto ){
    let api_url = `api/site/get_component/two_column_box/${request.component_id}/${request.site_id}`;
    return this._http.get<TwoColumnBox>( api_url );
  }

  getImage( request:IComponentRequestDto ){
    let api_url = `api/site/get_component/image/${request.component_id}/${request.site_id}`;
    return this._http.get<Image>( api_url );
  }

  getLinkBox( request:IComponentRequestDto ){
    let api_url = `api/site/get_component/link_box/${request.component_id}/${request.site_id}`;
    return this._http.get<LinkBox>( api_url );
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

  postNavBar(nav_bar: NavBar, admin_id: number, admin_token:string){
    return this._http.post(`api/site/create/nav_bar/${admin_id}/${admin_token}`, nav_bar);
  }

  //analytics
  createSession():Observable<object>{
    let s:session = {
      session_id:0,
      token:"duaiosfbol",
      time_on_homepage:0,
      url: window.location.href
    }
    return this._http.post(`https://analytics.siteleaves.com/storage/create`, s);
  }

  updateSession( s:session ):Observable<object>{
    // let s:session = {
    //   session_id: 0,
    //   token:token,
    //   time_on_homepage: time,
    //   url: window.location.href
    // }
    return this._http.post(`https://analytics.siteleaves.com/storage/update`, s);
  } 
}



