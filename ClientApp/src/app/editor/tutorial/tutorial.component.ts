import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-tutorial',
  templateUrl: './tutorial.component.html',
  styleUrls: ['./tutorial.component.css']
})
export class TutorialComponent implements OnInit {

  constructor() { }

  open_site_select: boolean;
  open_editor: boolean;
  display_sites_done: boolean;


  ngOnInit() {
    this.open_site_select = false;
    this.open_editor = false;
    this.display_sites_done = false;
  }

  beginTutorial(){
    this.open_site_select = true;
  }

  checkDone($event){
    console.log("me");
    this.display_sites_done = $event;
    this.open_editor = true;
    this.open_site_select = false;
    console.log("Open Editor?"+this.open_editor);
  }
}
