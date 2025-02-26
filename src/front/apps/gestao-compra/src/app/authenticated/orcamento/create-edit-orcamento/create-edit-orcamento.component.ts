import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoriaResponseModel, OrcamentoResponseModel } from '@front/data';
import { OrcamentoFormGroup } from '@front/forms';
import { CategoriaService, OrcamentoService } from '@front/services';
import { Observable, take } from 'rxjs';

@Component({
  selector: 'app-create-edit-orcamento',
  templateUrl: 'create-edit-orcamento.component.html',
  standalone: false
})

export class CreateEditOrcamentoComponent implements OnInit {
  form: OrcamentoFormGroup;

  dialogRef = inject(MatDialogRef<CreateEditOrcamentoComponent>)
  data = inject(MAT_DIALOG_DATA) as OrcamentoResponseModel;
  categorias$ = new Observable<CategoriaResponseModel[]>

  constructor(
    private orcamentoService: OrcamentoService,
    private categoriaService: CategoriaService,
  ) {
    this.form = new OrcamentoFormGroup();
  }

  ngOnInit() {
    this.categorias$ = this.categoriaService.getAtivos();

    if (this.data?.id) {
      this.orcamentoService.getById(this.data.id)
        .pipe(take(1))
        .subscribe(data => {
          this.form.patchValue(data);
        })
    }
  }

  salvar(): void {
    const { valid, value } = this.form;

    if (valid && this.data?.id) {
      this.orcamentoService.put(this.data.id, value)
        .pipe(take(1))
        .subscribe(_ => {
          this.dialogRef.close(true);
        })
    } else if (valid) {
      this.orcamentoService.post(value)
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
