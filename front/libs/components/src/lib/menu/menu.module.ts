import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './menu.component';

@NgModule({
  imports: [RouterModule, CommonModule],
  declarations: [MenuComponent],
  exports: [MenuComponent],
  providers: [],
})
export class MenuModule { }
