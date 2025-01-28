import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NovaContaFormGroup } from '@front/forms';

@Component({
  selector: 'app-form-nova-conta',
  templateUrl: 'form-nova-conta.component.html',
  styleUrls: ['form-nova-conta.component.scss'],
  standalone: false
})

export class FormaNovaContaComponent implements OnInit {
  form: NovaContaFormGroup;

  constructor(
    private router: Router
  ) {
    this.form = new NovaContaFormGroup();
  }

  ngOnInit() {
    return;
  }

  voltar(): void {
    this.router.navigate(['/login']);
  }
}
