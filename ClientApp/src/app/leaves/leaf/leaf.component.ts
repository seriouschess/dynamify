import { Component, OnInit, Input } from '@angular/core';
//import { SiteFormatterService } from '../../services/leaf-formatter/site-formatter.service';
import { ISiteFormatted } from '../../interfaces/dtos/tutorial_site_emulator_dtos/formatted_site_content';
import { Params, ActivatedRoute, Router } from '@angular/router';
import { ISkeletonSiteDto } from 'src/app/interfaces/dtos/formatted_sites/skeleton_site_dto';
import { HttpService } from 'src/app/services/http/http.service';
import { NavBar } from 'src/app/interfaces/dtos/site_components/NavBar';
import { NavLink } from 'src/app/interfaces/dtos/site_components/NavLink';

@Component({
  selector: 'app-leaf',
  templateUrl: './leaf.component.html',
  styleUrls: ['./leaf.component.css']
})
export class LeafComponent implements OnInit {

  constructor( private _route: ActivatedRoute,
      private _router: Router,
      private _httpClient:HttpService ) { }

  @Input() leaf_url:string;
  test_nav_bar: NavBar;
  formatted_site: ISkeletonSiteDto;
  sucessful_load:boolean;

  ngOnInit() {
    this.sucessful_load = false; //until proven otherwise
    this._route.params.subscribe((params:Params) => {
      this.leaf_url = params['leaf_url'];
      this.requireLeafContent();
    })
    console.log("leaf url: "+this.leaf_url);

    this.formatted_site = {
      title: null,
      site_id: null,
      site_components: null
    }
  }

  requireLeafContent(){
    this._httpClient.getLeafSkeletonByUrl(this.leaf_url).subscribe(res => {
      this.formatted_site = res
    }, err => {
      this._router.navigate(['app/not-found']);
    });;
  }
}
