import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderService } from '@front/services';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'gestao-compra-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  titulo?: string = "Dashboard";
  usuario?: any = {nome: "Admin", email: "admin@gmail.com"};
  destroy$ = new Subject<void>;

  constructor(
    private router: Router,
    private headerService: HeaderService
  ) {
    this.headerService.obterNovoTitulo()
      .pipe(takeUntil(this.destroy$))
      .subscribe(titulo => this.titulo = titulo);
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.destroy$.next();
  }
}
