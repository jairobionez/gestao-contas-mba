import { NgModule } from "@angular/core";
import { InicioRouting } from "./inicio.routing";
import { MatInputModule } from "@angular/material/input";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { InicioComponent } from "./inicio.component";
import { NgxEchartsModule } from "ngx-echarts";
import { MAT_DATE_LOCALE, MatNativeDateModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { CommonModule } from "@angular/common";
import { DashboardService } from "@front/services";

@NgModule({
  imports: [InicioRouting,
    CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatDatepickerModule,
    NgxEchartsModule.forRoot({echarts: () => import('echarts')})
  ],
  exports: [],
  declarations: [InicioComponent, DashboardComponent],
  providers: [DashboardService, {provide: MAT_DATE_LOCALE, useValue: 'pt-BR'}],
})
export class InicioModule { }
