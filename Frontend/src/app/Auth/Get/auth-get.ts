import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../MyConfig";

@Injectable({providedIn:'root'})
export class AuthGet{
  constructor(private http:HttpClient) {
  }

  getRole(){
    var url =MyConfig.api_address+"/role/get";
    return this.http.get<GetRole>(url);
  }
}

export interface GetRole{
  role:string
}
