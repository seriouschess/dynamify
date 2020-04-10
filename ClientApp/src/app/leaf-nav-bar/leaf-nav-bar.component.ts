import { Component, OnInit, Input } from '@angular/core';
import { NavBar } from '../interfaces/dtos/site_dtos';

@Component({
  selector: 'app-leaf-nav-bar',
  templateUrl: './leaf-nav-bar.component.html',
  styleUrls: ['./leaf-nav-bar.component.css']
})
export class LeafNavBarComponent implements OnInit {

  @Input() nav_bar_object: NavBar;
  constructor() { }

  ngOnInit() {
  }

}
