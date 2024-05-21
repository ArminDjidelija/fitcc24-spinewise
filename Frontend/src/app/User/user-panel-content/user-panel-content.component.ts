import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {Chart} from "chart.js/auto";
import {
  AiResponse,
  ChairSittingData,
  GrafDan,
  UserSittingdataDataservice,
  WrongPosture
} from "./user-sittingdata-dataservice";
import {DatePipe, NgClass, NgIf} from "@angular/common";
import {interval, switchMap} from "rxjs";
import {FormsModule} from "@angular/forms";
import {SignalRService} from "../signalr/SignalRService";


@Component({
  selector: 'app-user-panel-content',
  standalone: true,
  imports: [
    NgClass,
    NgIf,
    FormsModule,
    DatePipe
  ],
  providers:[
    DatePipe
  ],
  templateUrl: './user-panel-content.component.html',
  styleUrl: './user-panel-content.component.css'
})
export class UserPanelContentComponent implements OnInit{
  @ViewChild('prvi') prvi:ElementRef;
  @ViewChild('drugi') drugi:ElementRef;
  @ViewChild('treci') treci:ElementRef;
  @ViewChild('cetvrti') cetvrti:ElementRef;
  @ViewChild('peti') peti:ElementRef;
  constructor(private dataService:UserSittingdataDataservice,
              private datePipe:DatePipe,
              public signalRService:SignalRService) {
    this.prvi=new ElementRef<any>(null);
    this.drugi=new ElementRef<any>(null);
    this.treci=new ElementRef<any>(null);
    this.cetvrti=new ElementRef<any>(null);
    this.peti=new ElementRef<any>(null);
  }
  ngOnInit(): void {
    this.getLastDays();
    this.getGoodBadRatio();
    this.getWarning();
    this.getAiReponse();
    this.getDanasnjiDatum();
    this.ChairSittingData();
    this.GetDayDistribution();
    this.getPostureGraph();
    this.signalRService.startConnection();
    this.signalRService.addNotificationListener((message)=>{
      this.ChairSittingData();
      this.getLastDays();
      this.getGoodBadRatio();
      this.ChairHeatmap();
    })
  }

  getDanasnjiDatum(){
    const danas=new Date();
    this.heatMapDate=this.datePipe.transform(danas, 'yyyy-MM-dd');
  }
intervalPause=20;
  chart1:any;
  chart2:any;
  chart3:any;
  chart4:any;
  chart5:any;
  createChart1(){
    if(this.chart1){
      this.chart1.destroy();
    }
    this.chart1 = new Chart('vrijeme', {
      type: 'bar',
      data: {
        labels: this.days,
        datasets: [
          {
            label: 'Minutes',
            data: this.minutes,
            type: 'bar', // Ovo postavlja tip na bar za glavni set podataka
            borderWidth: 2,
            order: 2, // Redosled crtanja za barove
            backgroundColor: 'rgba(75, 192, 192, 0.2)', // Boja ispune barova
            borderColor: 'rgba(75, 192, 192, 1)', // Boja linija oko barova
          },
          {
            type: 'line', // Ovo postavlja tip na line za dodatni set podataka
            label: 'Minutes line',
            data: this.minutes, // Podaci za dodatni set (može biti drugačiji niz podataka ako je potrebno)
            fill: false, // Da li popuniti prostor ispod linije
            order: 1, // Redosled crtanja za liniju
            borderColor: 'rgba(255, 99, 132, 1)', // Boja linije
            tension: 0.1, // Tenzija linije (0.0 - 1.0)

          },
        ],
      },
      options: {
        plugins:{
          title:{
            display:true,
            text:'Sitting time per day'
          }
        },
        scales: {
          y: {
            beginAtZero: true,
          },
        },
      },
    });
  }
  createChart2(){
    if(this.chart2){
      this.chart2.destroy();
    }
    this.chart2 = new Chart('goodbad', {
      type: 'bar',
      data: {
        labels: this.daysratio,
        datasets: [
          {
            label: 'Good posture %',
            data: this.goodpercentages,
            type: 'bar', // Ovo postavlja tip na bar za glavni set podataka
            borderWidth: 2,
            order: 2, // Redosled crtanja za barove
            backgroundColor: 'rgba(75, 192, 192, 0.2)', // Boja ispune barova
            borderColor: 'rgba(75, 192, 192, 1)', // Boja linija oko barova
          },
          {
            label: 'Bad posture %',
            data: this.badpercentages,
            type: 'bar', // Ovo postavlja tip na bar za glavni set podataka
            borderWidth: 2,
            order: 2, // Redosled crtanja za barove
            backgroundColor: 'rgba(255, 5, 5, 0.2)', // Boja ispune barova
            borderColor: 'rgba(175, 100, 192, 1)', // Boja linija oko barova
          },
        ],
      },
      options: {
        plugins:{
          title:{
            display:true,
            text:'Good and bad posture ratio per day'
          }
        },
        scales: {
          y: {
            beginAtZero: true,

          },
        },
      },
    });
  }

