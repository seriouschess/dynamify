import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LiveSiteComponent } from './live-site/live-site.component';
import { AdminComponent } from './admin/admin.component';
import { SiteEditorComponent } from './site-editor/site-editor.component';

const routes: Routes = [
    { path: '', component: LiveSiteComponent, pathMatch: 'full' },
    { path: 'admin_display', component: AdminComponent },
    { path: 'edit_site/:current_site_id/:current_admin_id', component: SiteEditorComponent,  }
  ]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppFrontendRoutesModule { }
