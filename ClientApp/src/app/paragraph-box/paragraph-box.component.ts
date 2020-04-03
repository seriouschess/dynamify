import { Component, OnInit, Input } from '@angular/core';
import { ParagraphBox } from '../interfaces/dtos/site_dtos';

@Component({
  selector: 'app-paragraph-box',
  templateUrl: './paragraph-box.component.html',
  styleUrls: ['./paragraph-box.component.css']
})
export class ParagraphBoxComponent implements OnInit {

  @Input() pbox_object: ParagraphBox;
  constructor() { }

  ngOnInit() {
  }

}
