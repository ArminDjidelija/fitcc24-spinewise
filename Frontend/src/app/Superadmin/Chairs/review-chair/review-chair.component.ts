import {Component, OnInit} from '@angular/core';
import {GetChairDataservice} from "./get-chair-dataservice";
import {DatePipe, NgForOf} from "@angular/common";

@Component({
  selector: 'app-review-chair',
  standalone: true,
  imports: [
    DatePipe,
    NgForOf
  ],
  templateUrl: './review-chair.component.html',
  styleUrl: './review-chair.component.css'
})
export class ReviewChairComponent implements OnInit{
constructor(private dataService:GetChairDataservice) {
}

  chairs:any=[];
  ngOnInit(): void {
  this.getAllChairs();
  }

  getAllChairs(){
    this.dataService.GetAll().subscribe(x=>{
      this.chairs=x;
    })
  }

  deleteChair(id:any) {
    var result=confirm("Do you want to delete chair?");
    if(result){
      this.dataService.DeleteChair(id).subscribe(x=>{

      })
    }
  }
}
