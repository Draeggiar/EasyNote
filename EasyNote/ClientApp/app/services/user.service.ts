import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ConfigService } from './config.service';
import { BaseService } from "./base.service";

@Injectable()

export class UserService extends BaseService {

  baseUrl: string = '';

  private loggedIn = false;
  private loggedUser = '';

  constructor(private http: Http, private configService: ConfigService) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    this.loggedUser = localStorage.getItem('loggedUser');
    this.baseUrl = configService.getApiURI();
  }

  register(email: string, password: string, userName: string): Observable<boolean> {
    let body = JSON.stringify({ email, password, userName });
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    return this.http.post(this.baseUrl + "/accounts/register", body, options)
      .pipe(map(res => true));
  }

  login(email, password) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this.http
      .post(
        this.baseUrl + '/accounts/login',
        JSON.stringify({ email, password }), { headers }
      )
      .pipe(
        map(res => res.json()),
        map(res => {
          localStorage.setItem('auth_token', res.authToken);
          localStorage.setItem('loggedUser', res.userName);
          this.loggedIn = true;
          return true;
        }));
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  getNameOfLoggedUser() {
    return this.loggedUser;
  }
}