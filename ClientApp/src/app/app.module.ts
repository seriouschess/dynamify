import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AppFrontendRoutesModule} from './app-frontend-routes.module';
import { AdminComponent } from './admin-related/admin/admin.component';
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
import { LeafNavBarEditorComponent } from './editor/leaf-nav-bar-editor/leaf-nav-bar-editor.component';
import { AnaComponent } from './ana/ana.component';
import { LogoutComponent } from './admin-related/logout/logout.component';
import { PopInComponent } from './pop-in/pop-in.component';
import { AboutComponent } from './about/about.component';
import { AdminAccountComponent } from './admin-related/admin-account/admin-account.component';
import { ActivateAccountComponent } from './admin-related/activate-account/activate-account.component';
import { PasswordResetComponent } from './admin-related/password-reset/password-reset.component';
import { VerificationEmailSentConfirmationComponent } from './admin-related/verification-email-sent-confirmation/verification-email-sent-confirmation.component';
import { TutorialEditorComponent } from './editor/tutorial/tutorial-editor/tutorial-editor.component';
import { ParagraphBoxEditorComponent } from './editor/component-editors/paragraph-box-editor/paragraph-box-editor.component';
import { TwoColumnBoxEditorComponent } from './editor/component-editors/two-column-box-editor/two-column-box-editor.component';
import { ImageEditorComponent } from './editor/component-editors/image-editor/image-editor.component';
import { PortraitEditorComponent } from './editor/component-editors/portrait-editor/portrait-editor.component';
import { LinkBoxEditorComponent } from './editor/component-editors/link-box-editor/link-box-editor.component';
import { ContactFormComponent } from './contact-form/contact-form.component';
import { SiteAnalyticsComponent } from './site-analytics/site-analytics.component';
import { DataPlanDisplayComponent } from './admin-related/data-plan-display/data-plan-display.component';
import { EditorOptionsComponent } from './editor/editor-options/editor-options.component';
import { LeafNavBarComponent } from './leaves/leaf-nav-bar/leaf-nav-bar.component';
import { SiteTitleEditorComponent } from './editor/site-title-editor/site-title-editor.component';
import { PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    AdminComponent,
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
    LeafNavBarEditorComponent,
    AnaComponent,
    LogoutComponent,
    PopInComponent,
    AboutComponent,
    AdminAccountComponent,
    ActivateAccountComponent,
    PasswordResetComponent,
    VerificationEmailSentConfirmationComponent,
    TutorialEditorComponent,
    ParagraphBoxEditorComponent,
    TwoColumnBoxEditorComponent,
    ImageEditorComponent,
    PortraitEditorComponent,
    LinkBoxEditorComponent,
    ContactFormComponent,
    SiteAnalyticsComponent,
    DataPlanDisplayComponent,
    EditorOptionsComponent,
    LeafNavBarComponent,
    SiteTitleEditorComponent,
    PrivacyPolicyComponent
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
