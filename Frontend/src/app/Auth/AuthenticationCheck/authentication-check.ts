import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../MyConfig";

@Injectable({providedIn:'root'})
export class AuthenticationCheck{
  constructor(private readonly httpClient:HttpClient) { }

  IsSuperAdmin(){
    let url=MyConfig.api_address+"/auth/isSuperAdmin";
    return this.httpClient.get(url, {observe:'response'});
  }
  IsAdmin(){
    let url=MyConfig.api_address+"/auth/isAdmin";
    return this.httpClient.get(url, {observe:'response'});
  }
  IsUser(){
    let url=MyConfig.api_address+"/auth/isUser";
    return this.httpClient.get(url, {observe:'response'});
  }

}
