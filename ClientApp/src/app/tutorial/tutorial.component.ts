import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-tutorial',
  templateUrl: './tutorial.component.html',
  styleUrls: ['./tutorial.component.css']
})
export class TutorialComponent implements OnInit {

  constructor() { }

  open_editor:boolean

  ngOnInit() {
    this.open_editor = false;

  }

  beginTutorial(){
    this.open_editor = true;
  }

}
