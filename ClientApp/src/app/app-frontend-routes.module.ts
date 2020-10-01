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

const routes: Routes = [
    { path: '', component: HomepageComponent, pathMatch: 'full' },
    { path: 'base/about', component: AboutComponent },
    { path: 'base/admin', component: AdminComponent },
    { path: 'base/account', component: AdminAccountComponent },
    { path: 'base/tutorial', component: TutorialComponent },
    { path: 'base/password/reset/:email/:token', component: PasswordResetComponent },
    { path: 'base/activate/:email/:token', component: ActivateAccountComponent },
    { path: 'base/not-found', component: PageNotFoundComponent },
    { path: 'base/logout', component: LogoutComponent },
    { path: ':leaf_url', component: LeafComponent },
    { path: '**', component: PageNotFoundComponent }
  ]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppFrontendRoutesModule { }
