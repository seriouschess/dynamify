import { Component, OnInit, Input } from '@angular/core';
import { HttpService } from '../http.service';
import { Router } from '@angular/router';
import { SiteEditorComponent } from '../site-editor/site-editor.component';

@Component({
  selector: 'app-display-sites',
  templateUrl: './display-sites.component.html',
  styleUrls: ['./display-sites.component.css']
})

export class DisplaySitesComponent implements OnInit {

  @Input() current_admin_id:number;
  all_sites:any;
  newSiteObject:Site;

  constructor(private _httpService:HttpService, private router: Router) { }

  ngOnInit() {
    this.getSitesByAdminFromService(this.current_admin_id);
    this.newSiteObject = {
      site_id: 0, //never used, just here for type consistency
      title : "Default",
      admin_id: this.current_admin_id,
      owner: null,
      paragraph_boxes: null
    };
  }

  postSiteToService(){
    console.log(this.newSiteObject);
    //this.newSiteObject.admin_id = 0; //will be changed on the backend
    this._httpService.postSite(this.newSiteObject).subscribe(
      result => {
        console.log(result);
        this.getSitesByAdminFromService(this.current_admin_id);
      });
  }

  setSiteActive(site_id:number){
    var setMyIdActive:Site = { //created only to pass id, preferred over parameter
      site_id: site_id,
      title: "",
      admin_id: 0,
      owner: null,
      paragraph_boxes: null
    };

    this._httpService.setActiveSite(setMyIdActive).subscribe(
      result => {
        console.log(result);
        this.router.navigateByUrl('');
      }
    )
  }

  getSitesByAdminFromService(admin_id:number){
    this._httpService.getSitesByAdmin(admin_id).subscribe(results => 
    {
      this.all_sites = results;
      console.log(results);
    }, error => console.log(error));
  }

  deleteSiteById(site_id:number){
    this._httpService.deleteSite(site_id).subscribe(result =>{
      console.log(result);
      this.getSitesByAdminFromService(this.current_admin_id);
    });
  }

  editSite(current_site_id:number){
    this.router.navigateByUrl(`/edit_site/${current_site_id}/${this.current_admin_id}`);
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