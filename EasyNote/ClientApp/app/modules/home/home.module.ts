import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { AccountModule } from '../account/account.module';
import { AppRoutingModule } from 'ClientApp/app/app-routing.module';
import { UserService } from 'ClientApp/app/services/user.service';
import { FilesService } from 'ClientApp/app/services/files.service';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    AccountModule,
    AppRoutingModule,
  ],
  providers: [
    UserService
  ]
})
export class HomeModule { }
