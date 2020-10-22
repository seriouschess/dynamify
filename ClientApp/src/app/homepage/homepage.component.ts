import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
  
  constructor( ) { }
  
  ngOnInit() { }

  paypalDonate(){
    let url = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=G7ZYNSSEFQQ8N&currency_code=USD";
    window.open(url, "_blank");
  }

}
