import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TransacaoComponent } from "./transacao.component";
import { ListaTransacaoComponent } from "./lista-transacao/lista-transacao.component";

const routes: Routes = [
  {
    path: '',
    component: TransacaoComponent,
    children: [
      {
        path: '',
        component: ListaTransacaoComponent
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
export class TransacaoRouting { }