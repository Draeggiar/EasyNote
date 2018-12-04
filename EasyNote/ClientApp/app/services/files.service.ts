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

  getFilesList() {
    const headers = this.configService.createAuthHeaders();
    return this.http.get(this.baseUrl + '/list', { headers })
      .pipe(map(res => res.json()));
  }
}