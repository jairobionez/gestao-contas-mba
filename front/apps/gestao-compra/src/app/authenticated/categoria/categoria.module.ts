import { NgModule } from '@angular/core';
import { CategoriaComponent } from './categoria.component';
import { CategoriaRouting } from './categoria.routing';
import { ListaCategoriaComponent } from './lista-categoria/lista-categoria.component';
import { CreateEditCategoriaComponent } from './create-edit-categoria/create-edit.categoria.component';
import { CategoriaService } from '@front/services';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ValidationMessagePipe } from '@front/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  imports: [CategoriaRouting, MatTableModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    ValidationMessagePipe,
    MatDialogModule,
    MatSnackBarModule
  ],
  exports: [],
  declarations: [CategoriaComponent, ListaCategoriaComponent, CreateEditCategoriaComponent],
  providers: [CategoriaService],
})
export class CategoriaModule { }
