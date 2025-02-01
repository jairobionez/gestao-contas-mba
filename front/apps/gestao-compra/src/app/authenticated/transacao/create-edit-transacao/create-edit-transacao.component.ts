import { Component, inject, OnInit } from '@angular/core';
import { TransacaoFormGroup } from '@front/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoriaService } from '@front/services';
import { take } from 'rxjs';
import { CategoriaResponseModel } from '@front/data';

@Component({
  selector: 'app-create-edit-transacao',
  templateUrl: './create-edit-transacao.component.html',
  styleUrls: ['create-edit-transacao.component.scss'],
  standalone: false
})
export class CreateEditTransacaoComponent implements OnInit {
  form: TransacaoFormGroup;

  dialogRef = inject(MatDialogRef<CreateEditTransacaoComponent>)
  data = inject(MAT_DIALOG_DATA);
  
  categorias: CategoriaResponseModel[] = [];

  constructor(private categoriaService: CategoriaService,) {
    this.form = new TransacaoFormGroup(); 
  }

  ngOnInit(): void { 
        //TODO e melhor passar como parametro para a dialog?
        this.categoriaService.getAtivos()
          .pipe(take(1))
          .subscribe(data => {
            this.categorias = data;
          });

  }

  salvar(): void {
    
    console.log(this.form.valid);
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
    else {
      console.log( this.form);
    }
  }

  voltar(): void {
    this.dialogRef.close(false);
  }
}