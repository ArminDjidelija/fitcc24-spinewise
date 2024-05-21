import {Inject, Injectable} from "@angular/core";
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Observable, tap} from "rxjs";
import {Router} from "@angular/router";
import {MyAuthService} from "../Auth service/my-auth-service";

@Injectable({providedIn:'root'})
export class MyAuthInterceptor implements HttpInterceptor{

  constructor(
    private auth: MyAuthService,
    private router: Router) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //const myAuth=inject(MyAuthService);
    //console.log("intercept");
    // Get the auth token from the service.
    const authToken = this.auth.getAuthorizationToken()??"";
    //console.log(authToken);
    // Clone the request and replace the original headers with
    // cloned headers, updated with the authorization.
    const authReq = req.clone({
      headers: req.headers.set('auth-token', authToken)
    });

    // send cloned request with header to the next handler.
    return next.handle(authReq).pipe(
      tap(()=>{}, err=>{
        if (err instanceof HttpErrorResponse)
        {
          if (err.status !== 401){
            return;
          }
          this.router.navigateByUrl('/login');
        }
      })
    );  }

}
