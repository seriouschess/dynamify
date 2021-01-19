import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin-related/admin/admin.component';
import { TutorialComponent } from './editor/tutorial/tutorial.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LeafComponent } from './leaves/leaf/leaf.component';
import { LogoutComponent } from './admin-related/logout/logout.component';
import { AboutComponent } from './about/about.component';
import { AdminAccountComponent } from './admin-related/admin-account/admin-account.component';
import { ActivateAccountComponent } from './admin-related/activate-account/activate-account.component';
import { PasswordResetComponent } from './admin-related/password-reset/password-reset.component';
import { VerificationEmailSentConfirmationComponent } from './admin-related/verification-email-sent-confirmation/verification-email-sent-confirmation.component';
import{ PrivacyPolicyComponent } from './privacy-policy/privacy-policy.component';

const routes: Routes = [
    { path: '', component: HomepageComponent, pathMatch: 'full' },
    { path: 'app/about', component: AboutComponent },
    { path: 'app/admin', component: AdminComponent },
    { path: 'app/account', component: AdminAccountComponent },
    { path: 'app/tutorial', component: TutorialComponent },
    { path: 'app/privacy', component: PrivacyPolicyComponent },
    { path: 'app/password/email/confirmation', component: VerificationEmailSentConfirmationComponent},
    { path: 'app/password/reset/:admin_id/:token', component: PasswordResetComponent },
    { path: 'app/activate/:admin_id/:token', component: ActivateAccountComponent },
    { path: 'app/not-found', component: PageNotFoundComponent },
    { path: 'app/logout', component: LogoutComponent },
    { path: ':leaf_url', component: LeafComponent },
    { path: '**', component: PageNotFoundComponent }
  ]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppFrontendRoutesModule { }
