import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {AuthLoginRequest} from "./auth-login-request";
import {MyConfig} from "../../MyConfig";
import {AuthLoginResponse} from "./auth-login-response";

@Injectable({providedIn:'root'})
export class AuthLoginEndpoint {
  constructor(private readonly http:HttpClient) {
  }

  Login(request:AuthLoginRequest){
    let url=MyConfig.api_address+"/auth/login";
    return this.http.post<AuthLoginResponse>(url, request);
  }

  Logout(){
    let url=MyConfig.api_address+"/auth/logout";
    var obj={

    };
    return this.http.post(url, obj);
  }
}
