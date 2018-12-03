import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {HomeComponent} from './modules/home/home.component';
import { RegistrationFormComponent } from './modules/account/registration-form/registration-form.component';
import { LoginFormComponent } from './modules/account/login-form/login-form.component';
import { AddingFileFormComponent } from './modules/files/addingFile-form/addingFile-form.component';



const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginFormComponent },
  { path: 'register', component: RegistrationFormComponent },
  { path: 'addingFile', component: AddingFileFormComponent },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }