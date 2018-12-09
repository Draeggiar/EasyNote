import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { FileAdd } from '../model/file.add.interface';
import { FilesService } from '../../../services/files.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-addingFile-form',
  templateUrl: './addingFile-form.component.html',
  styleUrls: ['./addingFile-form.component.scss']
})
export class AddingFileFormComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;

  file: Observable<FileAdd>;

  constructor(private filesService: FilesService) {

  }
  saveFile({ value}: { value: FileAdd}) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    this.file = this.filesService.saveFile( value.name , value.author, value.content);
   


  }
  ngOnInit() {
    
  }
  
  
}
