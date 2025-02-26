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
    return this.httpCliente.get<CategoriaResponseModel[]>(`${environment.apiBase}/categorias`);
  }

  getAtivos(): Observable<CategoriaResponseModel[]> {
    return this.httpCliente.get<CategoriaResponseModel[]>(`${environment.apiBase}/categorias`);
  }

  getById(categoriaId: any): Observable<CategoriaResponseModel> {
    return this.httpCliente.get<CategoriaResponseModel>(`${environment.apiBase}/categorias/${categoriaId}`);
  }

  post(categoria: CategoriaModel): Observable<CategoriaResponseModel> {
    return this.httpCliente.post<CategoriaResponseModel>(`${environment.apiBase}/categorias`, categoria);
  }

  put(categoriaId: any, categoria: CategoriaModel): Observable<CategoriaResponseModel> {
    return this.httpCliente.put<CategoriaResponseModel>(`${environment.apiBase}/categorias/${categoriaId}`, categoria);
  }

  delete(categoriaId: any): Observable<CategoriaResponseModel> {
    return this.httpCliente.delete<CategoriaResponseModel>(`${environment.apiBase}/categorias/${categoriaId}`);
  }
}
