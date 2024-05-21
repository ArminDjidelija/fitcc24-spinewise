import {Component, OnInit} from '@angular/core';
import {RouterLink, RouterOutlet} from "@angular/router";
import {MyAuthService} from "../../Auth/Auth service/my-auth-service";
declare function init_plugin():any;

@Component({
  selector: 'app-super-admin-panel',
  standalone: true,
    imports: [
      RouterOutlet,
        RouterLink
    ],
  templateUrl: './super-admin-panel.component.html',
  styleUrl: './super-admin-panel.component.css'
})
export class SuperAdminPanelComponent implements OnInit{
  constructor(private myAuth:MyAuthService) {
  }
  ngOnInit(): void {
    init_plugin();
  }

  signOut() {
    this.myAuth.signOut();
  }
}
