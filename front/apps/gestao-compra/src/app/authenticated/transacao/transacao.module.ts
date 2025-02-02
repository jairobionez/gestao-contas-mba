import { NgModule } from "@angular/core";
import { TransacaoComponent } from "./transacao.component";
import { TransacaoRouting } from "./transacao.routing";
import { ListaTransacaoComponent } from "./lista-transacao/lista-transacao.component";
import { MatTableModule } from "@angular/material/table";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { ValidationMessagePipe } from "@front/forms";
import { MatDialogModule } from "@angular/material/dialog";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatSelectModule } from "@angular/material/select";
import { CreateEditTransacaoComponent } from "./create-edit-transacao/create-edit-transacao.component";
import { MatNativeDateModule } from "@angular/material/core";
import { CategoriaService, TransacaoService } from "@front/services";

@NgModule({
  imports: [TransacaoRouting,
    MatTableModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    ValidationMessagePipe,
    MatDialogModule,
    MatSnackBarModule,
    MatSelectModule,
    MatNativeDateModule
  ],
  exports: [],
  declarations: [TransacaoComponent, ListaTransacaoComponent, CreateEditTransacaoComponent],
  providers: [CategoriaService, TransacaoService],
})
export class TransacaoModule { }