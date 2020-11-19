import { Component, Input, OnInit } from '@angular/core';
import { DataPlan } from 'src/app/interfaces/dtos/admin_related/data_plan';
import { HttpService } from 'src/app/services/http/http.service';

@Component({
  selector: 'app-data-plan-display',
  templateUrl: './data-plan-display.component.html',
  styleUrls: ['./data-plan-display.component.css']
})
export class DataPlanDisplayComponent implements OnInit {

  @Input() admin_id:number;
  current_data_plan:DataPlan;

  bytes_in_megabyte:number;

  constructor(private _httpService:HttpService) { }

  ngOnInit(): void {
    this.bytes_in_megabyte = 1000000;
    this.getDataPlan();
  }

  getDataPlan(){
    this._httpService.getDataPlanByAdminId(this.admin_id).subscribe( res => {
      this.current_data_plan = res;
    }, err => console.log(err));
  }

}
