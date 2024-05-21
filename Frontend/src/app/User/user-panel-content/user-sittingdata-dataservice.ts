import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {MyConfig} from "../../MyConfig";

@Injectable({providedIn:'root'})
export class UserSittingdataDataservice{
  constructor(private http:HttpClient) {
  }

  GetLastDay(){
    var url=MyConfig.api_address+"/logsdata/getlastdayminutes";
    return this.http.get(url);
  }

  GetLastSpecificDays(x:any){
    var url=MyConfig.api_address+"/lastndays/get?request="+x.toString();
    return this.http.get<LastDaysResponse[]>(url);
  }

  GetGoodBadRatioData(x:any){
    var url=MyConfig.api_address+"/goodbadratio/get?request="+x.toString();
    return this.http.get<GoodBadRatioResponse[]>(url);
  }
  GetWarning(){
    var url=MyConfig.api_address+"/warning/get";
    return this.http.get<WarningInfo>(url);
  }

  GetOpenAiData(){
    var url=MyConfig.api_address+"/aicontroller/getresponse";
    return this.http.get<AiResponse>(url);

  }

  GetHeatmapData(date:Date){
    var url=MyConfig.api_address+"/heatmap?Datum="+date.toString();
    return this.http.get<HeatmapData>(url);
  }
  GetSittingData(date:Date){
    var url=MyConfig.api_address+"/data?Datum="+date.toString();
    return this.http.get<ChairSittingData>(url);
  }
  GetGraphDay(date:Date){
    var url=MyConfig.api_address+"/daygraph?Datum="+date.toString();
    return this.http.get<GrafDan[]>(url);
  }
  GetPostureGraph(dani:number=3){
    var url=MyConfig.api_address+"/posture?Days="+dani.toString();
    return this.http.get<WrongPosture[]>(url);
  }
}

export interface AiResponse{
  message:string
}
export interface LastDaysResponse{
  date:string,
  totalMinutes:number
}
export interface GoodBadRatioResponse {
  date: string
  countGood: number
  countBad: number
  ratioGood: number
  ratioBad: number
}

export interface WarningInfo {
  badCount: number
  goodCount: number
  goodBadRatio: number
  badGoodRatio: number
  badCount5: number
  goodCount5: number
  goodBadRatio5: number
  badGoodRatio5: number
}

export interface HeatmapData{
  chairId: number
  datum: string
  brojZapisa: number
  s1Percentage: number
  s2Percentage: number
  s3Percentage: number
}

export interface ChairSittingData{
  sittingHours: number
  sittingMinutes:number
  badPercentage: number
  goodPercentage: number
  s1Percentage: number
  s2Percentage: number
  s3Percentage: number
  datum:Date
}

export interface GrafDan {
  intervalStart: string
  intervalEnd: string
  sittingMinutes: number
  start: string
  end: string
  interval:string
}
export interface WrongPosture{
  datum: string
  good: number
  datumString:string
}
