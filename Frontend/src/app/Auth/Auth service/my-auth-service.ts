import {Injectable} from "@angular/core";
import {AuthLoginEndpoint} from "../Login/auth-login-endpoint";
import {AuthLogoutEndpoint} from "../Logout/auth-logout-endpoint";
import {AuthLoginRequest} from "../Login/auth-login-request";
import {tap} from "rxjs";
import {AuthToken} from "../AuthToken/auth-token";
import {Router} from "@angular/router";

@Injectable({providedIn:'root'})
export class MyAuthService{
  constructor(
    private authLoginEndpoint: AuthLoginEndpoint,
    //private authGetEndpoint: AuthGetEndpoint,
    private authLogoutEndpoint: AuthLogoutEndpoint,
    private router:Router,
  ) {
  }

  signIn(loginRequest:AuthLoginRequest){
    return this.authLoginEndpoint.Login(loginRequest)
      .pipe(
        tap(r=>{
          this.setLoggedUser(r.authTokenValue);
          //this.setRole(r.role);
        })
      )
  }
  setRole(role:string){
    if (role == null){
      window.localStorage.setItem("role", '');
    }
    else {
      window.localStorage.setItem("role", JSON.stringify(role));
    }
  }
  async signOut():Promise<void>{
   // console.log("Signing out");

    const token = this.getAuthorizationToken();

    if (token) {
      try {
        this.authLogoutEndpoint.SignOut(token).subscribe();
      } catch (err) {
        // Handle error if needed
      }

      this.setLoggedUser(null);
      await this.router.navigate([""]);
    }
    //console.log("We signed out successfully");

  }
  setLoggedUser(x:string | null){
    if (x == null){
      window.localStorage.setItem("auth-token", '');
    }
    else {
      window.localStorage.setItem("auth-token", JSON.stringify(x));
    }
  }

  getAuthorizationToken():string | null {
    let tokenString = window.localStorage.getItem("auth-token")??"";
    try {
      return JSON.parse(tokenString);
    }
    catch (e){
      return null;
    }
  }
}
