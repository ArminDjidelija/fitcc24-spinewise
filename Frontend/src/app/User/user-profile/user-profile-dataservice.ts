import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../MyConfig";

@Injectable({providedIn:'root'})
export class UserProfileDataservice{
  constructor(private http:HttpClient) {
  }
  getDetails(){
    var url=MyConfig.api_address+"/korisnik";
    return this.http.get<UserGet>(url);
  }
  updateUser(user:UserUpdate){
    var url=MyConfig.api_address+"/korisnik";
    return this.http.put(url, user);
  }
}

export interface UserGet{
  firstName:string,
  lastName:string
}
export interface UserUpdate{
  firstname:string,
  lastname:string,
  password:string,
  newPassword:string,
  newPasswordConfirm:string
}
