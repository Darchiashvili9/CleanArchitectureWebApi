import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../helpers/authGuard';
import { Role } from '../models';

import { AuthLayoutComponent } from './../components/auth-layout/auth-layout.component';
import { AppLayoutComponent } from './../components/app-layout/app-layout.component';
import { LoginComponent } from './../components/login/login.component';
import { RegisterComponent } from './../components/register/register.component';
import { HomeComponent } from './../components/home/home.component';
import { SettingsComponent } from './../components/settings/settings.component';

const routes: Routes = [
  { path: 'login', component: AuthLayoutComponent,
   children: [{ path: '', component: LoginComponent }]},
  { path: 'register', component: AuthLayoutComponent,
   children: [{ path: '', component: RegisterComponent }] },
  { path: 'quiz', component: AppLayoutComponent,
   children: [{ path: '', component: HomeComponent }], canActivate: [AuthGuard] },
  { path: 'users', component: AppLayoutComponent },
  { path: 'quotes', component: AppLayoutComponent },
  { path: 'settings', component: AppLayoutComponent,
   children: [{ path: '', component: SettingsComponent }], canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'quiz' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
