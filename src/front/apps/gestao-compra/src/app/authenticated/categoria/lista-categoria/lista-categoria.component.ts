import { Component, inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CategoriaResponseModel } from '@front/data';
import { CategoriaService } from '@front/services';
import { filter, switchMap, take } from 'rxjs';
import { CreateEditCategoriaComponent } from '../create-edit-categoria/create-edit.categoria.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AlertComponent, AlertOptions, ModalInfoComponent, ModalInfoModel } from '@front/components';


@Component({
  selector: 'app-lista-categoria',
  templateUrl: 'lista-categoria.component.html',
  styleUrls: ['lista-categoria.component.scss'],
  standalone: false
})
export class ListaCategoriaComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  displayedColumns: string[] = ['nome', 'descricao', 'ativo', 'acoes'];
  categorias: CategoriaResponseModel[] = [];

  constructor(
    private categoriaService: CategoriaService,
  ) {
  }

  ngOnInit() {
    this.getCategorias();
  }

  getCategorias(): void {
    this.categoriaService.get()
    .pipe(take(1))
    .subscribe(data => {
      this.categorias = data;
    });
  }

  novaCategoria(): void {
    const ref = this.dialog.open(CreateEditCategoriaComponent, {
      width: '50rem',
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Categoria criada',
            status: 'sucesso'
          } as AlertOptions
        });

        this.getCategorias();
      });
  }

  editarCategoria(categoria: CategoriaResponseModel): void {
    const ref = this.dialog.open(CreateEditCategoriaComponent, {
      width: '50rem',
      data: categoria
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Categoria alterada',
            status: 'sucesso'
          } as AlertOptions
        });

        this.getCategorias();
      });
  }

  removerCategoria(categoriaId: any): void {
    const ref = this.dialog.open(ModalInfoComponent, {
      width: '50rem',
      data: {
        titulo: 'Remover categoria',
        texto: 'Esta ação não pode ser desfeita, deseja confirma-la?',
        btnOk: 'Confirmar',
        btnCancel: 'Voltar'
      } as ModalInfoModel
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data),
      switchMap(data => this.categoriaService.delete(categoriaId)))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Categoria removida',
            status: 'sucesso'
          } as AlertOptions
        });

        this.getCategorias();
      });
  }

}
