import { Component, OnInit, Input } from '@angular/core';
import { AdminComponent } from '../admin/admin.component';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-edit-admin',
  templateUrl: './edit-admin.component.html',
  styleUrls: ['./edit-admin.component.css']
})
export class EditAdminComponent implements OnInit {

  @Input() editAdminObject:Admin;
    constructor(private _httpService:HttpService) {
      this.editAdminObject = this.editAdminObject;
  }

  ngOnInit() { }
  editAdminFromService(admin_to_edit:Admin){
  }

}

interface Admin {
  adminId: number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}
