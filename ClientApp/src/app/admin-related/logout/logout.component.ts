import { Component, OnInit } from '@angular/core';
import { ClientStorageService } from 'src/app/services/client-storage/client-storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor( private _clientStorage:ClientStorageService,
                private router: Router ){}

  ngOnInit() {
    this._clientStorage.logoutAdmin();
    this.router.navigate(['/']);
  }

  logOutAdmin(){
    console.log("ljabf;db;flajsbdf;ljabsdkfjbasldkfjblkasjdbflkjasdbflkjbasdlkfjbsldkjfbklajsdbflkjbdskfjbaskdfb");
  }

}
