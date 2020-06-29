import { Component, OnInit, Input } from '@angular/core';
import { ISiteFormatted } from '../../interfaces/formatted_site_content';
import{ ISiteCredentials } from '../../interfaces/dtos/site_credentials_dto';

@Component({
  selector: 'app-page-generator',
  templateUrl: './page-generator.component.html',
  styleUrls: ['./page-generator.component.css']
})
export class PageGeneratorComponent implements OnInit {

  constructor() { }

  @Input() formatted_site:ISiteFormatted;
  site_credentials:ISiteCredentials;

  ngOnInit() {
    console.log(this.formatted_site.nav_bar);
    if(this.formatted_site.nav_bar != null){
      console.log("Condition Approved");
    }
  }

}
