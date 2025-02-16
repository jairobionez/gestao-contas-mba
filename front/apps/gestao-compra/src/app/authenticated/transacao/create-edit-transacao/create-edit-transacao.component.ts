import { Component, inject, OnInit } from '@angular/core';
import { TransacaoFormGroup } from '@front/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoriaService, TransacaoService } from '@front/services';
import { take } from 'rxjs';
import { CategoriaResponseModel } from '@front/data';
import { formatDate } from '@angular/common';

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

  constructor(private categoriaService: CategoriaService, private transacaoService: TransacaoService) {
    this.form = new TransacaoFormGroup(); 
  }

  ngOnInit(): void { 
    //TODO e melhor passar como parametro para a dialog?
    this.categoriaService.getAtivos()
      .pipe(take(1))
      .subscribe(data => {
        this.categorias = data;
      });

    if(this.data.id) {
      this.transacaoService.getById(this.data.id)
        .pipe(take(1))
        .subscribe(data => {
          console.log(data);
          this.form.patchValue(data);
        })
    }

  }

  salvar(): void {
    const { valid, value } = this.form;
    if(valid && this.data?.id) {
      this.transacaoService.put(this.data.id, value)
      .pipe(take(1))
      .subscribe(_ => {
        this.dialogRef.close(true);
      })
    } else if(valid) {
      this.transacaoService.post(value)
      .pipe(take(1))
      .subscribe(_ => {
        this.dialogRef.close(true);
      })
    }
  }

  voltar(): void {
    this.dialogRef.close(false);
  }

}