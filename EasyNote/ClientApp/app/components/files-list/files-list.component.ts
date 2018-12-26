import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FilesService } from 'ClientApp/app/services/files.service';
import { File } from "../../modules/files/model/file.interface";

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
    //TODO Spinner przy ładowaniu listy plików
    this.filesList = this.filesService.getFilesList();
  }
}