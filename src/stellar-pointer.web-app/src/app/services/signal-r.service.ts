import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private celestialBodies: string[];
  private hubConnection: signalR.HubConnection;
  
  constructor() { }

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chart')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log("Successfully connection."))
      .catch(err => console.error("Error has occurred. Error:" + err));
  }

  onGetVisibleCelestialBodies(): void {
    this.hubConnection.on('get-visible-celestial-bodies', (celestialBodies) => {

    });
  }
}
