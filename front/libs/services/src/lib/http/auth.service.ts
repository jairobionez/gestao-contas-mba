import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { environment } from '../configuration';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  authenticated$ = new Subject<void>();
  apiBase: string;

  constructor(
    private http: HttpClient,
    private router: Router
  ) {
    this.apiBase = `${environment.apiBase}/auth`;
  }

  public logout(): void {
    localStorage.removeItem('token');
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  public isLoggedIn() {
    return this.getUsuarioToken();
  }

  public getUserRole(): string {
    const user = this.getUsuarioToken();

    if (user)
      return user.claims.find((p: any) => p.type === 'role')!.value;

    return '';
  }

  public setToken(token: string): void {
    localStorage.setItem('token', token);
  }

  public getUsuarioToken(): any {
    return JSON.parse(localStorage.getItem('token')!)?.usuarioToken as any;
  }

  public getToken(): any {
    return JSON.parse(localStorage.getItem('token')!) as any;
  }

  public getEmail(): string {
    const user = this.getUsuarioToken();

    if (user)
      return this.getUsuarioToken().email;

    return '';
  }

  public getNome(): string {
    const user = this.getUsuarioToken();

    if (user)
      return this.getUsuarioToken().nome;

    return '';
  }

  public setUrl(url: string) {
    try {
      localStorage.setItem('url', url);
    } catch (ex) {
      console.log(ex);
    }
  }

  public getUrl(): string {
    const url = localStorage.getItem('url');
    return url ? url.toString() : '';
  }

  getPerfil(): string {
    const url = localStorage.getItem('perfil');
    return url ? url.toString() : '';
  }

  public getId(): string {
    const user = this.getUsuarioToken();

    if (user)
      return this.getUsuarioToken().id;

    return '';
  }

  public setPerfil(perfil: string): void {
    localStorage.setItem('perfil', perfil);
  }
}
