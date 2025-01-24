import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DashboardComponent } from "./dashboard.component";

@NgModule({
    imports: [
      CommonModule,
      RouterModule,
    ],
    exports: [],
    declarations: [
      DashboardComponent,
    ],
    providers: [],
})

export class DashboardModule {}
  