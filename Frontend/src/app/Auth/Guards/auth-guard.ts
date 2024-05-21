import {inject, Injectable} from "@angular/core";
import {CanActivateFn, Router} from "@angular/router";
import {GetAuth} from "../AuthenticationCheck/get-auth";

@Injectable({providedIn:'root'})
export class PermissionsService{
  constructor(private router:Router, private getAuth:GetAuth) {
  }

  async canActivateSuperAdmin(): Promise<boolean> { //, route:ActivatedRouteSnapshot,state:RouterStateSnapshot
    //console.log(this.getAuth.getAuthorizationToken());
    const isAdmin=await this.getAuth.IsSuperAdmin();
    if(isAdmin){
      return true;
    }
    else{
      this.router.navigate(['/login']);
      return false;
    }
    return false;

  }
  async canActivateAdmin(): Promise<boolean> { //, route:ActivatedRouteSnapshot,state:RouterStateSnapshot
   // console.log(this.getAuth.getAuthorizationToken());
    const isAdmin=await this.getAuth.IsAdmin();
    if(isAdmin){
      return true;
    }
    else{
      this.router.navigate(['/login']);
      return false;
    }
    return false;

  }
  async canActivateUser(): Promise<boolean> { //, route:ActivatedRouteSnapshot,state:RouterStateSnapshot
   // console.log(this.getAuth.getAuthorizationToken());
    const isAdmin=await this.getAuth.IsUser();
    if(isAdmin){
      return true;
    }
    else{
      this.router.navigate(['/login']);
      return false;
    }
    return false;

  }
}
export const authGuardSuperAdmin: CanActivateFn = (route, state) => {
  //console.log("Can activate check");
  return inject(PermissionsService).canActivateSuperAdmin(); //, inject(route), inject(state)
};
export const authGuardAdmin: CanActivateFn = (route, state) => {
  //console.log("Can activate check");
  return inject(PermissionsService).canActivateAdmin(); //, inject(route), inject(state)
};
export const authGuardUser: CanActivateFn = (route, state) => {
  //console.log("Can activate check");
  return inject(PermissionsService).canActivateUser(); //, inject(route), inject(state)
};
