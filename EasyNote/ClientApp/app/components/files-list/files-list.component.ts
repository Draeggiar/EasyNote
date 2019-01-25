import { Component, OnInit, HostListener } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { FilesService } from 'ClientApp/app/services/files.service';
import { File } from "../../modules/files/model/file.interface";
import { UserService } from 'ClientApp/app/services/user.service';

@Component({
  selector: 'files-list',
  templateUrl: './files-list.component.html',
  styleUrls: ['./files-list.component.scss'],
})

export class FilesListComponent implements OnInit {
  filesList: Observable<File>;
  subscription: Subscription;

  constructor(private filesService: FilesService, private userService: UserService) {
  }

  ngOnInit() {
    this.getFilesList();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getFilesList() {
    if (this.userService.isLoggedIn()) {
      this.subscription = this.filesService.getFilesList()
        .subscribe(list => {
          this.filesList = list;
        });
      this.filesService.refreshFilesList();
    }
  }
}