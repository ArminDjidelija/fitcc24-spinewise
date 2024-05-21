import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {MyConfig} from "../../MyConfig";

@Injectable({providedIn:'root'})
export class AuthLogoutEndpoint{
  constructor(public httpClient:HttpClient) { }

  SignOut(tokenValue: string) {
    let url=MyConfig.api_address+`/auth/logout`;
    //const headers = new HttpHeaders({ 'Content-Type': 'application/json', 'auth-token': tokenValue });
   // console.log("doing logout: ", tokenValue);
    var obj={

    };
    return this.httpClient.post(url, obj); //, {'headers': headers}
  }}
