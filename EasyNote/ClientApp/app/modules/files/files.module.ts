import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { FilesService } from 'ClientApp/app/services/files.service';
import { EditFileFormComponent } from './editFile-form/editFile-form.component';
import { UserService } from 'ClientApp/app/services/user.service';

@NgModule({
  declarations: [
    EditFileFormComponent
  ],
  imports: [
    CommonModule,
    FormsModule,  
    SharedModule,
  ],
  exports: [
    EditFileFormComponent,
  ],
  providers: [FilesService, UserService]
})
export class FilesModule { }
