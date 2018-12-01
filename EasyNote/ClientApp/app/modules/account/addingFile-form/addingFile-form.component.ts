import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { FileAdd } from '../model/file.add.interface';
import { FileService } from '../../../services/file.service';

@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.scss']
})
export class AddingFileComponent implements OnInit {

  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;

  constructor(private fileService: FileService, @Inject('BASE_URL') baseUrl: string, private router: Router) { }

  ngOnInit() {
  }

  addFile({ value, valid }: { value: FileAdd, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    if (valid) {
      this.fileService.save(value.name, value.content)
        .pipe(
          finalize(() => this.isRequesting = false))
        .subscribe(
          result => {
            if (result) {
              this.router.navigate(['/text-edit'], { queryParams: { brandNew: true, name: value.name } });
            }
          },
          errors => this.errors = errors);
    }
  }
}
