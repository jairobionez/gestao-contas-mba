import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriaComponent } from './categoria.component';
import { ListaCategoriaComponent } from './lista-categoria/lista-categoria.component';

const routes: Routes = [
  {
    path: '',
    component: CategoriaComponent,
    children: [
      {
        path: '',
        component: ListaCategoriaComponent
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
export class CategoriaRouting { }
