import { Component, OnInit } from '@angular/core';
import { FilesService } from 'ClientApp/app/services/files.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'files-list',
  templateUrl: './files-list.component.html',
  styleUrls: ['./files-list.component.scss'],
})

export class FilesListComponent implements OnInit {
  filesList: Observable<File>;

  constructor(private filesService: FilesService) {

  }

  ngOnInit() {
    this.filesList = this.filesService.getFilesList();
  }

}