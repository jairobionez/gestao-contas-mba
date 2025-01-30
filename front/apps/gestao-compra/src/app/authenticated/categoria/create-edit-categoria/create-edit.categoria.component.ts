import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoriaFormGroup } from '@front/forms';

@Component({
  selector: 'app-create-edit-categoria',
  templateUrl: './create-edit-categoria.component.html',
  styleUrls: ['create-edit-categoria.component.scss'],
  standalone: false
})

export class CreateEditCategoriaComponent implements OnInit {
  form: CategoriaFormGroup;

  dialogRef = inject(MatDialogRef<CreateEditCategoriaComponent>)
  data = inject(MAT_DIALOG_DATA);

  constructor() {
    this.form = new CategoriaFormGroup();
  }

  ngOnInit() { }

  salvar(): void {
    this.dialogRef.close(true);
  }

  voltar(): void {
    this.dialogRef.close(false);
  }
}
