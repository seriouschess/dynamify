import { Component, OnInit, Input } from '@angular/core';
import { HttpService } from '../http.service';
import { Router } from '@angular/router';

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
      paragraph_boxes: null,
      images: null,
      two_column_boxes: null,
      portraits: null
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
    var setMyIdActive:any = { //created only to pass id, preferred over parameter
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

//site interfaces

interface ParagraphBox{
  title: string;
  priority: number;
  site_id: number;

  content: string;
}

interface Image{
  title: string;
  priority:number;
  site_id: number;

  image_src: string;
}

interface Portrait{
  title: string;
  priority:number;
  site_id: number;

  image_src: string;
  content: string;
}

interface TwoColumnBox{
  title:string;
  priority:number;
  site_id:number;

  heading_one:string;
  heading_two:string;
  content_one:string;
  content_two:string;
}

interface Site{
  site_id: number;
  title: string;
  admin_id: number;
  owner: Admin;
  paragraph_boxes: ParagraphBox[];
  images: Image[];
  two_column_boxes: TwoColumnBox[];
  portraits: Portrait[];
}


