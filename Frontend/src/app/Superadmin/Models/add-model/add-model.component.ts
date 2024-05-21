import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {ChairmodelDataservice} from "./chairmodel-dataservice";

@Component({
  selector: 'app-add-model',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './add-model.component.html',
  styleUrl: './add-model.component.css'
})
export class AddModelComponent {
  constructor(private dataService:ChairmodelDataservice) {
  }
name="";
  dateOfCreating: any;

  addNewChair() {
    this.dataService.postChairModel(this.name, this.dateOfCreating).subscribe(x=>{
      alert("Successfully added new chair model");
    })
  }
}
