import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { HttpService } from '../services/http/http.service';
import { session } from '../interfaces/dtos/analytics_session_dto';

@Component({
  selector: 'app-ana',
  templateUrl: './ana.component.html',
  styleUrls: ['./ana.component.css']
})
export class AnaComponent implements OnInit, OnDestroy {
  s: session;
  active: boolean;
  session_interval: number;

  constructor( private _httpService:HttpService ) { }

  ngOnInit() {
    this.session_interval = 5;
    this.active = true;
    let full_url = window.location.href
    this.s = {
      session_id:0,
      token:"duaiosfbol",
      time_on_homepage:0,
      url:full_url
    }
    this.startUpdateSequence();
   

    this._httpService.createSession().subscribe(results =>{
      var x:any = results;
      this.s.token = x.token;
      this.s.session_id = x.session_id;
    }, error => console.log(error));
  }

  ngOnDestroy(){
    this.active = false;
  }

  startUpdateSequence(){

    var continueSequence = () => { //like set interval only it checks for it's parent object each time.
      setTimeout( () => {
        this.s.time_on_homepage += 1;
        if(this.active){
          if( this.session_interval <= this.s.time_on_homepage){
            this.session_interval *= 2;
            this.update();
          }
          continueSequence();
        }
      }, 1000);
    };

    continueSequence();
  }

  update( ){
    this._httpService.updateSession(this.s).subscribe(res => {
      console.log(res);
    });
  }

}
