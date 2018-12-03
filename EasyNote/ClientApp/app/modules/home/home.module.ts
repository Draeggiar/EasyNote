import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { AccountModule } from '../account/account.module';
import { AppRoutingModule } from 'ClientApp/app/app-routing.module';
import { FilesModule } from '../files/files.module';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    AccountModule,
    FilesModule,
    AppRoutingModule,
  ],
})
export class HomeModule { }
