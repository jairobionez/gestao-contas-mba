import { Component, OnInit } from '@angular/core';
import { HeaderService } from '@front/services';

@Component({
  selector: 'app-transacao',
  template: '<router-outlet></router-outlet>',
  standalone: false
})
export class TransacaoComponent {

  constructor(
    private headerService: HeaderService
  ) {
    this.headerService.alterarTitulo('Transações');

  }
}
