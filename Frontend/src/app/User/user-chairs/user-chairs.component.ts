import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Chair, ChairUpdate, DataServiceChairs} from "./data-service-chairs";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {animationFrameScheduler} from "rxjs";

@Component({
  selector: 'app-user-chairs',
  standalone: true,
  imports: [
    NgForOf,
    DatePipe,
    NgIf,
    FormsModule
  ],
  templateUrl: './user-chairs.component.html',
  styleUrl: './user-chairs.component.css'
})
export class UserChairsComponent implements OnInit{
  constructor(private dataService:DataServiceChairs) {
  }
  ngOnInit(): void {
    this.loadChairs();
    this.stolica={
      id:0,
      serialNumber:"",
      dateOfCreating:"",
      chairModelName:"",
      delay:30,
      naziv:"",
      saljiPodatke:true
    };
  }
  stolica:Chair={
    id:2,
    serialNumber:"",
    dateOfCreating:"",
    chairModelName:"",
    delay:30,
    naziv:"",
    saljiPodatke:true
  };
  serialNumber="";
  loadChairs(){
    this.dataService.GetChairs().subscribe(x=>{
      this.stolica=x;
      //console.log(this.stolica);
    })
  }

  setUserChair(){
    this.dataService.SetUserChair(this.serialNumber).subscribe(x=>{
      this.loadChairs();
    })
  }

  editovanje=false;
  editovanaStolica:ChairUpdate={
    naziv:"",
    delay:0,
    saljiPodatke:true,
    id:0
  }
  setEditovanje() {
    this.editovanje=true;
    this.editovanaStolica=this.stolica;
  }

  protected readonly animationFrameScheduler = animationFrameScheduler;

  editujStolicu() {
    this.dataService.updateChair(this.editovanaStolica).subscribe(x=>{
    }, error => {
      alert(JSON.stringify(error.error));
    })
    this.loadChairs();
    this.editovanje=false;
  }
}
