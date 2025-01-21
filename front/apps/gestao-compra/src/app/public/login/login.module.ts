import { NgModule } from '@angular/core';

import { LoginComponent } from './login.component';
import { LoginRouting } from './login.routing';

@NgModule({
  imports: [LoginRouting],
  exports: [],
  declarations: [LoginComponent],
  providers: []
})
export class LoginModule {
}
