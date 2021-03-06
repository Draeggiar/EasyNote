import { Injectable } from '@angular/core';
import { Headers } from '@angular/http';
 
@Injectable()
export class ConfigService {
     
    _apiURI : string;
 
    constructor() {
        this._apiURI = 'http://localhost:64794';
     }
 
     getApiURI() {
         return this._apiURI;
     }   
     
     createAuthHeaders() : Headers{
        const headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let authToken = localStorage.getItem('auth_token');
        headers.append('Authorization', `Bearer ${authToken}`);
        return headers;
     }
}
 