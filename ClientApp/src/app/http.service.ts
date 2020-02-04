import { Injectable } from '@angular/core';
import{ HttpClient } from '@angular/common/http';

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

  deleteSite(site_id:number){
    return this._http.delete(`site/delete/${site_id}`)
  }

  postSite(input_site:Site){
    return this._http.post('site/create_site', input_site);
  }

  getSite<Site>(site_id_parameter:number){
    return this._http.get(`site/get/${site_id_parameter}`);
  }

  getSitesByAdmin(admin_id: number) {
    return this._http.get(`site/get_by_admin/${admin_id}`);
  }

  getActiveSite<Site>(){
    return this._http.get('site/get_active');
  }
  
  setActiveSite(new_active_site:Site){
    return this._http.post(`site/set_active`, new_active_site);
  }
  
  //site configuration services
  postParagraphBox(paragraph_box:ParagraphBox){
    return this._http.post(`site/create_paragraph_box`, paragraph_box);
  }

}

interface Admin{
  admin_id: number;
  first_name: string;
  last_name: string;
  email: string;
  password: string;
}

interface ParagraphBox{
  paragraph_box_id: number;
  title: string;
  content: string;
  site_id:number;
}

interface Site{
  site_id: number;
  title: string;
  admin_id: number;
  owner: Admin;
  paragraph_boxes: ParagraphBox[];
}

interface Login{
  email: string;
  password: string;
}
