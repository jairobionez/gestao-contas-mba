import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'gestao-compra-menu',
  standalone: false,
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})

export class MenuComponent {


  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {

  }

  ativo(menu: string): boolean {
    if (this.router.url.indexOf('inicio') >= 0 && menu === 'inicio')
      return true;
    else if (this.router.url.indexOf('contas') >= 0 && menu === 'contas')
      return true;
    else if (this.router.url.indexOf('categoria') >= 0 && menu === 'categoria')
      return true;
    else if (this.router.url.indexOf('transacoes') >= 0 && menu === 'transacoes')
      return true;
    else if (this.router.url.indexOf('orcamentos') >= 0 && menu === 'orcamentos')
      return true;


    return false;
  }
}
