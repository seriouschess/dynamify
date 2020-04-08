import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ProfileComponent } from './profile/profile.component';
import { AppFrontendRoutesModule} from './app-frontend-routes.module';
import { AdminComponent } from './admin/admin.component';
// import { EditAdminComponent } from './edit-admin/edit-admin.component'; //not currently used, perhaps later
import { SiteEditorComponent } from './site-editor/site-editor.component';
import { LiveSiteComponent } from './live-site/live-site.component';
import { DisplaySitesComponent } from './display-sites/display-sites.component';
import { LoginComponent } from './login/login.component';
import { TutorialComponent } from './tutorial/tutorial.component';
import { PageGeneratorComponent } from './page-generator/page-generator.component';
import { ParagraphBoxComponent } from './paragraph-box/paragraph-box.component';
import { ImageComponent } from './image/image.component';
import { PortraitComponent } from './portrait/portrait.component';
import { TwoColumnBoxComponent } from './two-column-box/two-column-box.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LinkBoxComponent } from './link-box/link-box.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ProfileComponent,
    AdminComponent,
    // EditAdminComponent,
    SiteEditorComponent,
    LiveSiteComponent,
    DisplaySitesComponent,
    LoginComponent,
    TutorialComponent,
    PageGeneratorComponent,
    ParagraphBoxComponent,
    ImageComponent,
    PortraitComponent,
    TwoColumnBoxComponent,
    PageNotFoundComponent,
    HomepageComponent,
    LinkBoxComponent
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
