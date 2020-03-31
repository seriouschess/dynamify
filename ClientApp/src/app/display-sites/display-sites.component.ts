import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpService } from '../http.service';
import { Router, ActivatedRoute } from '@angular/router';
import { INewSiteDto } from '../interfaces/dtos/new_site_dto';
import { ISiteRequestDto } from '../interfaces/dtos/site_request_dto';

@Component({
  selector: 'app-display-sites',
  templateUrl: './display-sites.component.html',
  styleUrls: ['./display-sites.component.css']
})

export class DisplaySitesComponent implements OnInit {

  //get Admin information
  @Input() current_admin_id: number;
  @Input() current_admin_token: string;

  //select site to be sent to admin component
  @Output() siteIdSelectEvent = new EventEmitter<number>();
  
  all_sites:any;
  newSiteObject:INewSiteDto;
  current_site_id:number;

  constructor(private _httpService:HttpService, private router: Router, private _Activatedroute:ActivatedRoute) { }

  ngOnInit() {
    //inport url params
    //this.current_admin_id = Number(this._Activatedroute.snapshot.paramMap.get("current_admin_id"));
    this.current_site_id = 0; //non SQL id default value

    this.getSitesByAdminFromService();
    this.newSiteObject = {
      title : "Default",
      admin_id: this.current_admin_id,
      token: this.current_admin_token
    };
  }

  //creates a new site without any content

  postSiteToService(){
    console.log(this.newSiteObject);
    //this.newSiteObject.admin_id = 0; //will be changed on the backend
    this._httpService.postSite(this.newSiteObject).subscribe(
      result => {
        console.log(result);
        this.getSitesByAdminFromService();
      });
  }

  //determines which site will be displayed on the homepage

  setSiteActive(site_id:number){
    var setMyIdActive: ISiteRequestDto = { //created only to pass id, preferred over parameter
      site_id: site_id,
      admin_id: this.current_admin_id,
      token: this.current_admin_token
    };

    this._httpService.setActiveSite(setMyIdActive).subscribe(
      result => {
        console.log(result);
        this.router.navigateByUrl('');
      }
    )
  }

  //requests all sites owned by a specific admin 

  getSitesByAdminFromService(){
    this._httpService.getSitesByAdmin(this.current_admin_id, this.current_admin_token).subscribe(results => 
    {
      this.all_sites = results;
      console.log(results);
    }, error => console.log(error));
  }

  //it does what it says

  deleteSiteById(site_id:number){
    this._httpService.deleteSite(site_id).subscribe(result =>{
      console.log(result);
      this.getSitesByAdminFromService();
    });
  }


  //unused methods
  editSite(current_site_id:number){
    this.current_site_id = current_site_id;
    this.siteIdSelectEvent.emit(current_site_id);
  }
}


