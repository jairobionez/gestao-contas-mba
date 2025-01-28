import { Component } from '@angular/core';
import { PublicRouting } from './public.routing';
import { Routes } from '@angular/router';


@Component({
  selector: 'app-public',
  standalone: false,
  template: '<router-outlet></router-outlet>'
})

export class PublicComponent {
}
