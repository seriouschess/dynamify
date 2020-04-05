import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LiveSiteComponent } from './live-site/live-site.component';
import { AdminComponent } from './admin/admin.component';
import { TutorialComponent } from './tutorial/tutorial.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomepageComponent } from './homepage/homepage.component';

const routes: Routes = [
    { path: '', component: HomepageComponent, pathMatch: 'full' },
    { path: 'base/admin', component: AdminComponent },
    { path: 'base/tutorial', component: TutorialComponent },
    { path: ':leaf_name', component: LiveSiteComponent },
    { path: '**', component: PageNotFoundComponent }
  ]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppFrontendRoutesModule { }
