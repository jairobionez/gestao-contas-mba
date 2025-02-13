import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../configuration";
import { Observable } from "rxjs";
import { UsuarioModel } from "@front/data";

@Injectable()
export class UsuarioService {

  constructor(
    private httpCliente: HttpClient
  ) { }

  post(usuario: UsuarioModel): Observable<any> {
    return this.httpCliente.post<any>(`${environment.apiBase}/usuarios`, usuario);
  }
}
