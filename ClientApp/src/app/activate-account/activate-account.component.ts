import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { HttpService } from '../services/http/http.service';

@Component({
  selector: 'app-activate-account',
  templateUrl: './activate-account.component.html',
  styleUrls: ['./activate-account.component.css']
})
export class ActivateAccountComponent implements OnInit {

  constructor(private _route: ActivatedRoute,
    private _httpClient:HttpService,
    private _router:Router) { }

  ngOnInit() {
    this._route.params.subscribe((params:Params) => {
      let email = params['email'];
      let token = params['token'];

      this._httpClient.activateAccount(email, token).subscribe(( ) => {
        this._router.navigate(['base/admin']);
      });
    });
  }
}
