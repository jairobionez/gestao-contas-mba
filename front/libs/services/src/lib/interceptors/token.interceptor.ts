import { HttpErrorResponse, HttpInterceptorFn, HttpRequest } from "@angular/common/http";
import { inject } from "@angular/core";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { AuthService } from "../http/auth.service";
import { ErrorHandler } from "./error-handler";

export const TokenInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const handle = inject(ErrorHandler);

  let authReq = req;

  const token = authService.getToken();

  if (token) {
    authReq = addTokenHeader(req, token.accessToken);
  }

  return next(authReq).pipe(catchError(error => {
    const erros = ['']; // TODO

    switch (error?.status) {
      case 400:
        handle.handleError(erros);
        break;
      case 401:
        authService.logout();
        break;
      case 500:
        handle.handleError(['Erro interno, por favor contate o administrador.']);
        break;
      case 0:
        handle.handleError(['Falha ao se comunicar com o servidor']);
        break;
    }
    return throwError(() => error);
  }));

  function addTokenHeader(request: HttpRequest<any>, token: string) {
    return request.clone({ headers: request.headers.set('Authorization', `bearer ${token}`) });
  }
};
