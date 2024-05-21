import {Component, OnInit} from '@angular/core';
import {AddChairDataservice} from "./add-chair-dataservice";
import {NgForOf, NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-add-chair',
  standalone: true,
  imports: [
    NgForOf,
    FormsModule,
    NgIf
  ],
  templateUrl: './add-chair.component.html',
  styleUrl: './add-chair.component.css'
})
export class AddChairComponent implements OnInit{
  constructor(private dataService:AddChairDataservice) {
  }
  ngOnInit(): void {
    this.loadModels();
  }

  models:any=[];
  chairModelId=0;
  private loadModels() {
    this.dataService.getAllModels().subscribe(x=>{
      this.models=x;
    })
  }

  newChair:any;
  newChairId=0;
  newChairSerialNumber="";
  addNewChair(){
    this.dataService.postChair(this.chairModelId).subscribe(x=>{
      this.newChair=x;
      this.newChairId=this.newChair.id;
      this.newChairSerialNumber=this.newChair.serialNumber;
    })
  }
}
