import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { RegisterComponent } from './register/register.component';
import { StartComponent } from './start/start.component';
import { PriceListComponent } from './price-list/price-list.component';
import { UnregistredComponent } from './unregistred/unregistred.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { LogoutComponent } from './logout/logout.component';
import { ScheduleAdminComponent } from './components/schedule-admin/schedule-admin.component';
import { HeaderComponent} from './header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    StartComponent,
    PriceListComponent,
    LogoutComponent,
    ScheduleComponent,
    ScheduleAdminComponent,
    UnregistredComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
