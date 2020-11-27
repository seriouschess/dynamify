import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-activate-account',
  templateUrl: './activate-account.component.html',
  styleUrls: ['./activate-account.component.css']
})
export class ActivateAccountComponent implements OnInit {

  activated:boolean;

  constructor(private _route: ActivatedRoute,
    private _httpClient:HttpService,
    private _router:Router) { }

  ngOnInit() {
    this._route.params.subscribe((params:Params) => {
      let admin_id = params['admin_id'];
      let token = params['token'];
      this.activated = false;
      
      this._httpClient.activateAccount(admin_id, token).subscribe(( ) => {
        this.activated = true;
      });
    });
  }

  goToLogin(){
    this._router.navigate(['app/admin']);
  }
}
