import { Component } from '@angular/core';
import {RouterLink, RouterLinkActive, RouterModule, RouterOutlet} from '@angular/router';
import {HttpClientModule} from "@angular/common/http";
import {AuthLoginEndpoint} from "./Auth/Login/auth-login-endpoint";
import {AuthLogoutEndpoint} from "./Auth/Logout/auth-logout-endpoint";
import {PermissionsService} from "./Auth/Guards/auth-guard";
//import { HashLocationStrategy, LocationStrategy } from '@angular/common';
//import { routes } from './app.routes';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterLink,
    HttpClientModule,
    RouterLinkActive,
  ],
  providers:[
    AuthLoginEndpoint,
    AuthLogoutEndpoint,
    PermissionsService,
    //{provide:LocationStrategy, useClass:HashLocationStrategy},
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'SpineWise';
}
