import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';
import { CategoriaModel, CategoriaResponseModel } from '@front/data';
import { environment } from '../configuration';

@Injectable()
export class CategoriaService {

  constructor(
    private httpCliente: HttpClient
  ) { }


  get(): Observable<CategoriaResponseModel[]> {
    return this.httpCliente.get<CategoriaResponseModel[]>(`${environment.apiBase}/categoria`);
  }

  getById(categoriaId: any): Observable<CategoriaResponseModel> {
    return this.httpCliente.get<CategoriaResponseModel>(`${environment.apiBase}/categoria/${categoriaId}`);
  }

  post(categoria: CategoriaModel): Observable<CategoriaResponseModel> {
    return this.httpCliente.post<CategoriaResponseModel>(`${environment.apiBase}/categoria`, categoria);
  }

  put(categoriaId: any, categoria: CategoriaModel): Observable<CategoriaResponseModel> {
    return this.httpCliente.put<CategoriaResponseModel>(`${environment.apiBase}/categoria/${categoriaId}`, categoria);
  }

  delete(categoriaId: any): Observable<CategoriaResponseModel> {
    return this.httpCliente.delete<CategoriaResponseModel>(`${environment.apiBase}/categoria/${categoriaId}`);
  }
}
