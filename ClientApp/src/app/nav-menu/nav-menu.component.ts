import { Component, OnInit, Input } from '@angular/core';
import { ClientStorageService } from '../services/client-storage/client-storage.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  @Input() logged_in: boolean;
  isExpanded: boolean;

  constructor( private _clientStorage:ClientStorageService ){}
  
  ngOnInit(): void {
    this.updateLogin();
    this.isExpanded = false;
  }

  collapse(){
    this.isExpanded = false;
  }

  toggle(){
    this.isExpanded = !this.isExpanded;
  }

  updateLogin(){
    if(this._clientStorage.checkLogin()){
      this.logged_in = true;
    }else{
      this.logged_in = false;
    }
  }
}
