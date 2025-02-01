import { NgModule } from "@angular/core";
import { InicioRouting } from "./inicio.routing";
import { MatInputModule } from "@angular/material/input";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { InicioComponent } from "./inicio.component";
import { NgxEchartsModule } from "ngx-echarts";

@NgModule({
  imports: [InicioRouting,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    NgxEchartsModule.forRoot({echarts: () => import('echarts')})
  ],
  exports: [],
  declarations: [InicioComponent, DashboardComponent],
  providers: [],
})
export class InicioModule { }
