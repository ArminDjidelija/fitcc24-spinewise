import { Component } from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {MyAuthService} from "../Auth/Auth service/my-auth-service";
import {FormsModule} from "@angular/forms";
import {AuthLoginRequest} from "../Auth/Login/auth-login-request";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(private readonly myAuth:MyAuthService, private readonly router:Router) {
  }

  email:string="";
  password:string="";

  signIn(){
    var request:AuthLoginRequest={
      email:this.email,
      password:this.password
    };

    this.myAuth.signIn(request).subscribe(async x=>{
      if(x.role==="user"){
        this.router.navigate(["/user"]);
      }
      else if(x.role==="admin"){
        this.router.navigate(["/s-admin"]);
      }
    })
  }

  onEnter($event: any) {
   this.signIn();
  }

}
