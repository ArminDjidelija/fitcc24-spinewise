import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../../MyConfig";

@Injectable({providedIn:'root'})
export class AddChairDataservice{
  constructor(private http:HttpClient) {
  }

  getAllModels(){
    var url=MyConfig.api_address+"/chairmodel/getall";
    return this.http.get(url);
  }

  postChair(id:any){
    var url=MyConfig.api_address+"/chair/add";
    var model={
      chairModelId:id
    };
    return this.http.post(url, model);
  }
}
