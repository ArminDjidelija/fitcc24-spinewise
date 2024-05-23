import { Routes } from '@angular/router';
import {authGuardAdmin, authGuardSuperAdmin, authGuardUser} from "./Auth/Guards/auth-guard";
import {LoginComponent} from "./login/login.component";
import {UserPanelComponent} from "./User/user-panel/user-panel.component";
import {AdminPanelComponent} from "./admin-panel/admin-panel.component";
import {SuperAdminPanelComponent} from "./Superadmin/super-admin-panel/super-admin-panel.component";
import {UserPanelContentComponent} from "./User/user-panel-content/user-panel-content.component";
import {UserChairsComponent} from "./User/user-chairs/user-chairs.component";
import {UserProfileComponent} from "./User/user-profile/user-profile.component";
import {HomepageComponent} from "./homepage/homepage.component";
import {RegisterComponent} from "./register/register.component";
import {SuperAdminProfileComponent} from "./Superadmin/super-admin-profile/super-admin-profile.component";
import {SuperAdminDashboardComponent} from "./Superadmin/super-admin-dashboard/super-admin-dashboard.component";
import {AddChairComponent} from "./Superadmin/Chairs/add-chair/add-chair.component";
import {ReviewChairComponent} from "./Superadmin/Chairs/review-chair/review-chair.component";
import {AddModelComponent} from "./Superadmin/Models/add-model/add-model.component";
import {ReviewModelComponent} from "./Superadmin/Models/review-model/review-model.component";
import {UserWelcomeComponent} from "./User/user-welcome/user-welcome.component";

export const routes: Routes = [
  {path:'', component:HomepageComponent},
  {path:'login', component:LoginComponent},
  {path:'register', component:RegisterComponent},
  {path:'user', component:UserPanelComponent, canActivate:[authGuardUser], children:[
      {path:'dashboard', component:UserPanelContentComponent},
      {path:'chairs', component:UserChairsComponent},
      {path:'profile', component:UserProfileComponent},
      {path:'panel', component:UserWelcomeComponent},
    ]},
  {path:'user', redirectTo:'user/dashboard'},
  {path:'admin-panel', component:AdminPanelComponent, canActivate:[authGuardAdmin]},
  {path:'s-admin', component:SuperAdminPanelComponent, canActivate:[authGuardSuperAdmin], children:[
      {path: 'add-chair', component: AddChairComponent},
      {path: 'review-chair', component: ReviewChairComponent},
      {path: 'profile', component: SuperAdminProfileComponent},
      {path: 'dashboard', component: SuperAdminDashboardComponent},
      {path: 'add-model', component: AddModelComponent},
      {path: 'review-model', component: ReviewModelComponent},
    ]},
];
