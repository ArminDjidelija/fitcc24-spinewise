import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../../MyConfig";

@Injectable({providedIn:'root'})
export class ChairmodelDataservice{
  constructor(private http:HttpClient) {
  }

  getAllChairModels(){
    var url=MyConfig.api_address+"/chairmodels/getall";
    return this.http.get(url);
  }

  postChairModel(name1:string, dateOfAdding1:Date){
    var url=MyConfig.api_address+"/chairmodel/add";
    var obj={
      name:name1,
      dateOfAdding:dateOfAdding1
    };
    return this.http.post(url, obj);
  }
}
