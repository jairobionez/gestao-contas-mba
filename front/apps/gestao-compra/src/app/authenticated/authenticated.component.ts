import { Component, OnInit } from "@angular/core";

@Component({
    selector: 'gestao-compra-authenticated',
    standalone: false,
    template: `
      <!-- <gestao-compra-menu [grupos]="menu"></gestao-compra-menu> -->
      <router-outlet></router-outlet>
    `,
})

export class AuthenticatedComponent implements OnInit {
    // menu: MenuModel[] = grupos;
    constructor() {}
  
    ngOnInit() {}
}