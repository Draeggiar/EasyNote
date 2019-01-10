import { Component, OnInit } from '@angular/core';
import { File } from '../model/file.interface';
import { FilesService } from '../../../services/files.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'ClientApp/app/services/user.service';

@Component({
  selector: 'app-editFile-form',
  templateUrl: './editFile-form.component.html',
  styleUrls: ['./editFile-form.component.scss']
})
export class EditFileFormComponent implements OnInit {
  fileId: string;
  file: File;
  //TODO trzeba jakoś sprawdzać czy plik jest nowy, żeby nie wyświetlać przycisków do usuwania i edycji

  constructor(private filesService: FilesService, private route: ActivatedRoute,
    private userService: UserService, private router: Router) {
  }

  getFile(id: string) {
    //TODO Spinner przy ładowaniu pliku
    this.filesService.getFile(id).subscribe(f => this.file = f);
  }

  saveFile({ value }: { value: File }) {
    //TODO Spinner przy zapsie
    if (this.fileId !== '0') {
      this.filesService.saveFile(this.fileId, value.name, value.content);
    }
    else {
      let newFileId;
      this.filesService.createFile(value.name, this.userService.getNameOfLoggedUser(), value.content)
        .subscribe(f => newFileId = f);
      this.router.navigateByUrl(this.router.url.replace("id", newFileId));
    }
  }

  deleteFile() {
    this.filesService.deleteFile(this.fileId);
    this.router.navigateByUrl('/home');
  }

  checkoutFile(){
    //TODO Edycja pliku
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.fileId = params.get("id");

      if (this.fileId !== '0') {
        this.getFile(this.fileId);
      }
      else {
        this.file = <File>{ name: "", content: "" };
      }
    });
  }
}
