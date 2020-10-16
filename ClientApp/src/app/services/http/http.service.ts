//Angular resources
import { Injectable } from '@angular/core';
import{ HttpClient } from '@angular/common/http';

//dto imports
import { Admin } from '../../interfaces/dtos/admin_related/admin_dto';
import { Login } from '../../interfaces/dtos/admin_related/login_dto';
import { INewSiteDto } from '../../interfaces/dtos/database_changers/new_site_dto';
//import { ISiteRequestDto } from '../../interfaces/dtos/site_request_dto';
import { Observable } from 'rxjs';
import { session } from 'src/app/interfaces/dtos/analytics_session_dto';
import { IComponentRequestDto } from 'src/app/interfaces/dtos/formatted_sites/component_request_dto';
import { admin_request_dto } from 'src/app/interfaces/dtos/admin_related/admin_request_dto';
import { ISkeletonSiteDto } from 'src/app/interfaces/dtos/formatted_sites/skeleton_site_dto';
import { ParagraphBox } from 'src/app/interfaces/dtos/site_components/ParagraphBox';
import { Portrait } from 'src/app/interfaces/dtos/site_components/Portrait';
import { TwoColumnBox } from 'src/app/interfaces/dtos/site_components/TwoColumnBox';
import { LinkBox } from 'src/app/interfaces/dtos/site_components/LinkBox';
import { NavBar } from 'src/app/interfaces/dtos/site_components/NavBar';
import { Image } from 'src/app/interfaces/dtos/site_components/Image';
import { JsonResponseDto } from 'src/app/interfaces/dtos/json_response_dto';
import { NewNavLinkDto } from 'src/app/interfaces/dtos/site_components/NewNavLinKDto';

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

  getSkeletonSiteById(site_id:number):Observable<ISkeletonSiteDto>{
    return this._http.get<ISkeletonSiteDto>(`api/site/get_by_id/skeleton/${site_id}`);
  }

  getLeafSkeletonByUrl(leaf_url:string):Observable<ISkeletonSiteDto>{
    return this._http.get<ISkeletonSiteDto>(`api/site/get_by_url/skeleton/${leaf_url}`);
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

  getNavBar(site_id:number){
    return this._http.get<NavBar>( `api/site/get_component/navbar/${site_id}` );
  }
  
  //site configuration services
  deleteSiteComponent(component_id:number, component_type:string, admin_id:number, admin_token:string){
    var component_reference: ComponentReference = {
      component_id: component_id,
      component_type:component_type
    }
    return this._http.post(`api/site/delete/site_component/${admin_id}/${admin_token}`, component_reference);
  }

  deleteNavBar( admin_id:number, admin_token:string, site_id:number){
    return this._http.delete<JsonResponseDto>( `api/site/delete/site_component/${admin_id}/${admin_token}/${site_id}` );
  }

  deleteNavLink(admin_id:number, admin_token:string, site_id:number, link_id:number){
    return this._http.delete<JsonResponseDto>(`delete/navlink/${admin_id}/${admin_token}/${site_id}/${link_id}`);
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

  postNavLink(new_link:NewNavLinkDto, admin_id:number , admin_token:string, site_id:number ){
    return this._http.post( `api/site/create/nav_link/{admin_id}/{admin_token}/{site_id}`, new_link);
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

//Needs a home. Adopt an interface today
interface ComponentReference{
  component_id:number,
  component_type:string
}



