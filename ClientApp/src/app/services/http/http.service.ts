//Angular resources
import { Injectable } from '@angular/core';
import{ HttpClient } from '@angular/common/http';

//dto imports
import { Admin } from '../../interfaces/dtos/admin_related/admin_dto';
import { Login } from '../../interfaces/dtos/admin_related/login_dto';
import { INewSiteDto } from '../../interfaces/dtos/database_changers/new_site_dto';
import { Observable } from 'rxjs';
import { session } from 'src/app/interfaces/dtos/analytics_session_dto';
import { admin_request_dto } from 'src/app/interfaces/dtos/admin_related/admin_request_dto';
import { ISkeletonSiteDto } from 'src/app/interfaces/dtos/formatted_sites/skeleton_site_dto';
import { ParagraphBox } from 'src/app/interfaces/dtos/site_components/paragraph_box';
import { Portrait } from 'src/app/interfaces/dtos/site_components/portrait_dto';
import { TwoColumnBox } from 'src/app/interfaces/dtos/site_components/two_column_box';
import { LinkBox } from 'src/app/interfaces/dtos/site_components/link_box';
import { NavBar } from 'src/app/interfaces/dtos/site_components/nav_bar';
import { Image } from 'src/app/interfaces/dtos/site_components/image_dto';
import { JsonResponseDto } from 'src/app/interfaces/dtos/json_response_dto';
import { NewNavLinkDto } from 'src/app/interfaces/dtos/site_components/new_nav_link_dto';
import { ComponentReference } from 'src/app/interfaces/dtos/site_components/component_reference';
import { ContactForm } from 'src/app/interfaces/dtos/reports_related/contact_form';
import { ISiteViewDto } from 'src/app/interfaces/dtos/analytics_dtos/site_view_dto';
import { DataPlan } from 'src/app/interfaces/dtos/admin_related/data_plan';
import { IUpdatedSiteDto } from 'src/app/interfaces/dtos/formatted_sites/update_site_dto';

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
   return this._http.post<Admin>('api/admin/new', NewAdmin);
  }

  deleteAdmin(admin_id:number, token:string){
    let payload:admin_request_dto = {
      admin_id: admin_id,
      token: token
    }

    return this._http.request('delete',`api/admin/delete`, { body: payload });
  }

  editAdmin( AdminToEdit:Admin ): Observable<Admin>{
    return this._http.put<Admin>('api/admin', AdminToEdit);
  }

  activateAccount(admin_id:number, admin_token:string){
    return this._http.request<Admin>('put',`api/admin/activate/${admin_id}/${admin_token}`);
  }

  sendPasswordResetEmail(email:string){
    return this._http.get<string>(`api/admin/email/password_reset/send/${email}`);
  }

  changeAdminPassword(admin_email:string, admin_token:string, new_password:string){
    return this._http.request<Admin>(`put`, `api/admin/password/reset/${admin_email}/${admin_token}/${new_password}`);
  }

  //Admin DataPlan management
  getDataPlanByAdminId(admin_id:number){
    return this._http.get<DataPlan>(`api/admin/data_plan/${admin_id}`);
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

  editSiteTitle(updated_site:IUpdatedSiteDto, admin_token:string){
    return this._http.put<IUpdatedSiteDto>(`api/site/edit/title/${admin_token}`, updated_site);
  }

  //component retrieval services

  getParagraphBox( component_id:number ){
    let api_url = `api/site/get_component/paragraph_box/${component_id}`;
    return this._http.get<ParagraphBox>( api_url );
  }

  getPortrait( component_id:number ){
    let api_url = `api/site/get_component/portrait/${component_id}`;
    return this._http.get<Portrait>( api_url );
  }

  getTwoColumnBox( component_id:number ){
    let api_url = `api/site/get_component/two_column_box/${component_id}`;
    return this._http.get<TwoColumnBox>( api_url );
  }

  getImage( component_id:number ){
    let api_url = `api/site/get_component/image/${component_id}`;
    return this._http.get<Image>( api_url );
  }

  getLinkBox( component_id:number ){
    let api_url = `api/site/get_component/link_box/${component_id}`;
    return this._http.get<LinkBox>( api_url );
  }

  getNavBar(site_id:number){
    return this._http.get<NavBar>( `api/site/get_component/navbar/${site_id}` );
  }
  
  //site configuration services
  deleteSiteComponent(component_id:number, component_type:string, admin_id:number, admin_token:string, site_id:number){
    var component_reference: ComponentReference = {
      component_id: component_id,
      component_type:component_type
    }
    return this._http.post(`api/site/delete/site_component/${admin_id}/${admin_token}/${site_id}`, component_reference);
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
    return this._http.post(`api/site/create/link_box/${admin_id}/${admin_token}`, link_box);
  }

  postNavBar(admin_id:number, admin_token:string, site_id:number){
    return this._http.request('post' ,`api/site/create/nav_bar/${admin_id}/${admin_token}/${site_id}`);
  }

  postNavLink(new_link:NewNavLinkDto, admin_id:number , admin_token:string, site_id:number ){
    return this._http.post( `api/site/create/nav_link/${admin_id}/${admin_token}/${site_id}`, new_link);
  }

  //edit components
  editParagraphBox(paragraph_box:ParagraphBox, admin_id:number, admin_token:string, site_id:number){
    return this._http.put<ParagraphBox>(`api/site/edit/paragraph_box/${admin_id}/${admin_token}/${site_id}`, paragraph_box);
  }

  editImage(image:Image, admin_id:number, admin_token:string, site_id:number){
    return this._http.put<Image>(`api/site/edit/image/${admin_id}/${admin_token}/${site_id}`, image);
  }

  editTwoColumnBox(two_column_box:TwoColumnBox, admin_id:number, admin_token:string, site_id:number){
    return this._http.put<TwoColumnBox>(`api/site/edit/two_column_box/${admin_id}/${admin_token}/${site_id}`, two_column_box);
  }

  editPortrait(portrait:Portrait, admin_id:number, admin_token:string, site_id:number){
    return this._http.put<Portrait>(`api/site/edit/portrait/${admin_id}/${admin_token}/${site_id}`, portrait);
  }

  editLinkBox(link_box:LinkBox, admin_id:number, admin_token:string, site_id:number){
    return this._http.put<LinkBox>(`api/site/edit/link_box/${admin_id}/${admin_token}/${site_id}`, link_box);
  }

  SwapComponentPriority(component_one:ComponentReference, component_two:ComponentReference, admin_id:number, admin_token:string, site_id:number ){
    return this._http.put<JsonResponseDto>(`api/site/edit/swap_components/${admin_id}/${admin_token}/${site_id}`, {component_one:component_one, component_two:component_two});
  }


  //reports
  SendFeedbackReport(contact_form:ContactForm){
    return this._http.post(`api/reports/feedback`, contact_form);
  }


  //analytics
  createSession():Observable<object>{
    let s:session = {
      session_id:0,
      token:"duaiosfbol",
      time_on_homepage:0,
      url: window.location.href
    }
    return this._http.post(`api/analytics/create/`, s);
  }

  updateSession( s:session ):Observable<object>{
    return this._http.post(`api/analytics/update`, s);
  }
  
  //retrieve analytics data
  getAnalyticsForSite(site_id:number ){
    return this._http.get<ISiteViewDto>(`api/analytics/by_site_id/${site_id}`);
  }
}


