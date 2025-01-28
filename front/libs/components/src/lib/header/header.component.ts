import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'gestao-compra-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  titulo?: string = "Dashboard";
  usuario?: any = {nome: "Admin", email: "admin@gmail.com"};

  constructor(
    private router: Router
  ) {

  }

  ngOnInit(): void {

  }
}
