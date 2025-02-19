import { Component } from '@angular/core';
import { HeaderService } from '@front/services';

@Component({
  selector: 'app-orcamento',
  template: '<router-outlet></router-outlet>',
  standalone: false
})

export class OrcamentoComponent {

  constructor(
    private headerService: HeaderService
  ) {
    this.headerService.alterarTitulo('Orçamentos');
  }

}
