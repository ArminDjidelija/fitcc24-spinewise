import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../../MyConfig";

@Injectable({providedIn:'root'})
export class ChairmodelDataservice{
  constructor(private http:HttpClient) {
  }

  getAllChairModels(){
    var url=MyConfig.api_address+"/chairmodel/getall";
    return this.http.get(url);
  }
  deleteChairModel(idObj:any){
    var url=MyConfig.api_address+"/chairmodel/delete?id="+idObj;

    return this.http.delete(url);
  }
}
