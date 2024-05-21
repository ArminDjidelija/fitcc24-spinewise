import {Injectable} from "@angular/core";
import * as signalR from "@aspnet/signalr";
import {MyConfig} from "../../MyConfig";
@Injectable({providedIn:'root'})
export class SignalRService{
  public data:any;
  private hubConnection: signalR.HubConnection | undefined;
  public startConnection=()=>{
    this.hubConnection=new signalR.HubConnectionBuilder()
      .withUrl(MyConfig.api_address+"/signalr")
      .build();
    this.hubConnection
      .start()
      .then(()=>console.log("Connection started"))
      .catch(err=>console.log("Error while starting signalR"));
  }
  public addNotificationListener(callback: (message: string) => void) {
    this.hubConnection!.on('user sitting data', (data) => {
      callback(data);
    });
  }
}
