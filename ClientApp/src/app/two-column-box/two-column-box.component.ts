import { Component, OnInit, Input } from '@angular/core';
import { TwoColumnBox } from '../interfaces/dtos/site_dtos';

@Component({
  selector: 'app-two-column-box',
  templateUrl: './two-column-box.component.html',
  styleUrls: ['./two-column-box.component.css']
})
export class TwoColumnBoxComponent implements OnInit {

  @Input() tcb_object:TwoColumnBox;

  constructor() { }

  ngOnInit() {
  }

}
