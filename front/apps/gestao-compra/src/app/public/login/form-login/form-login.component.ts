import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form-login',
  templateUrl: 'form-login.component.html',
  styleUrls: ['form-login.component.scss'],
  standalone: false
})

export class FormLoginComponent implements OnInit {

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router
  ) {
    this.form = fb.group({
      usuario: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]]
    })
  }

  ngOnInit() { }

  novaConta(): void {
    this.router.navigate(['/nova-conta']);
  }
}
