import {Component, OnInit} from '@angular/core';
import {Router, RouterLink, RouterOutlet} from "@angular/router";
import {AuthGet, GetRole} from "../Auth/Get/auth-get";
import {GetAuth} from "../Auth/AuthenticationCheck/get-auth";
import _default from "chart.js/dist/core/core.interaction";
import x = _default.modes.x;
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [
    RouterLink,
    RouterOutlet,
    NgIf
  ],
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.css'
})
export class HomepageComponent implements OnInit{
  constructor(private getAuth:AuthGet, private router:Router) {
  }
  ngOnInit(): void {
    this.getRole();
  }
  role:any;
  getRole(){
    this.getAuth.getRole().subscribe(x=>{
      this.role=x;

    })
  }

  forward(){
    if(this.role.role==="user"){
      this.router.navigate(["/user"]);
    }
    else if(this.role.role==="superadmin"){
      this.router.navigate(["/s-admin"]);
    }
    else{
      this.router.navigate(["/login"]);
    }
  }



}
