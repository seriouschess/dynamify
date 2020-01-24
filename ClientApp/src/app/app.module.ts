import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { ProfileComponent } from './profile/profile.component';
import { AppFrontendRoutesModule} from './app-frontend-routes.module';
import { AdminComponent } from './admin/admin.component';
import { EditAdminComponent } from './edit-admin/edit-admin.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    ProfileComponent,
    AdminComponent,
    EditAdminComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppFrontendRoutesModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