  createChart3(){
    if(this.chart3){
      this.chart3.destroy();
    }
    this.chart3 = new Chart('dan', {
      type: 'line',
      data: {
        labels: this.intervali,
        datasets: [
          {
            label: 'Sitting minutes per hour',
            data: this.sjedenja,
            type: 'line', // Ovo postavlja tip na bar za glavni set podataka
            borderWidth: 2,
            backgroundColor: 'rgba(75, 192, 192, 0.2)', // Boja ispune barova
            borderColor: 'rgba(75, 192, 192, 1)', // Boja linija oko barova
            fill:true,
          },
        ],
      },
      options: {
        plugins:{
          title:{
            display:true,
            text:'Distribution of sitting per day'
          }
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks:{
              stepSize:1
            }
          },
        },
        elements:{
          line:{
            tension:0.5
          },
        }
      },
    });
  }

  createChart4(){
    if(this.chart4){
      this.chart4.destroy();
    }
    this.chart4 = new Chart('wrongposture', {
      type: 'line',
      data: {
        labels: this.wrongPostureDani,
        datasets: [
          {
            label: 'Good sitting posture percentage (%)',
            data: this.wrongPosturePercentage,
            type: 'line', // Ovo postavlja tip na bar za glavni set podataka
            borderWidth: 2,
            backgroundColor: 'rgba(75, 192, 192, 0.2)', // Boja ispune barova
            borderColor: 'rgba(75, 192, 192, 1)', // Boja linija oko barova
            fill:true,
          },
        ],
      },
      options: {
        plugins:{
          // title:{
          //   display:true,
          //   text:''
          // }
        },
        scales: {
          y: {
            beginAtZero: true,
            ticks:{
              stepSize:5
            },
            min:0,
            max:100
          },
        },
      },
    });
  }
  pieChart:any;
  createChart5(){
    if(this.chart5){
      this.chart5.destroy();
    }
    this.chart5 = new Chart('piechart', {
      type: 'doughnut',
      data:this.pieChart,
      options: {
        responsive: true,
        plugins: {
          legend: {
            position: 'top',
            reverse:true,
          },
          title: {
            display: true,
            text: 'Chart.js Doughnut Chart'
          }
        },
      },
    });
  }

  dnevniGraf:GrafDan[]=[];
  intervali:string[]=[];
  sjedenja:number[]=[];
  public GetDayDistribution(){
    this.dataService.GetGraphDay(this.heatMapDate).subscribe(x=>{
      this.dnevniGraf=x;
      this.intervali=[];
      this.sjedenja=[];

      this.intervali.push(this.dnevniGraf[0].start);
      this.intervali.push(...this.dnevniGraf.map(obj => obj.end))

      this.sjedenja.push(0);
      this.sjedenja.push(...this.dnevniGraf.map(obj=>obj.sittingMinutes));
      this.createChart3();
      this.grafDani=true;
    }, error=>{
      this.grafDani=false;
    })
  }
  grafDani=true;

  lastDays:LastDaysResponse[]=[];
  minutes:number[]=[];
  days:string[]=[];
  private getLastDays() {
  this.dataService.GetLastSpecificDays(5).subscribe(x=>{
    this.lastDays=x;

    this.days=this.lastDays.map(item=>item.date.split('T')[0]);
    this.minutes=this.lastDays.map(item=>item.totalMinutes);
    //console.log(this.lastDays);
    //console.log(this.days);
    //console.log(this.minutes);
    this.createChart1();

  })
  }

  wrongPosture:WrongPosture[]=[];
  wrongPostureDani:string[]=[];
  wrongPosturePercentage:number[]=[];
  public getPostureGraph(){
    return this.dataService.GetPostureGraph(5).subscribe(x=>{
      this.wrongPosture=x;
      this.wrongPosturePercentage=this.wrongPosture.map(x=>x.good);
      this.wrongPostureDani=this.wrongPosture.map((x=>x.datumString));
      this.createChart4();
    })
  }


  ratios:GoodBadRatioResponse[]=[];
  daysratio:string[]=[];
  goodpercentages:number[]=[];
  badpercentages:number[]=[];

  private getGoodBadRatio(){
    this.dataService.GetGoodBadRatioData(5).subscribe(x=>{
      this.ratios=x;

      this.daysratio=this.ratios.map(item=>item.date.split('T')[0]);
      this.goodpercentages=this.ratios.map(item=>item.ratioGood);
      this.badpercentages=this.ratios.map(item=>item.ratioBad);
      //this.createChart2();
    })
  }

  warningobj:WarningInfo={
    badCount:0,
    badCount5:0,
    badGoodRatio:0,
    badGoodRatio5:0,
    goodBadRatio:0,
    goodBadRatio5:0,
    goodCount:0,
    goodCount5:0,
  }
  warningShow=false;

  procenatLast5=0;


  private getWarning(){
    //this.getAiReponse();
    this.dataService.GetWarning().subscribe(x=>{
    this.warningobj=x;
    if(this.warningobj.badGoodRatio5>70){
      this.procenatLast5=this.warningobj.badGoodRatio5;
      this.warningShow=true;
      this.activateWarning();
    }
    else{
      this.procenatLast5=0;
      this.warningShow=false;

      this.deactivateWarning();
    }
    })
  }
  heatMapDate:any;
  chairSittingData:Date=new Date(Date.now());
  heatMapObject:any;
  ChairHeatmap(){
    this.dataService.GetHeatmapData(this.heatMapDate).subscribe(x=>{
      //alert(x.chairId+" "+x.brojZapisa+" "+x.s1Percentage+" "+x.s2Percentage+" "+x.s3Percentage);
      this.heatMapObject=x;
    })
  }

  sjedenje:ChairSittingData={
    s1Percentage:0,
    s2Percentage:0,
    s3Percentage:0,
    badPercentage:0,
    goodPercentage:0,
    sittingHours:0,
    sittingMinutes:0,
    datum:new Date()
  }

  ChairSittingData(){
    console.log("Dobro je proslo");
    this.dataService.GetSittingData(this.heatMapDate).subscribe(x=>{
      //alert(x.badPercentage+" "+x.goodPercentage+" "+x.s1Percentage+" "+x.s2Percentage+" "+x.s3Percentage);
      this.sjedenje=x;
      this.drugi.nativeElement.style.backgroundColor=this.getRedColor(175, 255, x.s1Percentage);
      this.treci.nativeElement.style.backgroundColor=this.getRedColor(175, 255, x.s2Percentage);
      this.cetvrti.nativeElement.style.backgroundColor=this.getRedColor(175, 255, x.s3Percentage);
      this.prvi.nativeElement.style.backgroundColor=this.getRedColor(175, 255, x.s3Percentage);

      this.pieChart={
        labels:["Good %", "Bad %"],
        datasets:[{
          label:'Percentage',
          data:[this.sjedenje.goodPercentage, this.sjedenje.badPercentage],
          backgroundColor: ['#40ff00', '#ff0000'], // Boje za segmente
          hoverBackgroundColor: ['#59ff47', '#ff0000'], // Boje za segmente na hover
        }]
      }
      this.createChart5();
    })
  }
  getRedColor(min=175, max=255, procenat=0){
    var rgbRacun=min+(max-min)*procenat/100;
    return `rgb(${rgbRacun}, 0, 0)`;
  }

counter=0;
  isred=false;
  iswhite=false;
  async activateWarning(){

    await this.executeWithDelay(1000, true);
    await this.executeWithDelay(1000, false);
    await this.executeWithDelay(1000, true);
    await this.executeWithDelay(1000, false);
    await this.executeWithDelay(1000, true);
    await this.executeWithDelay(1000, false);
    this.isred=false;
    this.iswhite=true;
    this.warningShow=false;
  }
  deactivateWarning(){

  }

  executeWithDelay(delay:number, isr:boolean,):Promise<void>{
    return new Promise(resolve => {
      setTimeout(()=>{
        this.isred=isr;
        //console.log(this.isred);
        this.iswhite=!isr;
        resolve();
      }, delay);
    })
  }

  aiResponse:string="";
  obj:AiResponse={
    message:""
};
  private getAiReponse(){
    this.dataService.GetOpenAiData().subscribe(x=>{
      //console.log(x);
      this.obj=x;
    this.aiResponse=this.obj.message;
    //console.log(this.aiResponse);
    })
  }

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
