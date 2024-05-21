import {Injectable} from "@angular/core";
import {AuthLoginEndpoint} from "../Login/auth-login-endpoint";
import {AuthenticationCheck} from "./authentication-check";
import {AuthToken} from "../AuthToken/auth-token";

@Injectable({providedIn:'root'})
export class GetAuth{
  constructor(private dataService:AuthenticationCheck) {
  }

  getAuthorizationToken():AuthToken|null{
    let tokenString=window.localStorage.getItem("auth-token")??"";
    try {
      return JSON.parse(tokenString);
    }
    catch (e){
      return null;
    }
  }

  async IsSuperAdmin():Promise<boolean>{
    var isAdmin=false;

    const response=await this.dataService.IsSuperAdmin().toPromise();

    if(response){
      if(response.status==200)
      {
        isAdmin=true;
        //console.log("is super admin (check await): ", isAdmin);
      }
      else if(response.status==401){
        isAdmin=false;
       // console.log("is super admin (check await): ", isAdmin);
      }
      else {
       // console.log("error with sign in");
      }
    }

    return isAdmin;
  }

  async IsAdmin():Promise<boolean>{
    var isAdmin=false;

    const response=await this.dataService.IsAdmin().toPromise();

    if(response){
      if(response.status==200)
      {
        isAdmin=true;
       // console.log("is admin (check await): ", isAdmin);
      }
      else if(response.status==401){
        isAdmin=false;
       // console.log("is admin (check await): ", isAdmin);
      }
      else {
       // console.log("error with sign in");
      }
    }

    return isAdmin;
  }

  async IsUser():Promise<boolean>{
    var isUser=false;

    const response=await this.dataService.IsUser().toPromise();

    if(response){
      if(response.status==200)
      {
        isUser=true;
      //  console.log("is user (check await): ", isUser);
      }
      else if(response.status==401){
        isUser=false;
      //  console.log("is user (check await): ", isUser);
      }
      else {
      //  console.log("error with sign in");
      }
    }

    return isUser;
  }

}
