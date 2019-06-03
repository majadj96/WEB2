import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
// import { AdminComponent } from './admin/admin.component';

// import { AuthGuard } from './auth/auth.guard';
 import { LoginComponent } from './auth/login/login.component';

// import { CrisisCenterComponent } from './crisis-center/crisis-center/crisis-center.component';
// import { CrisisCenterHomeComponent } from './crisis-center/crisis-center-home/crisis-center-home.component';
// import { CrisisDetailComponent } from './crisis-center/crisis-detail/crisis-detail.component';
// import { CrisisListComponent } from './crisis-center/crisis-list/crisis-list.component';
// import { CrisisDetailResolverService } from './crisis-center/crisis-detail-resolver.service';

const appRoutes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  { 
    path: 'register', 
    component: LoginComponent, 
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
