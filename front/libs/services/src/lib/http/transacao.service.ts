import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { TransacaoModel, TransacaoResponseModel } from '@front/data';
import { environment } from '../configuration';

@Injectable()
export class TransacaoService {

  constructor(
    private httpCliente: HttpClient
  ) { }


  get(): Observable<TransacaoResponseModel[]> {
    return this.httpCliente.get<TransacaoResponseModel[]>(`${environment.apiBase}/transacoes`);
  }

  getById(transacoId: any): Observable<TransacaoResponseModel> {
    return this.httpCliente.get<TransacaoResponseModel>(`${environment.apiBase}/transacoes/${transacoId}`);
  }

  post(transacao: TransacaoModel): Observable<TransacaoResponseModel> {
    return this.httpCliente.post<TransacaoResponseModel>(`${environment.apiBase}/transacoes`, {'transacaoModel': transacao });
  }

  put(transacoId: any, transacao: TransacaoModel): Observable<TransacaoResponseModel> {
    return this.httpCliente.put<TransacaoResponseModel>(`${environment.apiBase}/transacoes/${transacoId}`, transacao);
  }

  delete(transacoId: any): Observable<TransacaoResponseModel> {
    return this.httpCliente.delete<TransacaoResponseModel>(`${environment.apiBase}/transacoes/${transacoId}`);
  }
}
