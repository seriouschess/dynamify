import { Component, OnInit, Input } from '@angular/core';
import { Portrait } from '../interfaces/dtos/site_dtos';

@Component({
  selector: 'app-portrait',
  templateUrl: './portrait.component.html',
  styleUrls: ['./portrait.component.css']
})
export class PortraitComponent implements OnInit {

  @Input() portrait_object: Portrait;

  constructor() { }

  ngOnInit() {
  }

}
