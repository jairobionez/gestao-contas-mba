import { NgModule } from '@angular/core';

import { LoginComponent } from './login.component';
import { LoginRouting } from './login.routing';
import { FormLoginComponent } from './form-login/form-login.component';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button'
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormaNovaContaComponent } from './form-nova-conta/form-nova-conta.component';
import { ValidationMessagePipe } from '@front/forms';
import { NgxMaskDirective } from 'ngx-mask';
import { UsuarioService } from '@front/services';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  imports: [
    LoginRouting,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    ValidationMessagePipe,
    NgxMaskDirective,
    MatSnackBarModule
  ],
  exports: [],
  declarations: [LoginComponent, FormLoginComponent, FormaNovaContaComponent],
  providers: [UsuarioService]
})
export class LoginModule {
}
