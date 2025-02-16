import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '@front/services';
import { take } from 'rxjs';

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
    private router: Router,
    private authService: AuthService
  ) {
    this.form = fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]]
    })
  }

  ngOnInit() { }


  login():void {
    const { value, valid } = this.form;

    if(valid) {
      this.authService.login(value)
      .pipe(take(1))
      .subscribe(token => {
        if(token) {
          console.log(token);
          this.authService.setToken(token.data.accessToken);
          this.authService.setUrl('inicio');
          this.router.navigate(['inicio']);
        }
      });
    }
  }

  novaConta(): void {
    this.router.navigate(['/nova-conta']);
  }
}
