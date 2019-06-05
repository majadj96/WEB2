import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
// import { AdminComponent } from './admin/admin.component';

// import { AuthGuard } from './auth/auth.guard';
 import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './register/register.component';
import { StartComponent } from './start/start.component';
import { PriceListComponent } from './price-list/price-list.component';
import { LogoutComponent } from './logout/logout.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { ScheduleAdminComponent } from './components/schedule-admin/schedule-admin.component';
import { ProfileViewComponent } from './profile-view/profile-view.component';
import { ValidateProfileComponent } from './validate-profile/validate-profile.component';
import { ValidateTicketComponent } from './validate-ticket/validate-ticket.component';

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
    path: 'profileView',
    component: ProfileViewComponent,
  },
  { 
    path: 'register', 
    component: RegisterComponent, 
  },
  { 
    path: 'start', 
    component: StartComponent, 
  },
  { 
    path: 'price', 
    component: PriceListComponent, 
  },
  { 
    path: 'logout', 
    component: LogoutComponent, 
  },
  { 
    path: 'schedule', 
    component: ScheduleComponent, 
  },
  { 
    path: 'validate-profile', 
    component: ValidateProfileComponent, 
  },
  { 
    path: 'validate-ticket', 
    component: ValidateTicketComponent, 
  }
  ,
  { 
    path: 'schedule-admin', 
    component: ScheduleAdminComponent, 
  }
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
