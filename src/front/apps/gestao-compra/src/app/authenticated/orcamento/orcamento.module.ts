import { NgModule } from '@angular/core';

import { CategoriaService, OrcamentoService } from '@front/services';
import { OrcamentoComponent } from './orcamento.component';
import { OrcamentoRouting } from './orcaento.routing';
import { ListaOrcamentoComponent } from './lista-orcamento/lista-orcamento.component';
import { CreateEditOrcamentoComponent } from './create-edit-orcamento/create-edit-orcamento.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { ValidationMessagePipe } from '@front/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  imports: [
    OrcamentoRouting,
    ReactiveFormsModule,
    CommonModule,
    MatTableModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    ValidationMessagePipe,
    MatSelectModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  exports: [],
  declarations: [OrcamentoComponent, ListaOrcamentoComponent, CreateEditOrcamentoComponent],
  providers: [OrcamentoService, CategoriaService],
})
export class OrcamentoModule { }
