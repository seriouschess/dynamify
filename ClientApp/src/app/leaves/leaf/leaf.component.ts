import { Component, OnInit, Input } from '@angular/core';
import { SiteFormatterService } from '../../services/leaf-formatter/site-formatter.service';
import { ISiteFormatted } from '../../interfaces/formatted_site_content';
import { ISiteRequestDto } from '../../interfaces/dtos/site_request_dto';
import { Params, ActivatedRoute, Router } from '@angular/router';
import { NavBar, NavLink } from '../../interfaces/dtos/site_dtos';

@Component({
  selector: 'app-leaf',
  templateUrl: './leaf.component.html',
  styleUrls: ['./leaf.component.css']
})
export class LeafComponent implements OnInit {

  constructor( private _route: ActivatedRoute,
      private _siteFormatter:SiteFormatterService,
      private router: Router ) { }

  @Input() leaf_url:string;

  test_nav_bar: NavBar;
  formatted_site:ISiteFormatted;
  request:ISiteRequestDto;
  sucessful_load:boolean;

  ngOnInit() {
    this.sucessful_load = false; //until proven otherwise
    this._route.params.subscribe((params:Params) => {
      this.leaf_url = params['leaf_url'];
      this.requireLeafContent();
    })

    this.formatted_site = {
      title: null,
      nav_bar: null,
      site_components: null
    }
  }

  requireLeafContent(){
    this._siteFormatter.getLeafByURLFormatted( this.leaf_url, this.recieveSite, this); 
  }

  recieveSite(formatted_site:ISiteFormatted, this_component:LeafComponent){
    if(formatted_site === null){
      this_component.router.navigate(['base/not-found']);
    }else{
      console.log("looks like we've made it")
      this_component.formatted_site = formatted_site;
      this.sucessful_load = true;
    }
  }
}
