import { Component, OnInit, Input } from '@angular/core';
import { ISiteFormatted } from '../../interfaces/dtos/tutorial_site_emulator_dtos/formatted_site_content';
import{ ISiteCredentials } from '../../interfaces/dtos/database_changers/site_credentials_dto';

@Component({
  selector: 'app-page-generator',
  templateUrl: './page-generator.component.html',
  styleUrls: ['./page-generator.component.css']
})
export class PageGeneratorComponent implements OnInit {

  constructor() { }

  @Input() formatted_site:ISiteFormatted;
  site_credentials:ISiteCredentials;

  ngOnInit() { }
}
