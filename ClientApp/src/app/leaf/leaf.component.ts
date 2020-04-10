import { Component, OnInit, Input } from '@angular/core';
import { SiteFormatterService } from '../site-formatter.service';
import { ISiteFormatted } from '../interfaces/formatted_site_content';
import { ISiteRequestDto } from '../interfaces/dtos/site_request_dto';
import { Params, ActivatedRoute } from '@angular/router';
import { NavBar, NavLink } from '../interfaces/dtos/site_dtos';

@Component({
  selector: 'app-leaf',
  templateUrl: './leaf.component.html',
  styleUrls: ['./leaf.component.css']
})
export class LeafComponent implements OnInit {

  constructor( private _route: ActivatedRoute, private _siteFormatter:SiteFormatterService ) { }

  @Input() leaf_url:string;

  test_nav_bar: NavBar;
  formatted_site:ISiteFormatted;
  request:ISiteRequestDto;

  ngOnInit() {
    this._route.params.subscribe((params:Params) => {
      console.log(params['leaf_url']);
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
    this_component.formatted_site = formatted_site;
  }

}
