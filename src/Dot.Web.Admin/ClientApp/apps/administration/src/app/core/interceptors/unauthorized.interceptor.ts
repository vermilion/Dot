import { HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, throwError } from "rxjs";

import { AuthenticationService } from "../services/auth.service";
import { Injectable } from "@angular/core";
import { catchError } from "rxjs/operators";

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
    return next.handle(request).pipe(
      catchError((err) => {
        if ([401, 403].includes(err.status)) {
          // auto logout if 401 or 403 response returned from api
          //this.authenticationService.logout();
          //return throwError(() => err);
        }

        console.error(err);

        this.authenticationService.redirectToLogin();

        // handle your auth error or rethrow
        return throwError(() => err);
      })
    );
  }
}
