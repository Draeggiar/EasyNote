import { Injectable } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import { ConfigService } from './config.service';
import * as signalR from '@aspnet/signalr';

@Injectable()
export class SocketService {
  private hubConnection: HubConnection

  constructor(private configService: ConfigService) {
    var hubUrl = configService.getApiURI() + "/easynotehub";
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .configureLogging(signalR.LogLevel.Information)
      .build();
  }

  //TODO odświeżanie listy plików
}