import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-site-editor',
  templateUrl: './site-editor.component.html',
  styleUrls: ['./site-editor.component.css']
})
export class SiteEditorComponent implements OnInit {

  //route parameters
  current_site_id:number;
  current_admin_id:number;

  test:any;
  current_site:any; //seriously though, it's a Site type
  new_paragraph_box: ParagraphBox;

  constructor( private _httpService:HttpService, private _Activatedroute:ActivatedRoute) { }

  ngOnInit() {
    
    this.current_site_id = Number(this._Activatedroute.snapshot.paramMap.get("current_site_id"));
    this.current_admin_id = Number(this._Activatedroute.snapshot.paramMap.get("current_admin_id"));
    
    this.current_site = {
      site_id: 0, //set as default parameter
      title : "Default",
      admin_id: 0,
      owner: null,
      paragraph_boxes: null
    }

    this.new_paragraph_box = {
      paragraph_box_id: 0,
      title: "",
      content: "",
      site_id: this.current_site_id
    }

    this.getSiteFromService(); //based on this.current_admin_id
  }

  postSiteToService(new_site:Site){
    this._httpService.postSite(new_site).subscribe(result =>{
      console.log(result);
    }, error => console.log(error));
  }

  getSiteFromService(){
    this._httpService.getSite(this.current_site_id).subscribe(result =>{
      this.current_site = result; //better be a site though
    });
  }

  postParagraphBoxToService(){
      this._httpService.postParagraphBox(this.new_paragraph_box).subscribe(results =>{
      console.log(results);
      this.getSiteFromService();
    }, error => console.log(error));
  }

  editSite(site_id:number, admin_id:number){
      this._httpService.getActiveSite().subscribe(results => {
      console.log(results);
      this.current_site = results;
      console.log("This is being rendered?: "+JSON.stringify(this.current_site));
    }, error => console.log(error));
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
  site_id: number;
}

interface Site{
  site_id: number;
  title: string;
  admin_id: number;
  owner: Admin;
  paragraph_boxes: ParagraphBox[];
}
