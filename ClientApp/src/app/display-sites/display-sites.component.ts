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

  //tutorial related
  @Input() is_tutorial:boolean;
  @Output() done = new EventEmitter<boolean>();
  tutorial_sequence: number;
  
  all_sites: any;
  newSiteObject: INewSiteDto;
  current_site_id: number;

  constructor(private _httpService:HttpService, private router: Router, private _Activatedroute:ActivatedRoute) { }

  ngOnInit() {
    //inport url params
    //this.current_admin_id = Number(this._Activatedroute.snapshot.paramMap.get("current_admin_id"));
    this.current_site_id = 0; //non SQL id default value

    if(!this.is_tutorial){
      this.getSitesByAdminFromService();
    }
    this.newSiteObject = {
      title : "Default",
      admin_id: this.current_admin_id,
      token: this.current_admin_token
    };

    //tutorial related
    if(this.is_tutorial){
      this.tutorial_sequence = 1;
    }else{
      this.tutorial_sequence = 0;
    }
  }

  //creates a new site without any content

  postSiteToService(){ //add a validator!
    if(this.is_tutorial && this.tutorial_sequence == 3){
      this.all_sites = [];
      this.all_sites.push(this.newSiteObject);
      this.iterateTutorial();
    }else if(!this.is_tutorial){
    console.log(this.newSiteObject);
    //this.newSiteObject.admin_id = 0; //will be changed on the backend
    this._httpService.postSite(this.newSiteObject).subscribe(
      result => {
        console.log(result);
        this.getSitesByAdminFromService();
      });
    }else{
      //do nothing
    }
  }

  //determines which site will be displayed on the homepage

  setSiteActive(site_id:number){
    if(!this.is_tutorial){
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
    }else{
      //do nothing
    }
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
    if(!this.is_tutorial){
      this._httpService.deleteSite(site_id).subscribe(result =>{
        console.log(result);
        this.getSitesByAdminFromService();
      });
    }
  }

  //unused methods
  editSite(current_site_id:number){
    if(this.is_tutorial){
      this.done.emit(true);
    }else{
      this.current_site_id = current_site_id;
      this.siteIdSelectEvent.emit(current_site_id);
    }
  }

  iterateTutorial(){
    if(this.is_tutorial){
      this.tutorial_sequence += 1;
    }
  }
}


