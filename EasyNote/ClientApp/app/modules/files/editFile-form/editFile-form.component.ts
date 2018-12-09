import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { FileAdd } from '../model/file.add.interface';
import { FilesService } from '../../../services/files.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-editFile-form',
  templateUrl: './editFile-form.component.html',
  styleUrls: ['./editFile-form.component.scss']
})
export class EditFileFormComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;

  file: Observable<FileAdd>;

  constructor(private filesService: FilesService) {

  }

  getFile({ value}: { value: FileAdd}) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    this.file = this.filesService.getFile( value.id);
   


  }
  saveFile({ value}: { value: FileAdd}) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    this.file = this.filesService.saveFile( value.id ,value.content);
   


  }
  ngOnInit() {
    
  }
  
  
}
