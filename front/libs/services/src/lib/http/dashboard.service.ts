import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { TransacaoFiltroModel, TransacaoModel, TransacaoResponseModel } from '@front/data';
import { environment } from '../configuration';

@Injectable()
export class DashboardService {

constructor(
    private httpCliente: HttpClient
) { }

    //   getByFilters(busca: TransacaoFiltroModel): Observable<TransacaoResponseModel[]> {
    //     let dataInicial = null;
    //     let dataFinal = null;
    //     console.log();
    //     if (busca.dataInicial != null) {
    //       dataInicial = dateCustomFormatting(busca.dataInicial); //`${busca.dataInicial.getFullYear()}-${busca.dataInicial.getMonth()}-${busca.dataInicial.getDay()}`;    
    //     }
    //     if (busca.dataFinal != null) {
    //       dataFinal = dateCustomFormatting(busca.dataFinal); //`${busca.dataFinal.getFullYear()}-${busca.dataFinal.getMonth()}-${busca.dataFinal.getDay()}`;    
    //     }    
    //     return this.httpCliente.get<TransacaoResponseModel[]>(`${environment.apiBase}/transacoes/Busca/?dataInicial=${dataInicial}&dataFinal=${dataFinal}&categoriaId=${busca.categoriaId}&tipo=${busca.tipo}`);
    //   }

    indicadores(filtro: {dataInicial: Date, dataFinal: Date}): Observable<any> {
        return this.httpCliente.post<any>(`${environment.apiBase}/Dashboard/indicadores`, filtro);
    }
}

function dateCustomFormatting(date: Date): string {
  const padStart = (value: number): string =>
      value.toString().padStart(2, '0');
  return `${padStart(date.getFullYear())}-
          ${padStart(date.getMonth() + 1)}-
      ${date.getDate()}`;
}
