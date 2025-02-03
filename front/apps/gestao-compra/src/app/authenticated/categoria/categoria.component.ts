import { Component, OnInit } from '@angular/core';
import { HeaderService } from '@front/services';

@Component({
  selector: 'app-categoria',
  template: '<router-outlet></router-outlet>',
  standalone: false
})

export class CategoriaComponent {

  constructor(
    private headerService: HeaderService
  ) {
    this.headerService.alterarTitulo('Categorias');
  }
}
