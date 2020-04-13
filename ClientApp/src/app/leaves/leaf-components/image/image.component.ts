import { Component, OnInit, Input } from '@angular/core';
import { Image } from '../../../interfaces/dtos/site_dtos';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css']
})
export class ImageComponent implements OnInit {

  @Input() image_object:Image;

  constructor() { }

  ngOnInit() {
  }

}
