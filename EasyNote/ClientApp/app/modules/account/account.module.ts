import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistrationFormComponent } from '../account/registration-form/registration-form.component';
import { LoginFormComponent } from '../account/login-form/login-form.component';  
import { UserService } from 'ClientApp/app/services/user.service';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    RegistrationFormComponent,
    LoginFormComponent,
    
  ],
  imports: [
    CommonModule,
    FormsModule,  
    SharedModule,
  ],
  exports: [
    RegistrationFormComponent,
    LoginFormComponent,
    
  ],
  providers: [UserService]
})
export class AccountModule { }
