import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(err => {
        if (err) {
          switch (err.status) {
            case 401:
            case 403:
              this.toastr.error(
                err.statusText === "OK" ? "Unauthorized" : err.statusText, err.status
              );
              break;
            case 400:
              if (err.error.errors) {
                const modalStateErrors = [];
                for (const key in err.error.errors) {
                  if (err.error.errors[key]) {
                    modalStateErrors.push(err.error.errors[key]);
                  }
                }
                throw modalStateErrors.flat();
              } else {
                this.toastr.error(
                  err.statusText === "OK" ? "Bad Request" : err.statusText, err.status
                );
              }
              break;
          }
        }
        const error = err.error || err.statusText;
        console.error(err.error);
        return throwError(() => error);
      })
    )
  }
}
