import { NgModule } from '@angular/core';
import { CategoriaComponent } from './categoria.component';
import { CategoriaRouting } from './categoria.routing';
import { ListaCategoriaComponent } from './lista-categoria/lista-categoria.component';
import { CreateEditCategoriaComponent } from './create-edit-categoria/create-edit.categoria.component';
import { CategoriaService } from '@front/services';

@NgModule({
  imports: [CategoriaRouting],
  exports: [],
  declarations: [CategoriaComponent, ListaCategoriaComponent, CreateEditCategoriaComponent],
  providers: [CategoriaService],
})
export class CategoriaModule { }
