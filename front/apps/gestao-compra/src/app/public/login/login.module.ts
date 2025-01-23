import { NgModule } from '@angular/core';

import { LoginComponent } from './login.component';
import { LoginRouting } from './login.routing';
import { FormLoginComponent } from './form-login/form-login.component';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button'
import { MatFormFieldModule } from '@angular/material/form-field';

@NgModule({
  imports: [LoginRouting, MatInputModule, ReactiveFormsModule, MatButtonModule, MatFormFieldModule],
  exports: [],
  declarations: [LoginComponent, FormLoginComponent],
  providers: []
})
export class LoginModule {
}
