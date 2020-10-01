import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppFrontendRoutesModule} from './app-frontend-routes.module';
import { AdminComponent } from './admin-related/admin/admin.component';
// import { EditAdminComponent } from './edit-admin/edit-admin.component'; //not currently used, perhaps later
import { SiteEditorComponent } from './editor/site-editor/site-editor.component';
import { DisplaySitesComponent } from './editor/display-sites/display-sites.component';
import { LoginComponent } from './admin-related/login/login.component';
import { TutorialComponent } from './editor/tutorial/tutorial.component';
import { PageGeneratorComponent } from './leaves/page-generator/page-generator.component';
import { ParagraphBoxComponent } from './leaves/leaf-components/paragraph-box/paragraph-box.component';
import { ImageComponent } from './leaves/leaf-components/image/image.component';
import { PortraitComponent } from './leaves/leaf-components/portrait/portrait.component';
import { TwoColumnBoxComponent } from './leaves/leaf-components/two-column-box/two-column-box.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LinkBoxComponent } from './leaves/leaf-components/link-box/link-box.component';
import { LeafComponent } from './leaves/leaf/leaf.component';
import { LeafNavBarComponent } from './leaves/leaf-components/leaf-nav-bar/leaf-nav-bar.component';
import { AnaComponent } from './ana/ana.component';
import { LogoutComponent } from './admin-related/logout/logout.component';
import { PopInComponent } from './pop-in/pop-in.component';
import { AboutComponent } from './about/about.component';
import { AdminAccountComponent } from './admin-related/admin-account/admin-account.component';
import { ActivateAccountComponent } from './admin-related/activate-account/activate-account.component';
import { PasswordResetComponent } from './admin-related/password-reset/password-reset.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AdminComponent,
    // EditAdminComponent,
    SiteEditorComponent,
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
    LinkBoxComponent,
    LeafComponent,
    LeafNavBarComponent,
    AnaComponent,
    LogoutComponent,
    PopInComponent,
    AboutComponent,
    AdminAccountComponent,
    ActivateAccountComponent,
    PasswordResetComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppFrontendRoutesModule
  ],
  providers: [
    { provide: 'Window',  useValue: window }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
