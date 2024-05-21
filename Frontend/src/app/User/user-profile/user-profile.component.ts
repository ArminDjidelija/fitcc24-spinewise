import {Component, OnInit} from '@angular/core';
import {UserGet, UserProfileDataservice, UserUpdate} from "./user-profile-dataservice";
import {FormsModule} from "@angular/forms";
import {NgIf} from "@angular/common";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.css'
})
export class UserProfileComponent implements OnInit{
  constructor(private dataService:UserProfileDataservice) {
  }

  ngOnInit(): void {
    this.getDetailInfo();
  }
  user:UserGet={
    firstName:"",
    lastName:""
  }
  userUpdate:UserUpdate={
    firstname:"",
    lastname:"",
    newPassword:"",
    newPasswordConfirm:"",
    password:""
}
  firstname="";
  lastname="";

  pass="";
  newPass="";
  confPass="";
  promjenaLozinke=false;

  getDetailInfo(){
    this.dataService.getDetails().subscribe(x=>{
      this.user=x;
      this.firstname=this.user.firstName;
      this.lastname=this.user.lastName;
      }
    )
  }

  updateUser() {
    this.userUpdate.firstname=this.firstname;
    this.userUpdate.lastname=this.lastname;
    this.userUpdate.password=this.pass;
    this.userUpdate.newPassword=this.newPass;
    this.userUpdate.newPasswordConfirm=this.confPass;

    this.dataService.updateUser(this.userUpdate).subscribe(x=>{
      this.promjenaLozinke=false;
        this.getDetailInfo();
        alert("Uspješno izmijenjen profil!");
    }, error=>{
      alert(JSON.stringify(error.error));
      window.location.reload();
    })
  }
}
