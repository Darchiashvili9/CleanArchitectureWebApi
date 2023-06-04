import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { SharedModule } from './modules/shared.module';

import { AuthInterceptor } from './interceptors/auth.interceptor';
import { AuthErrorInterceptor } from './interceptors/auth-error.interceptor';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { SettingsComponent } from './components/settings/settings.component';
import { AuthLayoutComponent } from './components/auth-layout/auth-layout.component';
import { AppLayoutComponent } from './components/app-layout/app-layout.component';
import { QuestionComponent } from './components/question/question.component';
import { DialogComponent } from './components/dialog/dialog.component';
import { UsersComponent } from './components/users/users.component';
import { QuotesComponent } from './components/quotes/quotes.component';
import { QuotesDialogComponent } from './components/quotes-dialog/quotes-dialog.component';
import { UsersDialogComponent } from './components/users-dialog/users-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    SettingsComponent,
    AuthLayoutComponent,
    AppLayoutComponent,
    QuestionComponent,
    DialogComponent,
    UsersComponent,
    QuotesComponent,
    QuotesDialogComponent,
    UsersDialogComponent
  ],
  imports: [
    SharedModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
