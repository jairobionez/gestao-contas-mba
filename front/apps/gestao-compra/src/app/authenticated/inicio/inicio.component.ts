import { Component, OnInit } from '@angular/core';
import { HeaderService } from '@front/services';

@Component({
  selector: 'app-inicio',
  template: '<router-outlet></router-outlet>',
  standalone: false
})

export class InicioComponent {

  constructor(
    private headerService: HeaderService
  ) {
    this.headerService.alterarTitulo('Início');
  }
}
