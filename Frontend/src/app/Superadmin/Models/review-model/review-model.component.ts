import {Component, OnInit} from '@angular/core';
import {DatePipe, NgForOf} from "@angular/common";
import {ChairmodelDataservice} from "./chairmodel-dataservice";

@Component({
  selector: 'app-review-model',
  standalone: true,
    imports: [
        DatePipe,
        NgForOf
    ],
  templateUrl: './review-model.component.html',
  styleUrl: './review-model.component.css'
})
export class ReviewModelComponent implements OnInit{
  constructor(private dataService:ChairmodelDataservice) {
  }
  ngOnInit(): void {
    this.loadModels();
  }
  stolice:any;
  loadModels(){
    this.dataService.getAllChairModels().subscribe(x=>{
      this.stolice=x;
    })
  }

  deleteChair(id:any) {
    var result=confirm("Do you want to delete chair?");
  if(result){
    this.dataService.deleteChairModel(id).subscribe(x=>{
      this.loadModels();
    })
  }

  }
}
