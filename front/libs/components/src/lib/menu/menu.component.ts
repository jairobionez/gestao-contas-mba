import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'gestao-compra-menu',
  standalone: false,
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})

export class MenuComponent {


  constructor(
    private router: Router,
  ) { 

  }

  ativo(menu: string): boolean {
    if (this.router.url.indexOf('dashboard') >= 0 && menu === 'dashboard')
      return true;
    else if (this.router.url.indexOf('contas') >= 0 && menu === 'contas')
      return true;

    return false;
  }
}
