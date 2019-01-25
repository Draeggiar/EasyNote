import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Http, Headers } from '@angular/http';
import { map } from 'rxjs/operators';
import { Observable, Subject } from 'rxjs';
import { File } from "../modules/files/model/file.interface";

@Injectable()

export class FilesService {
  baseUrl: String
  private subject = new Subject<any>();

  constructor(private http: Http, private configService: ConfigService) {
    this.baseUrl = configService.getApiURI() + '/files';
  }

  getFile(id: string) {
    const headers = this.configService.createAuthHeaders();
    return this.http.get(this.baseUrl + '/get/' + id, { headers })
      .pipe(map(res => res.json()));
  }

  getFilesList() {
    return this.subject.asObservable();
  }

  refreshFilesList() {
    const headers = this.configService.createAuthHeaders();
    this.subject.next(
      this.http.get(this.baseUrl + '/list', { headers })
        .pipe(map(res => res.json()))
    );
  }

  createFile(name: string, author: string, content: string) {
    let body = JSON.stringify({ name, author, content });
    const headers = this.configService.createAuthHeaders();
    return this.http.post(this.baseUrl + '/create', body, { headers })
      .pipe(map(res => res.json()));
  }

  saveFile(id: string, name: string, content: string) {
    let body = JSON.stringify({ id, name, content });
    const headers = this.configService.createAuthHeaders();
    return this.http.put(this.baseUrl + '/update', body, { headers });
  }

  deleteFile(id: string) {
    const headers = this.configService.createAuthHeaders();
    return this.http.get(this.baseUrl + '/delete/' + id, { headers }).subscribe();
  }

  checkoutFile(id: string, cancelCheckout: boolean) {
    const headers = this.configService.createAuthHeaders();

    return this.http.get(this.baseUrl + '/checkout/' + id, { headers })
      .pipe(map(res => res.json()));
  }
}