import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-editor-options',
  templateUrl: './editor-options.component.html',
  styleUrls: ['./editor-options.component.css']
})
export class EditorOptionsComponent implements OnInit {

  @Input() nav_bar_editor_open:boolean;
  @Output() nav_bar_editor_openChange = new EventEmitter<boolean>();
  @Input() preview_mode:boolean;
  @Output() preview_modeChange = new EventEmitter<boolean>();
  @Output() leave_editor = new EventEmitter<boolean>();

  @Output() sPbox = new EventEmitter<boolean>();
  @Output() sTcBox = new EventEmitter<boolean>();
  @Output() sLbox = new EventEmitter<boolean>();
  @Output() sPortrait = new EventEmitter<boolean>();
  @Output() sImage = new EventEmitter<boolean>();

  //asthetics
  main_menu:boolean

  constructor() { }

  ngOnInit(): void {
    this.main_menu = true;
  }

  //Actions

  toggleMainMenu(){
    this.main_menu = !this.main_menu;
  }

  //parent actions
  toggleNavBarEditor(){
    this.nav_bar_editor_open = !this.nav_bar_editor_open;
    this.nav_bar_editor_openChange.emit();
  }

  togglePreview(){
    this.preview_mode = !this.preview_mode;
    this.preview_modeChange.emit();
  }

  leaveEditor(){
    this.leave_editor.emit();
  }

  setPboxEdit(){
    this.sPbox.emit();
  }

  set2cBoxEdit(){
    this.sTcBox.emit();
  }

  setLinkBoxEdit(){
    this.sLbox.emit();
  }

  setPortraitEdit(){
    this.sPortrait.emit();
  }

  setImageEdit(){
    this.sImage.emit();
  }



}
