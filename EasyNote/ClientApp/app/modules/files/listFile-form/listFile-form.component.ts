import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { FileAdd } from '../model/file.add.interface';
import { FileService } from '../../../services/file.service';

@Component({
  selector: 'app-ListFile-form',
  templateUrl: './listFile-form.component.html',
  styleUrls: ['./listFile-form.component.scss']
})
export class ListFileFormComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;

  constructor(private fileService: FileService,  private router: Router) { }

  ngOnInit() {
  }

  save({ value, valid }: { value: FileAdd, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    if (valid) {
      this.fileService.save(value.name, value.author, value.content)
        .pipe(
          finalize(() => this.isRequesting = false))
        .subscribe(
          result => {
            if (result) {
              this.router.navigate(['/home']);
            }
          },
          errors => this.errors = errors);
    }
  }
}
