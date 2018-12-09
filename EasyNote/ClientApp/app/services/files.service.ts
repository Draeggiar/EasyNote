import { Injectable } from '@angular/core';
import { ConfigService } from './config.service';
import { Http, Headers } from '@angular/http';
import { map } from 'rxjs/operators';

@Injectable()

export class FilesService {
  baseUrl: String

  constructor(private http: Http, private configService: ConfigService) {
    this.baseUrl = configService.getApiURI() + '/files';
  }


  getFile(id : number) {
    const headers = this.configService.createAuthHeaders();
    return this.http.get(this.baseUrl + '/get/{'+id+'}', { headers })
      .pipe(map(res => res.json()));
  }
  getFilesList() {
    const headers = this.configService.createAuthHeaders();
    return this.http.get(this.baseUrl + '/list', { headers })
      .pipe(map(res => res.json()));
  }
  addFile(name: string, author: string) {
    let body = JSON.stringify({ name, author});
    const headers = this.configService.createAuthHeaders();
    return this.http.post(this.baseUrl + '/create', { body, headers })
      .pipe(map(res => res.json()));
  }
  saveFile(id: number, content: string) {
    let body = JSON.stringify({ id, content});
    const headers = this.configService.createAuthHeaders();
    return this.http.put(this.baseUrl + '/update', { body, headers })
      .pipe(map(res => res.json()));
  }

}