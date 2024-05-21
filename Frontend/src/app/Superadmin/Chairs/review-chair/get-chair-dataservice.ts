import {Injectable} from "@angular/core";
import {provideAnimations} from "@angular/platform-browser/animations";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../../MyConfig";

@Injectable({providedIn:'root'})
export class GetChairDataservice{
  constructor(private http:HttpClient) {
  }

  GetAll(){
    let url=MyConfig.api_address+"/chair/getall";
    return this.http.get(url);
  }

  DeleteChair(deleteid:number){
    let url=MyConfig.api_address+"/chair/delete"+"?Id="+deleteid.toString();
    return this.http.delete(url);
  }
}
