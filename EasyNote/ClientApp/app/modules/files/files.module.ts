import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { FilesService } from 'ClientApp/app/services/files.service';
import { AddingFileFormComponent } from './addingFile-form/addingFile-form.component';
import { EditFileFormComponent } from './editFile-form/editFile-form.component';



@NgModule({
  declarations: [
    AddingFileFormComponent,
    EditFileFormComponent
  ],
  imports: [
    CommonModule,
    FormsModule,  
    SharedModule,
  ],
  exports: [
    AddingFileFormComponent,
    EditFileFormComponent,
  ],
  providers: [FilesService]
})
export class FilesModule { }
