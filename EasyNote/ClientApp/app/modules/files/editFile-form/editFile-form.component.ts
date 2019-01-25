import { Component, OnInit } from '@angular/core';
import { File } from '../model/file.interface';
import { FilesService } from '../../../services/files.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'ClientApp/app/services/user.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-editFile-form',
  templateUrl: './editFile-form.component.html',
  styleUrls: ['./editFile-form.component.scss']
})

export class EditFileFormComponent implements OnInit {
  fileId: string;
  file: File;
  isLoaded: Promise<boolean>
  filesList: Observable<File>;

  constructor(private filesService: FilesService, private route: ActivatedRoute,
    private userService: UserService, private router: Router) {
  }

  getFile(id: string) {
    this.filesService.getFile(id)
      .subscribe(f => {
        this.file = f;
        this.file.isNew = false;
        this.file.isCheckouted = false;
        this.isLoaded = Promise.resolve(true);
      });
  }

  saveFile({ value }: { value: File }) {
    if (this.fileId !== '0') {
      this.filesService.saveFile(this.fileId, value.name, value.content)
        .subscribe(() => {
          this.file.isCheckouted = false;
          this.filesService.refreshFilesList();
        });
    }
    else {
      this.filesService.createFile(value.name, this.userService.getNameOfLoggedUser(), value.content)
        .subscribe(f => {
          this.router.navigateByUrl(this.router.url.replace("0", f));
          this.filesService.refreshFilesList();
        });
    }
  }

  deleteFile() {
    this.filesService.deleteFile(this.fileId);
    this.router.navigateByUrl('/home');
    this.filesService.refreshFilesList();
  }

  checkoutFile() {
    this.filesService.checkoutFile(this.fileId, this.file.isCheckouted)
      .subscribe(r => {
        if (r.canCheckout === false)
          window.alert("Plik zablokowany przez użytkownika " + r.lockedBy);
        else
          this.file.isCheckouted = !this.file.isCheckouted;
      });
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.fileId = params.get("id");

      if (this.fileId !== '0') {
        this.getFile(this.fileId); //Uwaga, to jest asynchroniczne
      }
      else {
        this.file = <File>{ name: "", content: "", isNew: true, isCheckouted: true };
        this.isLoaded = Promise.resolve(true);
      }
    });
  }

  ngOnDestroy() {
    this.file = null;
  }
}
