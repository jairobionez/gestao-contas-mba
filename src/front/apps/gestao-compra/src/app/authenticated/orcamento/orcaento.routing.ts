import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { OrcamentoComponent } from "./orcamento.component";
import { ListaOrcamentoComponent } from "./lista-orcamento/lista-orcamento.component";

const routes: Routes = [
  {
    path: '',
    component: OrcamentoComponent,
    children: [
      {
        path: '',
        component: ListaOrcamentoComponent
      }
    ]
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: [],
  providers: [],
})
export class OrcamentoRouting { }
