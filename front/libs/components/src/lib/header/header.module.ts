import { NgModule } from "@angular/core";
import { HeaderComponent } from "./header.component";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { MatIconModule } from "@angular/material/icon";


@NgModule({
  declarations: [HeaderComponent],
  exports: [HeaderComponent],
  imports: [
    RouterModule,
    CommonModule,
    MatIconModule
  ],
  providers: [],
})
export class HeaderModule {}
