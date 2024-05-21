import {Component, OnInit} from '@angular/core';
import {RouterLink, RouterOutlet} from "@angular/router";
import {MyAuthService} from "../../Auth/Auth service/my-auth-service";
declare function init_plugin():any;

@Component({
  selector: 'app-user-panel',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink
  ],
  templateUrl: './user-panel.component.html',
  styleUrl: './user-panel.component.css'
})
export class UserPanelComponent implements OnInit{
constructor(private myAuth:MyAuthService) {
}
  ngOnInit(): void {
    init_plugin();
    //console.log("sve ok");
  }
  signOut() {
    this.myAuth.signOut();
  }
}
