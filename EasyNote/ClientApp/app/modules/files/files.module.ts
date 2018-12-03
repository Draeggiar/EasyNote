import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { FileService } from 'ClientApp/app/services/file.service';
import { AddingFileFormComponent } from './addingFile-form/addingFile-form.component';
import { ListFileFormComponent } from './listFile-form/listFile-form.component';


@NgModule({
  declarations: [
    AddingFileFormComponent,
    ListFileFormComponent,
    
  ],
  imports: [
    CommonModule,
    FormsModule,  
    SharedModule,
  ],
  exports: [
    AddingFileFormComponent,
    ListFileFormComponent,
  ],
  providers: [FileService]
})
export class FilesModule { }
