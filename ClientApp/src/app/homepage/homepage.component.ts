import { Component, OnInit } from '@angular/core';
import { ISiteFormatted } from '../interfaces/formatted_site_content';
import { SiteFormatterService } from '../site-formatter.service';
import { ISiteRequestDto } from '../interfaces/dtos/site_request_dto';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  constructor(  ) { }
  
  ngOnInit() { 
  }

}
