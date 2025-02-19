import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { environment } from '../configuration';
import { OrcamentoModel, OrcamentoResponseModel } from '@front/data';

@Injectable()
export class OrcamentoService {

  constructor(
    private httpCliente: HttpClient
  ) { }


  get(): Observable<OrcamentoResponseModel[]> {
    return this.httpCliente.get<OrcamentoResponseModel[]>(`${environment.apiBase}/orcamentos`);
  }

  getById(categoriaId: any): Observable<OrcamentoResponseModel> {
    return this.httpCliente.get<OrcamentoResponseModel>(`${environment.apiBase}/orcamentos/${categoriaId}`);
  }

  post(categoria: OrcamentoModel): Observable<OrcamentoResponseModel> {
    return this.httpCliente.post<OrcamentoResponseModel>(`${environment.apiBase}/orcamentos`, categoria);
  }

  put(categoriaId: any, categoria: OrcamentoModel): Observable<OrcamentoResponseModel> {
    return this.httpCliente.put<OrcamentoResponseModel>(`${environment.apiBase}/orcamentos/${categoriaId}`, categoria);
  }

  delete(categoriaId: any): Observable<OrcamentoResponseModel> {
    return this.httpCliente.delete<OrcamentoResponseModel>(`${environment.apiBase}/orcamentos/${categoriaId}`);
  }
}
