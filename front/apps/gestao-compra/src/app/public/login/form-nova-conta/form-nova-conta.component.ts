import { Component, inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AlertComponent, AlertOptions } from '@front/components';
import { NovaContaFormGroup } from '@front/forms';
import { UsuarioService } from '@front/services';
import { take } from 'rxjs';

@Component({
  selector: 'app-form-nova-conta',
  templateUrl: 'form-nova-conta.component.html',
  styleUrls: ['form-nova-conta.component.scss'],
  standalone: false
})

export class FormaNovaContaComponent implements OnInit {
  private _snackBar = inject(MatSnackBar);
  
  form: NovaContaFormGroup;

  constructor(
    private router: Router,
    private usuarioService: UsuarioService
  ) {
    this.form = new NovaContaFormGroup();
  }

  ngOnInit() {
    return;
  }

  salvarConta(){
    this.usuarioService.post(this.form.value)
    .pipe(take(1))
    .subscribe(_ => {
      this._snackBar.openFromComponent(AlertComponent, {
        duration: 5000,
        data: {
          title: 'Sucesso!',
          subtitle: 'conta criada!',
          status: 'sucesso'
        } as AlertOptions
      });

      this.voltar();
    })
  }

  voltar(): void {
    this.router.navigate(['/login']);
  }
}
