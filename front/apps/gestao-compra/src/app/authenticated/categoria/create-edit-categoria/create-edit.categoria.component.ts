import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoriaResponseModel } from '@front/data';
import { CategoriaFormGroup } from '@front/forms';
import { CategoriaService } from '@front/services';
import { take } from 'rxjs';

@Component({
  selector: 'app-create-edit-categoria',
  templateUrl: './create-edit-categoria.component.html',
  styleUrls: ['create-edit-categoria.component.scss'],
  standalone: false
})

export class CreateEditCategoriaComponent implements OnInit {
  form: CategoriaFormGroup;

  dialogRef = inject(MatDialogRef<CreateEditCategoriaComponent>)
  data = inject(MAT_DIALOG_DATA) as CategoriaResponseModel;

  constructor(
    private categoriaService: CategoriaService
  ) {
    this.form = new CategoriaFormGroup();
  }

  ngOnInit() {
    if(this.data.id) {
      this.categoriaService.getById(this.data.id)
        .pipe(take(1))
        .subscribe(data => {
          this.form.patchValue(data);
        })
    }
  }

  salvar(): void {
    const { valid, value } = this.form;

    if(valid && this.data?.id) {
      this.categoriaService.put(this.data.id, value)
      .pipe(take(1))
      .subscribe(_ => {
        this.dialogRef.close(true);
      })
    } else if(valid) {
      this.categoriaService.post(value)
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
