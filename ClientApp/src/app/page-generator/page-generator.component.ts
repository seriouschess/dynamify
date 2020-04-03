import { Component, OnInit, Input } from '@angular/core';
import { ISiteFormatted } from '../interfaces/formatted_site_content';

@Component({
  selector: 'app-page-generator',
  templateUrl: './page-generator.component.html',
  styleUrls: ['./page-generator.component.css']
})
export class PageGeneratorComponent implements OnInit {

  constructor() { }

  @Input() formatted_site:ISiteFormatted;
  ngOnInit() {

  }

}
